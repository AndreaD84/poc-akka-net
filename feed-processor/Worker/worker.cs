using feed_processor.Interfaces;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace feed_processor.Worker
{
    public sealed class worker : BackgroundService
    {
        private IMainService _mainService { get; }
        private ILogger _logger { get; }

        public worker(IMainService mainService, ILogger logger)
        {
            _mainService = mainService;
            _logger = logger;
        }

        
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.Debug("Execute Async Worker");

            while(!stoppingToken.IsCancellationRequested)
            {
                _mainService.StartMainService();

                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            }
        }
    }
}
