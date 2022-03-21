using Application.Currency;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICurrencyApplication
    {
        Task<Result<CurrencyResponseModel>> SendCurrency(CurrencyRequestModel currency, Guid requestId, CancellationToken ctc);
    }
}
