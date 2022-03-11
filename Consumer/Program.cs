using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Consumer
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

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    try
                    {

//recebe safx no body                        
                        var body = ea.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);
                        Console.WriteLine(" [x] Received {0}", message);
                        //channel.BasicAck(ea.DeliveryTag, multiple: false);
//insere as tabelas temporárias
//dou um retorno pro rabbit mq dizendo que os dados estão nas temporárias
                    

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        //channel.BasicNack(ea.DeliveryTag, multiple: false, requeue: true);

                    }
                };
                channel.BasicConsume(queue: "hello",
                                     autoAck: true,
                                     consumer: consumer);

                //Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }
    }
}
