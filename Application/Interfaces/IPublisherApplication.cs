using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPublisherApplication<in TRequest>
    {

        Task Publish(TRequest request, CancellationToken ctx);
    }
}
