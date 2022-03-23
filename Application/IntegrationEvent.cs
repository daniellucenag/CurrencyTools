using System;

namespace Application
{
    public class IntegrationEvent<T> where T : struct
    {

        public IntegrationEvent(T data)
        {
            Data = data;
        }

        public T Data { get; }
        public DateTimeOffset CreatedAt => DateTime.Now;
        public Guid Id => Guid.NewGuid();
    }
}
