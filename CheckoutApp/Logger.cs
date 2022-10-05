using System;
using System.Runtime.CompilerServices;

namespace CheckoutApp
{
    public class Logger: ICustomLogger
    {
        public void LogError(string text, [CallerMemberName] string callerName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int lineNumber = 0) { 
            Console.WriteLine($"[Error] {DateTime.UtcNow} {sourceFilePath}:{lineNumber}\t{callerName}():\t\t {text}");
        }

        public void LogInfo(string text, [CallerMemberName] string callerName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int lineNumber = 0)
        {

            Console.WriteLine($"[Info] {DateTime.UtcNow} {sourceFilePath}:{lineNumber}\t{callerName}():\t\t {text}");
        }

        public void LogWarning(string text, [CallerMemberName] string callerName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int lineNumber = 0)
        {
            Console.WriteLine($"[Warning] {DateTime.UtcNow} {sourceFilePath}:{lineNumber}\t{callerName}():\t\t {text}");
        }
    }
}
