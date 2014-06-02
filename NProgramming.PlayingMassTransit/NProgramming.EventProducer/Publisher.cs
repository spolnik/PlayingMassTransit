using System;
using MassTransit;
using MassTransit.Log4NetIntegration.Logging;
using MassTransit.Services.Routing.Configuration;
using NProgramming.Domain;

namespace NProgramming.EventProducer
{
    public class Publisher
    {
        static void Main()
        {
            Log4NetLogger.Use("publisher.log4net.xml");
            Console.WriteLine("This is the client");
            
            Bus.Initialize(sbc =>
            {
                sbc.UseMsmq();
                sbc.VerifyMsmqConfiguration();
                sbc.UseMulticastSubscriptionClient();
                sbc.ReceiveFrom("msmq://localhost/publisher");

                sbc.ConfigureService<RoutingConfigurator>(
                    BusServiceLayer.Session,
                    rc => rc.Route<EventMessage>().To("msmq://localhost/subscriber")
                    );
            });

            Bus.Instance.Probe();
            Bus.Instance.WriteIntrospectionToConsole();

            // Send Message
            var message = new EventMessage {Value = "AB2312"};

            Bus.Instance.Publish(message);

            Console.ReadLine();
        }
    }
}
