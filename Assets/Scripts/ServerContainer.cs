using JamForge.Resolver;
using MessagePipe;
using VContainer;

public class TestServices
{
    public string GetText() => "Hello World";
}

public class ServerContainer : JamInstaller
{
    public override void Install(IContainerBuilder builder)
    {
        builder.RegisterMessageBroker<GameStartMessage>(MessagePipeOptions);
        builder.Register<TestServices>(Lifetime.Singleton);
    }
}
