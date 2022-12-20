using api.Extensions;
using NLog;
using Service.Contracts;

var builder = WebApplication.CreateBuilder(args);

LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(),
    "/nlog.config"));

// Add services to the container.
builder.Services.ConfigureSqlContext(builder.Configuration); // Db context
builder.Services.ConfigureRepositoryManager(); // Repository
builder.Services.ConfigureServiceManager(); // Services
builder.Services.AddAutoMapper(typeof(Program)); // Automapper
builder.Services.AddAuthentication(); // Auth
builder.Services.ConfigureIdentity(); // Auth
builder.Services.ConfigureJwt(builder.Configuration); // Auth
builder.Services.ConfigureLoggerService(); // Logger
builder.Services.AddHttpContextAccessor(); // HttpContext

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
var logger = app.Services.GetRequiredService<ILoggerManager>();
app.ConfigureExceptionHandler(logger);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();