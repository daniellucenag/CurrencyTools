using System;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Publisher
{
    public interface IPublisherApplication<TRequest>
    {
        Task Publish(TRequest request, Guid requestId, CancellationToken ctx);
    }
}