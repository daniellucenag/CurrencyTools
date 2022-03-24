using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace Application.Behaviors
{
    [ExcludeFromCodeCoverage]
    public class TransactionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>, IDisposable
    {
        private readonly IDbConnection Connection;
        private readonly ILogger<TransactionBehaviour<TRequest, TResponse>> logger;

        public TransactionBehaviour(IDbConnection connection, ILogger<TransactionBehaviour<TRequest, TResponse>> logger)
        {
            Connection = connection ?? throw new ArgumentNullException(nameof(connection));
            this.logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var response = default(TResponse);
            logger.LogInformation("Handling Transaction");

            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required,
                GetTransactionOptions(),
                TransactionScopeAsyncFlowOption.Enabled))
            {
                if (Connection.State == ConnectionState.Closed)
                    Connection.Open();

                response = await next();
                if (response is ResultWrapper resultWrapper &&
                    resultWrapper.Result is Result responseResult &&
                    responseResult.GetNotificationResult().Valid)
                    scope.Complete();
            }
            logger.LogInformation("Transaction Handled");
            return response;
        }
        private static TransactionOptions GetTransactionOptions() => new TransactionOptions
        {
            IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted,
            Timeout = TransactionManager.MaximumTimeout
        };

        private bool disposedValue = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Connection?.Close();
                    Connection?.Dispose();
                }
                disposedValue = true;
            }
        }
    }
}
