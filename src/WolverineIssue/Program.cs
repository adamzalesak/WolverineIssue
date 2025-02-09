using JasperFx.CodeGeneration;
using Oakton;
using Wolverine;
using WolverineIssue.Messages;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Host.UseWolverine(opts =>
{
    opts.CodeGeneration.TypeLoadMode = TypeLoadMode.Auto;
    
    opts.PublishMessage<TestMessage>().ToLocalQueue("queue1");
    opts.PublishMessage<TestMessage>().ToLocalQueue("queue2");
    opts.PublishMessage<TestMessage>().ToLocalQueue("queue3");
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();


// issue #1
// expected behavior:
// 1. Message1Handler and Message2Handler called sequentially by a single generated class (both sticky with queue1)
// 2. Message3Handler called twice (no sticky attribute, so it handles all queues without
// any sticky handler (queue2 and queue3) - not ideal IMHO but I suppose this is the current expected behavior)
// 3. "-- codegen write" generates code for all handlers
//
// actual behavior:
// 1. Message1Handler called once, Message2Handler not called at all - NOT OK
// 2. Message3Handler called twice - OK
// 3. If the load mode is TypeLoadMode.Auto, the code is generated at runtime but "-- codegen write"
// generates code only for Message3Handler -> leads to app crash if TypeLoadMode.Static
app.MapPost("/test", async (IMessageBus bus) => await bus.PublishAsync(new TestMessage()));

// issue #2
// AbcHandler is not discovered with this exact name. If the handler is renamed, it works as expected.
app.MapPost("/abc", async (IMessageBus bus) => await bus.PublishAsync(new AbcMessage()));

return await app.RunOaktonCommands(args);