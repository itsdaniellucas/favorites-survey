using FavoritesSurvey.Core.Architecture.BLL;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FavoritesSurvey.BLL.Misc
{
    public class Queue : IQueue, IDisposable
    {
        IConnectionFactory _factory;
        IConnection _conn;
        IModel _channel;

        private readonly string _appId = "fs.producer";
        private readonly string _exchangeName = "fs";
        private readonly string _queueName = "compute";
        private readonly string _routingKey = "fs.compute";
        private readonly string _exchangeType = "direct";

        AsyncEventingBasicConsumer _consumer;


        public Queue(IConnectionFactory factory)
        {
            _factory = factory;
            _conn = _factory.CreateConnection();
            _channel = _conn.CreateModel();
            _channel.ExchangeDeclare(exchange: _exchangeName, type: _exchangeType, durable: true, autoDelete: false);
            _channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false);
            _channel.QueueBind(queue: _queueName, exchange: _exchangeName, routingKey: _routingKey);
            _consumer = new AsyncEventingBasicConsumer(_channel);
        }

        public void RegisterConsumer<T>(Func<T, Task> action)
        {
            _consumer.Received += async (sender, eventArgs) =>
            {
                var bodyBytes = eventArgs.Body.ToArray();
                var bodyString = Encoding.UTF8.GetString(bodyBytes);
                var message = JsonConvert.DeserializeObject<T>(bodyString);
                await action(message);
                _channel.BasicAck(eventArgs.DeliveryTag, false);
            };
            _channel.BasicConsume(_queueName, false, _consumer);
        }

        public void Publish<T>(T message)
        {
            var serialized = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(serialized);
            var props = _channel.CreateBasicProperties();
            props.AppId = _appId;
            props.ContentType = "application/json";
            props.DeliveryMode = 2;
            props.Timestamp = new AmqpTimestamp((long)DateTime.Now.ToUnixTimestamp());
            _channel.BasicPublish(exchange: _exchangeName, routingKey: _routingKey, body: body, basicProperties: props);
        }

        public void Dispose()
        {
            try
            {
                _channel?.Close();
                _channel?.Dispose();
                _channel = null;

                _conn?.Close();
                _conn?.Dispose();
                _conn = null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }

    public class QueueMessage
    {
        public string Guid { get; set; }
        public string Action { get; set; } = "Compute";
        public DateTime Timestamp { get; set; }
    };

    public interface IQueue : IDisposable
    {
        void Publish<T>(T message);
        void RegisterConsumer<T>(Func<T, Task> action);
    }
}
