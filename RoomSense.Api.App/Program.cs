using RoomSense.Api.Lib.Authentication;
using RoomSense.Api.Lib.Data;
using RoomSense.Api.Lib.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

builder.Services.AddNpgsql<TemperatureHumidityDbContext>(builder.Configuration.GetConnectionString("room_sense_db"));

builder.Services.AddScoped<ApiKeyFilter>();
builder.Services.AddTransient<ITimeStampGenerator, TimeStampGenerator>();
builder.Services.AddTransient<IIdGenerator, IdGenerator>();
builder.Services.AddTransient<ITemperatureHumidityService, TemperatureHumidityService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();