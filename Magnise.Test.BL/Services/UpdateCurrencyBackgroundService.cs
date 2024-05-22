
using AutoMapper;
using Magnise.Test.BL.DTO;
using Magnise.Test.BL.Exceptions;
using Magnise.Test.DAL;
using Magnise.Test.DAL.Entities;
using Magnise.Test.DAL.Repositories.Read;
using Magnise.Test.DAL.Repositories.Write;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Net.Http.Headers;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;

namespace Magnise.Test.BL.Services
{
    public class UpdateCurrenciesBackgroundService : BackgroundService
    {
        private readonly IMemoryCache _cache;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly ICryptocurrencyReadRepository _readRepo;
        private readonly ICryptocurrencyWriteRepository _writeRepo;
        private readonly CryptoDBContext _dbContext;

        private readonly string _apiKey;
        private readonly string _endpointREST;
        private readonly string _endpointSocket;

        public UpdateCurrenciesBackgroundService(
            IMemoryCache cache,
            ILogger<UpdateCurrenciesBackgroundService> logger,
            IMapper mapper,
            IServiceScopeFactory scopeFactory,
            IConfiguration configuration)
        {
            _cache = cache;
            _logger = logger;
            _mapper = mapper;
            _readRepo = scopeFactory.CreateScope().ServiceProvider.GetRequiredService<ICryptocurrencyReadRepository>();
            _writeRepo = scopeFactory.CreateScope().ServiceProvider.GetRequiredService<ICryptocurrencyWriteRepository>();
            _dbContext = scopeFactory.CreateScope().ServiceProvider.GetRequiredService<CryptoDBContext>();

            _apiKey = configuration.GetSection("api-key").Value;
            _endpointREST = configuration.GetSection("endpoint-rest").Value;
            _endpointSocket = configuration.GetSection("endpoint-websocket").Value;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation($"{nameof(UpdateCurrenciesBackgroundService)} is starting.");

            _dbContext.Database.EnsureDeleted();
            _dbContext.Database.EnsureCreated();

            var dbData = await _readRepo.GetAllCurrenciesAsync();

            if( dbData.IsNullOrEmpty() )
            {
                try
                {
                    var initialCurr = await GetInitialCurencies();

                    await _writeRepo.InitCurrencies( initialCurr );

                    // Caching currencies ID and AssetID
                    var dbCurrencies = await _readRepo.GetAllCurrenciesAsync();
                    foreach (var x in dbCurrencies)
                    {
                        _cache.Set(x.AssetID, x.ID);
                    }
                }
                catch (CannotObtainInitialCryptocurrenciesException ex)
                {
                    _logger.LogError(ex.Message);
                }
                catch(Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
            }

            while (!stoppingToken.IsCancellationRequested)
            {
                ClientWebSocket webSocket = await InitSocketConnectionAsync(stoppingToken);

                await ReceiveMessagesFromSocketAsync(webSocket, stoppingToken);
            }
        }

        private async Task<ClientWebSocket> InitSocketConnectionAsync(CancellationToken stoppingToken)
        {
            var webSocket = new ClientWebSocket();
            webSocket.Options.SetRequestHeader("X-CoinAPI-Key", _apiKey);

            var uri = new Uri(_endpointSocket);

            await webSocket.ConnectAsync(uri, stoppingToken);

            _logger.LogInformation("Connected to WebSocket API.");

            var helloMessage = @"{
                                      ""type"": ""hello"",
                                      ""apikey"": ""ff869de0-1553-4d86-af08-3acf1dcf91cb"",
                                      ""heartbeat"": false,
                                      ""subscribe_data_type"": [
                                        ""trade""
                                      ],
                                      ""subscribe_filter_asset_id"": [""USD""],
                                      ""subscribe_filter_taker_side"": [
                                        ""BUY""
                                      ],
                                        ""subscribe_filter_exchange_id"": [
                                        ""COINBASE""
                                      ]
                                    }";

            var messageBytes = Encoding.UTF8.GetBytes(helloMessage);
            await webSocket.SendAsync(new ArraySegment<byte>(messageBytes), WebSocketMessageType.Text, true, stoppingToken);

            _logger.LogInformation("Hello message was sent.");

            return webSocket;
        }

        private async Task ReceiveMessagesFromSocketAsync(ClientWebSocket webSocket, CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var buffer = new byte[1024 * 4];
                var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), stoppingToken);

                if (result.MessageType == WebSocketMessageType.Text)
                {
                    var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                    await ProcessMessage(message);
                }
                else if (result.MessageType == WebSocketMessageType.Close)
                {
                    await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, stoppingToken);
                    _logger.LogInformation("WebSocket connection closed.");
                }

                if(webSocket.State != WebSocketState.Open) // If connection was closed then exit from current method
                {
                    break;
                }

                await Task.Delay(100, stoppingToken); // Prevent system overload
            }
        }

        private async Task<IEnumerable<Cryptocurrency>> GetInitialCurencies()
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("X-CoinAPI-Key", _apiKey);

                var response = await client.GetAsync(_endpointREST);

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var assetsDTO = JsonSerializer.Deserialize<List<CryptocurrencyDTO>>(data);

                    var filteredAssetsDTO = assetsDTO.Where(x => x.IsCrypto == 1 && x.PriceInUSD > 0);

                    return _mapper.Map<IEnumerable<Cryptocurrency>>(filteredAssetsDTO);
                }
                else
                {
                    throw new CannotObtainInitialCryptocurrenciesException(response.StatusCode.ToString());
                }
            }
        }

        private async Task ProcessMessage(string message)
        {
            var currency = JsonSerializer.Deserialize<CryptocurrencyUpdateDTO>(message);

            if (currency == null || (currency.Sequence % 25) != 0 ) // Taking only every 25th trade to prevent unnecessary frequent database calls
            {
                return;
            }

            var resultCurrency = _mapper.Map<Cryptocurrency>(currency);

            resultCurrency.ID = (int)_cache.Get(resultCurrency.AssetID);

            await _writeRepo.UpdateCurrencyPriceAsync(resultCurrency);
        }
    }
}
