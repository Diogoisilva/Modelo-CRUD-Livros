using Microsoft.Extensions.Logging;

namespace Modelo.Cross.Helpers
{
    public class Logger
    {
        private readonly ILogger<Logger> _logger;

        public Logger(ILogger<Logger> logger)
        {
            _logger = logger;
        }

        public void LogInformation(string message)
        {
            _logger.LogInformation(message);
        }

        public void LogError(string message)
        {
            _logger.LogError(message);
        }
    }
}
