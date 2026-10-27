using Amazon.SQS;
using Amazon.SimpleNotificationService;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

var awsServiceUrl = builder.Configuration["AWS:ServiceUrl"] ?? "http://localhost:4566";
// Configure Amazon SQS
builder.Host.UseNServiceBus(context =>
{
    var endpointConfiguration = new EndpointConfiguration("IngressWeb");

    var transport = new SqsTransport(
        new AmazonSQSClient(new Amazon.Runtime.BasicAWSCredentials("test", "test"), new AmazonSQSConfig { ServiceURL = awsServiceUrl }),
        new AmazonSimpleNotificationServiceClient(new Amazon.Runtime.BasicAWSCredentials("test", "test"), new AmazonSimpleNotificationServiceConfig { ServiceURL = awsServiceUrl })
    );

    endpointConfiguration.UseTransport(transport);
    endpointConfiguration.UseSerialization<SystemJsonSerializer>();
    endpointConfiguration.EnableInstallers();

    return endpointConfiguration;
});

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
    c.SwaggerDoc("v1", new Microsoft.OpenApi.OpenApiInfo
    {
       Title = "eBook Publishing API",
       Version = "v1",
       Description = "An event-driven microservices platform for publishing and managing books using .NET and NServiceBus." 
    });
});

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// app.UseHttpsRedirection();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "eBook API V1");
    c.RoutePrefix = string.Empty; // This sets Swagger UI as the root application page (http://localhost:5000/)
});

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/test", () => "Ingress API is running!")
    .ExcludeFromDescription();;

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.ExcludeFromDescription();
app.UseCors();
app.MapControllers();
app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
