using Microsoft.Extensions.Logging;

namespace Cff.Extensions;

public static class LoggerExtension
{
    public static Eff<Unit> InfoEff(this ILogger logger, string message, params object?[] args)
    {
        logger.LogInformation(message, args);
        return unitEff;
    }
}
