using trainee_management.Exceptions;
namespace trainee_management.Middlewares
{
    public class GlobalExceptionHandlingMiddleware
    {

        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        public GlobalExceptionHandlingMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {

                if(context.Request.Method == HttpMethods.Post || 
                context.Request.Method == HttpMethods.Put || 
                context.Request.Method == HttpMethods.Patch)
                {
                context.Request.EnableBuffering();
                using (TextReader reader = new StreamReader(
                context.Request.Body,
                encoding: System.Text.Encoding.UTF8,
                detectEncodingFromByteOrderMarks: false,
                leaveOpen: true))
                {
                    string body = await reader.ReadToEndAsync();
                    
                     if (string.IsNullOrWhiteSpace(body))
                     {
                            context.Response.StatusCode = StatusCodes.Status400BadRequest;
                            context.Response.ContentType = "application/json";
                            await context.Response.WriteAsync("{\"error\": \"The request body cannot be null or empty.\"}");
                            return; 
                      }
                    context.Request.Body.Position = 0;
                }
               }
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleException(context, ex);
            }
        }

        private async Task HandleException(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            switch (ex)
            {
                case NotFoundException:
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    break;

                case ValidationException:
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    break;

                case DuplicateEmailException:
                    context.Response.StatusCode = StatusCodes.Status409Conflict;
                    break;

                case InvalidCredentialsException:
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    break;

                case UpdateFailedException:
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    break;
                case InvalidExtensionException:
                    context.Response.StatusCode=StatusCodes.Status415UnsupportedMediaType;
                    break;
                case FileSizeException:
                    context.Response.StatusCode=StatusCodes.Status413PayloadTooLarge;
                    break;
                case ForbidenException:
                    context.Response.StatusCode=StatusCodes.Status403Forbidden;
                    break;
                default:
                    context.Response.StatusCode=StatusCodes.Status500InternalServerError;
                    break;

            }
            _logger.LogError($"\nException occured at ${context.Request.Path}\nMessage : {ex.Message}\nStatusCode:{context.Response.StatusCode}");
            await context.Response.WriteAsJsonAsync(new
            {
                message = ex.Message,
                statusCode = context.Response.StatusCode
            });
        }
    }
}