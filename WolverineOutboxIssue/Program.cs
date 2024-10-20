using JasperFx.Core;
using Oakton.Resources;
using Wolverine;
using Wolverine.SqlServer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Host.UseWolverine(opts =>
{
    // fill in the connection string
    var connectionString = "";
    opts.PersistMessagesWithSqlServer(connectionString, "wolverine");
    
    // the issue occurs only in Solo durability mode
    opts.Durability.Mode = DurabilityMode.Solo;

    opts.Policies.UseDurableLocalQueues();
});

builder.Host.UseResourceSetupOnStartup();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapPost("/test", async (IMessageBus bus) =>
    {
        var command = new TestCommand();
        var response = await bus.InvokeAsync<string>(command);

        return Results.Ok(response);
    })
    .WithName("Test")
    .WithOpenApi();


app.Run();
// return await app.RunOaktonCommands(args);


public record TestCommand;

public class TestCommandHandler
{
    public async Task Handle(TestCommand command, IMessageBus bus)
    {
        // this message is persisted in inbox and flushed
        await bus.PublishAsync(new TestEvent());
    }
}

public record TestEvent;
public class TestEventHandler
{
    public async Task Handle(TestEvent @event)
    {
        Console.WriteLine("Test event handler started");

        await Task.Delay(30.Seconds());
        // ! message is already marked as "Handled" in the inbox

        Console.WriteLine("Test event handler finished");
    }
}
