using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MobileBanking.Services.Interfaces;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MobileBanking.WorkerService
{
    public class TransactionsWorker : BackgroundService
    {
        private ConnectionFactory _connectionFactory;
        private IConnection _connection;
        private IModel _channel;
        private readonly string RabbitMQURI = "";
        private readonly string QueueName = "";

        private IServiceScopeFactory _services { get; }

        public TransactionsWorker(IServiceScopeFactory services)
        {
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

            return base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.Received += async (sender, e) =>
            {
                var message = Encoding.UTF8.GetString(e.Body.ToArray());
                try
                {
                    var transaction = JsonConvert.DeserializeObject<object>(message);

                    using (var scope = _services.CreateScope())
                    {
                        var _serviceProvider = scope.ServiceProvider.GetRequiredService<IServiceProviderService>();
                        //if (transaction.PaymentStatus == PaymentStatus.SUCCESSFUL)
                        //    await _serviceProvider.ProcessOrderAsync(transaction);
                    }

                    _channel.BasicAck(e.DeliveryTag, false);
                }
                catch (JsonException)
                {
                    _channel.BasicNack(e.DeliveryTag, false, false);
                }
                catch (AlreadyClosedException)
                {
                    //_logger.LogInformation("RabbitMQ is closed!");
                }
                catch (Exception exp)
                {
                    //_logger.LogError(default, exp, exp.Message);
                }
            };

            _channel.BasicConsume(queue: QueueName, autoAck: false, consumer: consumer);

            await Task.CompletedTask;
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            await base.StopAsync(cancellationToken);
            _connection.Close();
            //_logger.LogInformation("RabbitMQ connection is closed.");
        }
    }
}
