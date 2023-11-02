using mortgage_calculator_dotNetCore.Interfaces;
using mortgage_calculator_dotNetCore.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<IMortgageCalculateFactory, MortgageCalculateFactory>();
builder.Services.AddScoped<IMortgageCalculate, MortgageMonthlyService>();
builder.Services.AddScoped<IMortgageCalculate, MortgageWeeklyService>(); 
builder.Services.AddScoped<IMortgageCalculate, MortgageMonthlyService>(); 



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactOrigin", builder =>
    {
        builder
            .WithOrigins("http://localhost:5170", "https://localhost:5170") 
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors("AllowReactOrigin");

app.Run();

