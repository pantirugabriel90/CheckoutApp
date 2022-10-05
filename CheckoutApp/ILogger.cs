using System.Runtime.CompilerServices;

namespace CheckoutApp
{
    public interface ICustomLogger
    {
        void LogError(string text, [CallerMemberName] string callerName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int lineNumber = 0);
        void LogInfo(string text, [CallerMemberName] string callerName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int lineNumber = 0);
        void LogWarning(string text, [CallerMemberName] string callerName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int lineNumber = 0);
    }
}
