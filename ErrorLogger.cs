using System.Runtime.CompilerServices;

namespace EntitiesItSpecBot
{
    public class ErrorLogger
    {
        private char sep = Path.AltDirectorySeparatorChar;
        private string? fileName;
        private string? curDirectory;
        private string? path;

        public ErrorLogger()
        {
            curDirectory = Directory.GetCurrentDirectory();
            Directory.CreateDirectory(curDirectory + sep + "ErrorLogs");
            curDirectory += sep + "ErrorLogs";
        }

        public void LogBotError(string errorMessage, Exception e, [CallerLineNumber] int lineNumber = 0)
        {
            LogMessage($"Bot Error: {errorMessage}", e, lineNumber);
        }

        public void LogBotError(string errorMessage, [CallerLineNumber] int lineNumber = 0)
        {
            LogMessage($"Bot Error: {errorMessage}", lineNumber);
        }

        private void LogMessage(string data, Exception exception, [CallerLineNumber] int lineNumber = 0)
        {
            try
            {
                string message = $"~~{DateTime.Now} line:{lineNumber} - Exception: {data}\n{exception.Message}\n{exception.StackTrace}\n";
                fileName = sep + DateTime.Now.ToString("dd.MM.yyyy") + ".txt";
                path = curDirectory + fileName;
                using StreamWriter logWriter = new StreamWriter(path, true);
                logWriter.WriteLine(message);
                logWriter.Flush();
            }
            catch { }
        }

        private void LogMessage(string data, [CallerLineNumber] int lineNumber = 0)
        {
            try
            {
                string message = $"~~{DateTime.Now} line:{lineNumber} - Info: {data}\n";
                fileName = sep + DateTime.Now.ToString("dd.MM.yyyy") + ".txt";
                path = curDirectory + fileName;
                using StreamWriter logWriter = new StreamWriter(path, true);
                logWriter.WriteLine(message);
                logWriter.Flush();
            }
            catch { }
        }
    }
}