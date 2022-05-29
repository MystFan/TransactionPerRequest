using TransactionPerRequest.Api;
using TransactionPerRequest.Api.Extensions;
using TransactionPerRequest.Api.Infrastructure;
using TransactionPerRequest.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDatabase(builder.Configuration.GetConnectionString("DefaultConnection"));
builder.Services.AddTransient<IInitializer, DatabaseInitializer>();
builder.Services.AddScoped<TransactionFilter>();

builder.Services.AddControllers(o =>
{
    o.Filters.AddService<TransactionFilter>(1);
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapControllers();

app.Initialize();

app.Run();
