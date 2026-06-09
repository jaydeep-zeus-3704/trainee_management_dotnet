using trainee_management.Exceptions;
namespace trainee_management.Middlewares
{
    public class GlobalExceptionHandlingMiddleware
    {

        private readonly RequestDelegate _next;
        public GlobalExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next=next;
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
            }
            await context.Response.WriteAsJsonAsync(new
            {
               message=ex.Message,
               statusCode=context.Response.StatusCode 
            });
        }
    }
}