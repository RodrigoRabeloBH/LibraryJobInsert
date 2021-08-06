using LibraryJobInsert.Domain.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryJobInsert.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IApplicationServices _services;

        public Worker(ILogger<Worker> logger, IApplicationServices services)
        {
            _logger = logger;
            _services = services;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                try
                {
                    var messages = await _services.GET("IntegracaoClientes");

                    if (messages != null)
                    {
                        var executeMessages = await _services.InsertCustomer(messages);

                        await _services.POST(executeMessages);
                    }
                }
                catch (Exception exc)
                {

                    _logger.LogInformation(exc.Message);
                }

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
