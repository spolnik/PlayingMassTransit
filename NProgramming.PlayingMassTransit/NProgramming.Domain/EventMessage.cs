using System;
using Magnum;
using MassTransit;

namespace NProgramming.Domain
{
    [Serializable]
    public class EventMessage :
        CorrelatedBy<Guid>
    {
        public EventMessage()
        {
            CorrelationId = CombGuid.Generate();
        }

        public string Value { get; set; }

        public Guid CorrelationId { get; set; }

        public override string ToString()
        {
            return string.Format("CorrelationId: {0}, Value: {1}", CorrelationId, Value);
        }
    }
}