using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Text;

namespace Exam.Application.MIddlewares
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestResponseLoggingMiddleware> _logger;

        public RequestResponseLoggingMiddleware(RequestDelegate next, ILogger<RequestResponseLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            // Enable request buffering
            context.Request.EnableBuffering();

            // Log the request information
            _logger.LogInformation($"Request Method: {context.Request.Method}, Path: {context.Request.Path}");

            // Capture the request body
            using (var reader = new StreamReader(context.Request.Body, encoding: Encoding.UTF8, detectEncodingFromByteOrderMarks: false, bufferSize: 1024, leaveOpen: true))
            {
                var requestBody = await reader.ReadToEndAsync();
                _logger.LogInformation($"Request Body: {requestBody}");

                // Reset the position of the stream so it can be read again
                context.Request.Body.Position = 0;
            }

            // Call the next middleware in the pipeline
            await _next(context);

            // Enable response buffering
            var originalBodyStream = context.Response.Body;
            using (var responseBodyStream = new MemoryStream())
            {
                context.Response.Body = responseBodyStream;

                // Log the response information
                _logger.LogInformation($"Response Status Code: {context.Response.StatusCode}");

                // Capture the response body
                responseBodyStream.Position = 0;
                using (var reader = new StreamReader(responseBodyStream, encoding: Encoding.UTF8, detectEncodingFromByteOrderMarks: false, bufferSize: 1024, leaveOpen: true))
                {
                    var responseBody = await reader.ReadToEndAsync();
                    _logger.LogInformation($"Response Body: {responseBody}");

                    // Copy the response body back to the original stream
                    responseBodyStream.Position = 0;
                    await responseBodyStream.CopyToAsync(originalBodyStream);
                }
            }
        }
    }


    public static class RequestResponseLoggingMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestResponseLogging(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestResponseLoggingMiddleware>();
        }
    }

}
