using InventoryAndOrderManagementAPI.Responses;
using Microsoft.AspNetCore.Diagnostics;

namespace InventoryAndOrderManagementAPI.Middlewares
{
    public class AppExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext,
                                                Exception exception, CancellationToken cancellationToken)
        {
            var statusCode = exception switch
            {
                KeyNotFoundException => StatusCodes.Status404NotFound,
                ArgumentException => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            };


            var errorResponse = new ErrorResponse
            {
                StatusCode = statusCode,
                Message = exception.Message,
                Title = "Something Went Wrong"
            };

            httpContext.Response.StatusCode = statusCode;
            httpContext.Response.ContentType = "application/json";

            await httpContext.Response.WriteAsJsonAsync(errorResponse, cancellationToken);

            return true;
        }
    }
}
