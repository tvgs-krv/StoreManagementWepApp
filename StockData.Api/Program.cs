using MediatR;
using System.Reflection;
using Destructurama;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Serilog.Events;
using Serilog;
using System.Text.Json.Serialization;
using Services.Common.Behaviors;
using Services.Common.Middleware;
using StockData.Api.Application.Interfaces;
using StockData.Api.Application.Queries;
using StockData.Api.Infrastructure;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Destructure.UsingAttributes()
    .Enrich.WithProcessId()
    .Enrich.WithProcessName()
    .WriteTo.File(path: "/Logs/.log",
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff}|{Level:u3}|{ProcessId}|{ThreadId:x}|{Message:lj}{NewLine}{Exception}",
        rollingInterval: RollingInterval.Day)
    .CreateLogger();


var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services.AddControllers()
    .AddJsonOptions(options => { options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles; });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();

builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddValidatorsFromAssemblies(new[] { Assembly.GetExecutingAssembly() });
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
builder.Services.AddScoped<IStockQueries, StockQueries>();
builder.Services.AddDbContext<StockDbContext>(opt =>
{
    var connectionString = config["DbConnection"];
    opt.UseNpgsql(connectionString);
});
builder.Services.AddScoped<IStockDbContext>(provider => provider.GetService<StockDbContext>());
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        policy.AllowAnyOrigin();
    });
});
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});
builder.Services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true;
});
builder.Host.UseSerilog();


var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    try
    {
        var context = serviceProvider.GetRequiredService<StockDbContext>();
        context.Database.EnsureCreated();
    }
    catch (Exception e)
    {
        Log.Fatal(e, "An error occured while app initialization");
    }
}

app.UseSwagger();
app.UseSwaggerUI();
app.UseDeveloperExceptionPage();

app.UseMiddleware<LoggingInputDataFromControllerMiddleware>();
app.UseMiddleware<CustomExceptionHandlerMiddleware>();

app.UseRouting();
app.UseCors("AllowAll");

app.MapControllers();
app.Run();
