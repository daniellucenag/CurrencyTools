using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Microsoft.IO;
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Api.Middlewares
{
    [ExcludeFromCodeCoverage]
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger logger;
        private readonly RecyclableMemoryStreamManager recyclableMemoryStreamManager;

        /// <summary>
        /// Class do RequestResponseLoggingMiddleware
        /// </summary>
        /// <param name="next"></param>
        /// <param name="loggerFactory"></param>
        public RequestResponseLoggingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            logger = loggerFactory.CreateLogger<RequestResponseLoggingMiddleware>();
            this.next = next;
            recyclableMemoryStreamManager = new RecyclableMemoryStreamManager();
        }

        /// <summary>
        /// Function Invoke
        /// </summary>
        /// <param name="context">HttpContext</param>
        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path.StartsWithSegments(new PathString("/RS.PaymentProvider/api")))
            {
                await LogRequest(context);
                await LogResponse(context);
            }
            else
            {
                await next(context);
            }
        }

        private async Task LogRequest(HttpContext context)
        {
            context.Request.EnableBuffering();
            await using var requestStream = recyclableMemoryStreamManager.GetStream();
            await context.Request.Body.CopyToAsync(requestStream);

            string verb = context.Request.Method;
            Guid transactionId = GetTransactionId(context);
            string uri = GetUri(context.Request);
            string requestBody = ReadStreamInChunks(requestStream);
            JsonSerializerOptions options = new System.Text.Json.JsonSerializerOptions();
            options.Converters.Add(new DynamicJsonConverter());
            logger.LogInformation(JsonSerializer.Serialize(new
            {
                Date = DateTime.Now,
                TransactionId = transactionId,
                Uri = uri,
                Verb = verb,
                RequestPayload = string.IsNullOrWhiteSpace(requestBody) ?
                    new object() : JsonSerializer.Deserialize<dynamic>(requestBody, options)
            }));
            context.Request.Body.Position = 0;
        }

        private async Task LogResponse(HttpContext context)
        {
            var originalBodyStream = context.Response.Body;

            await using var responseBodyStream = recyclableMemoryStreamManager.GetStream();
            context.Response.Body = responseBodyStream;

            await next(context);

            context.Response.Body.Seek(0, SeekOrigin.Begin);
            string responseBody = await new StreamReader(context.Response.Body).ReadToEndAsync();
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.Converters.Add(new DynamicJsonConverter());
            string logContent = GetLogContent(context, responseBody, options);

            if (context.Response.StatusCode / 100 == 2)
                logger.LogInformation(logContent);
            else if (context.Response.StatusCode / 100 == 4)
                logger.LogWarning(logContent);
            else
                logger.LogError(logContent);
            await responseBodyStream.CopyToAsync(originalBodyStream);
        }

        private Guid GetTransactionId(HttpContext context)
        {
            Guid transaction = Guid.Empty;
            if (context.Request.Headers.TryGetValue("X-Request-ID", out StringValues values)
                && values.First() != null)
                Guid.TryParse(values.First(), out transaction);

            return transaction;
        }

        private string GetUri(HttpRequest request)
        {
            var builder = new UriBuilder
            {
                Scheme = request.Scheme,
                Host = request.Host.Host,
                Port = request.Host.Port ?? 0,
                Path = request.Path,
                Query = request.QueryString.ToUriComponent()
            };
            return builder.Uri.ToString();
        }

        private static string ReadStreamInChunks(Stream stream)
        {
            const int readChunkBufferLength = 4096;

            stream.Seek(0, SeekOrigin.Begin);

            using var textWriter = new StringWriter();
            using var reader = new StreamReader(stream);

            var readChunk = new char[readChunkBufferLength];
            int readChunkLength;

            do
            {
                readChunkLength = reader.ReadBlock(readChunk, 0, readChunkBufferLength);
                textWriter.Write(readChunk, 0, readChunkLength);
            } while (readChunkLength > 0);

            return textWriter.ToString();
        }

        private string GetLogContent(HttpContext context, string responseBody, JsonSerializerOptions options)
        {
            var contentType = context.Response.ContentType ?? string.Empty;
            if (contentType.Contains("application/json"))
                return JsonSerializer.Serialize(new
                {
                    Date = DateTime.Now,
                    TransactionId = GetTransactionId(context),
                    ResponseStatus = context.Response.StatusCode,
                    ResponseBody = string.IsNullOrWhiteSpace(responseBody) ?
                        new object() :
                        JsonSerializer.Deserialize<dynamic>(responseBody, options)
                });


            return JsonSerializer.Serialize(new
            {
                Date = DateTime.Now,
                TransactionId = GetTransactionId(context),
                ResponseStatus = context.Response.StatusCode,
                ResponseBody = string.IsNullOrWhiteSpace(responseBody) ? new object() : responseBody

            });
        }

    }

    /// <summary>
    /// RequestResponseLoggingMiddlewareExtensions
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class RequestResponseLoggingMiddlewareExtensions
    {

        /// <summary>
        /// Use Request Response for Logging
        /// </summary>
        /// <param name="builder">IApplicationBuilder</param>
        public static IApplicationBuilder UseRequestResponseLogging(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestResponseLoggingMiddleware>();
        }
    }
}
