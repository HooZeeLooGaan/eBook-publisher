using Amazon.SQS;
using Amazon.SimpleNotificationService;

var builder = Host.CreateDefaultBuilder(args);
builder.UseNServiceBus(context =>
    {
       var endpointConfiguration = new EndpointConfiguration("ContentProcessinfService");

       var transport = new SqsTransport(
            new AmazonSQSClient(new Amazon.Runtime.BasicAWSCredentials("test", "test"), new AmazonSQSConfig { ServiceURL = "http://localhost:4566" }),
            new AmazonSimpleNotificationServiceClient(new Amazon.Runtime.BasicAWSCredentials("test", "test"), new AmazonSimpleNotificationServiceConfig { ServiceURL = "http://localhost:4566" })
        );

        endpointConfiguration.UseTransport(transport);
        endpointConfiguration.UseSerialization<SystemJsonSerializer>();
        endpointConfiguration.EnableInstallers();

        return endpointConfiguration;
    });

var host = builder.Build();
await host.RunAsync();
