using System;
using System.Text;
using RabbitMQ.Client;

namespace Producer
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "hello",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);
                int count = 0;
                while (true)
                {
                    string message = $"{count++} - Testando uma estrutura Producer x Consumer!";
                    var body = Encoding.UTF8.GetBytes(message);
//busca dados do cosmos
//publica as mensagens na fila do rabbitMQ
                    channel.BasicPublish(exchange: "",
                                         routingKey: "hello",
                                         basicProperties: null,
                                         body: body);
                                         //body = IEnumerable<SAFX>
                    Console.WriteLine(" [x] Sent {0}", message);
//update no lote do cosmos                    
                    System.Threading.Thread.Sleep(400);
                }
            }
        }

    }
}
