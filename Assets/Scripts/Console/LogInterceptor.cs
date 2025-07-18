using UnityEngine;

public class LogInterceptor : ILogHandler
{
    private readonly ILogHandler original;
    private readonly System.Action<string> uiLogger;

    public LogInterceptor(ILogHandler originalHandler, System.Action<string> uiCallback)
    {
        original = originalHandler;
        uiLogger = uiCallback;
    }

    public void LogFormat(LogType logType, Object context, string format, params object[] args)
    {
        original.LogFormat(logType, context, format, args);
        string msg = string.Format(format, args);
        uiLogger?.Invoke($"[{logType}] {msg}");
    }

    public void LogException(System.Exception exception, Object context)
    {
        original.LogException(exception, context);
        uiLogger?.Invoke($"[Exception] {exception.Message}\n{exception.StackTrace}");
    }
}
