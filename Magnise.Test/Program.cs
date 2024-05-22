
using Magnise.Test.BL.MappingProfiles;
using Magnise.Test.BL.Services;
using Magnise.Test.DAL;
using Magnise.Test.DAL.Repositories.Read;
using Magnise.Test.DAL.Repositories.Write;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddDbContext<CryptoDBContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("MSSQLConnection"))
);

builder.Services.AddScoped<ICryptocurrencyWriteRepository, CryptocurrencyWriteRepository>();
builder.Services.AddScoped<ICryptocurrencyReadRepository, CryptocurrencyReadRepository>();

var assemblies = new[]
{
    Assembly.GetAssembly(typeof(MappingProfile))
};
builder.Services.AddAutoMapper(assemblies);

builder.Services.AddHostedService<UpdateCurrenciesBackgroundService>();

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