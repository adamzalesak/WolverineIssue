using Oakton;
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

    opts.Policies.UseDurableLocalQueues();
});

builder.Host.UseResourceSetupOnStartup();
builder.Host.ApplyOaktonExtensions();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapPost("/invoke", async (IMessageBus bus) =>
    {
        var command = new TestCommand();
        // TestCommand returned value should not be persisted in inbox
        var response = await bus.InvokeAsync<string>(command);

        return Results.Ok(response);
    })
    .WithName("Invoke")
    .WithOpenApi();


app.Run();
// return await app.RunOaktonCommands(args);


public record TestCommand;

public class TestCommandHandler
{
    public string Handle(TestCommand command, IMessageBus bus)
    {
        // this value is persisted in inbox
        return "response";
    }
}