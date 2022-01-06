using feed_processor.Interfaces;
using Serilog;

namespace feed_processor.Services
{
    public class MainService : IMainService
    {
        private ILogger _logger;

        public MainService(ILogger logger)
        {
            _logger = logger;
        }
        public void StartMainService()
        {
            _logger.Debug("Main Service Start");
        }
    }
}
