using Application;
using Datadog.Trace;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;
using System.Net;

namespace Api.Filters
{
    public class DefaultExceptionFilterAttribute : ExceptionFilterAttribute
	{
		private const string DEFAULT_EXCEPTION = "Ocorreu um erro inesperado.";

		public override void OnException(ExceptionContext context)
		{
			Tracer.Instance?.ActiveScope?.Span?.SetException(context.Exception);

			Log.Error(context.Exception, context.Exception.Message);
			var resultWrapper = ResultWrapper.Error("Error", DEFAULT_EXCEPTION);

			context.Result = new ObjectResult(resultWrapper.Result)
			{
				StatusCode = HttpStatusCode.InternalServerError.GetHashCode()
			};
		}
	}
}
