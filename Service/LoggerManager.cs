using NLog;
using Service.Contracts;

namespace Service;

public class LoggerManager : ILoggerManager
{
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();

    public void LogInfo(string message)
    {
        logger.Info(message);
    }

    public void LogWarn(string message)
    {
        logger.Warn(message);
    }

    public void LogDebug(string message)
    {
        logger.Debug(message);
    }

    public void LogError(string message)
    {
        logger.Error(message);
    }
}