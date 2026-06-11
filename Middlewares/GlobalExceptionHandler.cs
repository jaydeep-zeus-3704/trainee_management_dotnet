using trainee_management.Exceptions;
namespace trainee_management.Middlewares
{
    public class GlobalExceptionHandlingMiddleware
    {

        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        public GlobalExceptionHandlingMiddleware(RequestDelegate next,ILogger<GlobalExceptionHandlingMiddleware> logger)
        {
            _next=next;
            _logger=logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);    
            }
            catch (Exception ex)
            {
                await HandleException(context,ex);
                
            }
            
        }

        private async Task HandleException(HttpContext context,Exception ex)
        {
            context.Response.ContentType="application/json";
            switch (ex)
            {
                case NotFoundException:
                    context.Response.StatusCode=StatusCodes.Status404NotFound;
                    break;
                
                case ValidationException:
                    context.Response.StatusCode=StatusCodes.Status400BadRequest;
                    break;
                
                case DuplicateEmailException:
                    context.Response.StatusCode=StatusCodes.Status409Conflict;
                    break;

                case InvalidCredentialsException:
                    context.Response.StatusCode=StatusCodes.Status401Unauthorized;
                    break;

                case UpdateFailedException:
                    context.Response.StatusCode=StatusCodes.Status400BadRequest;
                    break;

                 
            }
            _logger.LogError($"\nException occured at ${context.Request.Path}\nMessage : {ex.Message}\nStatusCode:{context.Response.StatusCode}");
            await context.Response.WriteAsJsonAsync(new
            {
               message=ex.Message,
               statusCode=context.Response.StatusCode 
            });
        }
    }
}