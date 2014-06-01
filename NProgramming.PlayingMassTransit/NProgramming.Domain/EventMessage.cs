using System;

namespace NProgramming.Domain
{
    [Serializable]
    public class EventMessage
    {
        public EventMessage()
        {
            Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }
        public string Value { get; set; }

        public override string ToString()
        {
            return string.Format("Id: {0}, Value: {1}", Id, Value);
        }
    }
}