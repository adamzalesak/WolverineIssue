using Confluent.Kafka;
using Wolverine;
using Wolverine.Kafka;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Host.UseWolverine(opts =>
{
    opts.UseKafka("")
        .ConfigureClient(c =>
        {
            c.SaslUsername = "";
            c.SaslPassword = "";
            c.SaslMechanism = SaslMechanism.Plain;
            c.SecurityProtocol = SecurityProtocol.SaslSsl;
        });

    opts.PublishAllMessages().ToKafkaTopic("topic_0");

    opts.BatchMessagesOf<TestMessage>();
    opts.ListenToKafkaTopic("topic_0");
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/test", async (IMessageBus bus) =>
    {
        var message = new TestMessage();
        await bus.PublishAsync(message);
        await bus.PublishAsync(message);
        // results in:
        // No known handler for TestMessage#08dced0c-3834-b4c6-54d7-e075bf020000 from kafka://topic/topic_0
    })
    .WithOpenApi();

app.Run();
// return await app.RunOaktonCommands(args);


public record TestMessage;

public class TestMessagesHandler
{
    public void Handle(TestMessage[] messages)
    {
        Console.WriteLine("Messages received");
    }
}