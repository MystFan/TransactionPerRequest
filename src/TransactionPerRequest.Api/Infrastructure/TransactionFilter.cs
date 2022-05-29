using Microsoft.AspNetCore.Mvc.Filters;
using System.Data;

namespace TransactionPerRequest.Api.Infrastructure
{
    public class TransactionFilter : IAsyncActionFilter
    {
        private readonly IDbTransaction transaction;

        public TransactionFilter(IDbTransaction transaction)
        {
            this.transaction = transaction;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var connection = transaction.Connection;
            if (connection == null || connection.State != ConnectionState.Open)
            {
                throw new NotSupportedException("Connection was not open");
            }

            ActionExecutedContext executedContext = await next.Invoke();
            if (executedContext.Exception == null)
            {
                transaction.Commit();
            }
            else
            {
                transaction.Rollback();
            }
        }
    }
}
