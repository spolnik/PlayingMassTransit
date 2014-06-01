using System;
using MassTransit;
using MassTransit.Context;
using NProgramming.Domain;

namespace NProgramming.EventProducer
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
                    sbc.ReceiveFrom("msmq://localhost/TestQueue.publisher");

                    sbc.UseControlBus();
                });

                // Send Message
                var message = new EventMessage { Value = "AB2312" };
                var sendContext = new SendContext<EventMessage>(message);
                sendContext.SetMessageId(Guid.NewGuid().ToString());
                sendContext.SetCorrelationId(Guid.NewGuid().ToString());
                sendContext.SetExpirationTime(DateTime.Now.AddDays(1));

                Bus.Instance.Publish(message, x => x.SetResponseAddress("msmq://localhost/TestQueue.subscriber"));

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
