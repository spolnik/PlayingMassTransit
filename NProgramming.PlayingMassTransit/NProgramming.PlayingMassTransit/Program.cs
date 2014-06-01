using System;
using MassTransit;
using NProgramming.Domain;

namespace NProgramming.PlayingMassTransit
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Bus.Initialize(serviceBusConfigurator =>
                {
                    serviceBusConfigurator.UseMsmq(msmqConfigurator =>
                    {
                        msmqConfigurator.VerifyMsmqConfiguration();
                        msmqConfigurator.UseMulticastSubscriptionClient();
                    });
                    serviceBusConfigurator.ReceiveFrom("msmq://localhost/test_queue");
                    serviceBusConfigurator.Subscribe(subs => subs.Handler<EventMessage>(msg => Console.WriteLine(msg.ToString())));
                });
            }
            catch (Exception exception)
            {
                Console.WriteLine("Subscriber");
                Console.WriteLine(exception);
            }

            try
            {
                Bus.Instance.Publish(new EventMessage{ Value = "XC12234"});
            }
            catch (Exception exception)
            {
                Console.WriteLine("Publisher");
                Console.WriteLine(exception);
            }

            Console.WriteLine("Press enter to close program ...");
            Console.ReadLine();
        }
    }
}
