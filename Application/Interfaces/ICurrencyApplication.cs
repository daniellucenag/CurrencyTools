using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICurrencyApplication
    {
        Task<ResultWrapper> SendCurrency(CurrencyRequestModel currency, Guid requestId, CancellationToken ctx);
    }
}
