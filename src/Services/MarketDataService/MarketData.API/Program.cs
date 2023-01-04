using MarketData.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IMarketDataService, MarketDataService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("api/marketdata", (IMarketDataService _marketDataService) =>
{
    var data = _marketDataService.GetAllMarketData();
    return Results.Ok(data);
});

app.MapGet("api/marketdata/{id}", (IMarketDataService _marketDataService, string id) =>
{
    var Item = _marketDataService.GetMarketDataBySymbol(id.ToUpper());

    return Results.Ok(Item);

});

app.Run();