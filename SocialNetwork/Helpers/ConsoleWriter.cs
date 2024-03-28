namespace SocialNetwork.Helpers
{
    public static class ConsoleWriter
    {
        public static void GetAlarmMessageConsole(string message)
        {
            ConsoleColor def = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("******************");
            Console.WriteLine(message);
            Console.WriteLine("******************");
            Console.ForegroundColor=def;
        }
    }
}
