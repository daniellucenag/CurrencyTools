using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPublisherApplication<TMessage> where TMessage : class
    {
        Task Publish(TMessage request, Guid requestId, CancellationToken ctx);
    }
}
