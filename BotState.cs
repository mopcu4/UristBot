namespace UristBot
{
    public static class BotState
    {
        public static Dictionary<long, string> UserLastButton { get; } = new();
    }
}