using Magnise.Test.DAL;
using Magnise.Test.DAL.Repositories.Read;
using Magnise.Test.DAL.Repositories.Write;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddDbContext<CryptoDBContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("MSSQLConnection"))
);

builder.Services.AddScoped<ICryptocurrencyWriteRepository, CryptocurrencyWriteRepository>();
builder.Services.AddScoped<ICryptocurrencyReadRepository, CryptocurrencyReadRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
