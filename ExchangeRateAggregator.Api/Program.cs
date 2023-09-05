using ExchangeRateAggregator.Api.Extensions;
using ExchangeRateAggregator.Composition;

var builder = WebApplication.CreateBuilder(args);

#region Service Scope
CompositionRoot.RegisterDependencies(builder.Services, builder.Configuration);

builder.Services.AddExchangeRateAggregatorSwagger();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
#endregion

#region App Scope
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

#endregion