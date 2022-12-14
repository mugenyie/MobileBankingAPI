using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MobileBanking.Services.Interfaces;
using MobileBanking.Shared;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace MobileBanking.WorkerService
{
    public class LoggingWorkerService : BackgroundService
    {
        private readonly ILogger<LoggingWorkerService> _logger;
        private ConnectionFactory _connectionFactory;
        private IConnection _connection;
        private IModel _channel;
        private readonly string RabbitMQURI = "";
        private readonly string QueueName = "";

        private IServiceScopeFactory _services { get; }

        public LoggingWorkerService(ILogger<LoggingWorkerService> logger, IServiceScopeFactory services)
        {
            _logger = logger;
            _services = services;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _connectionFactory = new ConnectionFactory
            {
                Uri = new Uri(RabbitMQURI),
                DispatchConsumersAsync = true
            };
            _connection = _connectionFactory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: QueueName,
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);
            _logger.LogInformation($"Queue [{QueueName}] is waiting for messages.");

            return base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //listen to transactions and process
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.Received += async (sender, e) =>
            {
                var message = Encoding.UTF8.GetString(e.Body.ToArray());
                _logger.LogInformation($"Processing msg: '{message}'.");
                try
                {
                    await Task.Run((Action)(() =>
                    {
                        var log = JsonConvert.DeserializeObject<LogVM>(message);

                        using (var scope = _services.CreateScope())
                        {
                            var _transactionLogService = scope.ServiceProvider.GetRequiredService<ILoggingService>();
                            //_transactionLogService.AddTransactionLogAsync((string)log.Title, (string)log.Data);
                        }
                    }));

                    _channel.BasicAck(e.DeliveryTag, false);
                }
                catch (System.Text.Json.JsonException)
                {
                    _logger.LogError($"JSON Parse Error: '{message}'.");
                    _channel.BasicNack(e.DeliveryTag, false, false);
                }
                catch (AlreadyClosedException)
                {
                    _logger.LogInformation("RabbitMQ is closed!");
                }
                catch (Exception exp)
                {
                    _logger.LogError(default, exp, exp.Message);
                }
            };

            _channel.BasicConsume(queue: QueueName, autoAck: false, consumer: consumer);

            await Task.CompletedTask;
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            await base.StopAsync(cancellationToken);
            _connection.Close();
            _logger.LogInformation("RabbitMQ connection is closed.");
        }
    }
}
