public class GameStartMessage
{
    public string GameName { get; }

    public GameStartMessage(string gameName)
    {
        GameName = gameName;
    }
}