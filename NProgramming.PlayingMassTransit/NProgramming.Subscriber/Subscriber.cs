using System;
using MassTransit;
using MassTransit.Log4NetIntegration.Logging;
using NProgramming.Domain;

namespace NProgramming.Subscriber
{
    public class Subscriber
    {
        static void Main()
        {
            Log4NetLogger.Use("subscriber.log4net.xml");
            Console.WriteLine("This is the server");
            
            Bus.Initialize(sbc =>
            {
                sbc.UseMsmq();
                sbc.VerifyMsmqConfiguration();
                sbc.UseMulticastSubscriptionClient();
                sbc.ReceiveFrom("msmq://localhost/subscriber");

                sbc.Subscribe(subs => 
                    subs.Handler<EventMessage>(msg => Console.WriteLine(msg.ToString())));
            });

            Bus.Instance.Probe();
            Bus.Instance.WriteIntrospectionToConsole();

            Console.WriteLine("Press enter to close program ...");
            Console.ReadLine();
        }
    }
}
