using System;
using MassTransit;
using MassTransit.Context;
using NProgramming.Domain;

namespace NProgramming.Subscriber
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Bus.Initialize(sbc =>
                {
                    sbc.UseMsmq(msmqConfigurator => msmqConfigurator.UseSubscriptionService("msmq://localhost/mt_subscriptions"));
                    sbc.ReceiveFrom("msmq://localhost/TestQueue.subscriber");

                    sbc.UseControlBus();

                    sbc.Subscribe(subs => subs.Handler<EventMessage>(msg => Console.WriteLine(msg.ToString())));
                });
            }
            catch (Exception exception)
            {
                Console.WriteLine("Subscriber");
                Console.WriteLine(exception);
            }
            
            Console.WriteLine("Press enter to close program ...");
            Console.ReadLine();
        }
    }
}
