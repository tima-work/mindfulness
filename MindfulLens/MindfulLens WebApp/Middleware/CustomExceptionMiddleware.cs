using Logic_Layer.Custom_exceptions;

namespace MindfulLens_WebApp.Middleware
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (BannedUserException ex)
            {
                await HandleExceptionAsync(context, ex, "/Banned");
            }
            catch (DatabaseException ex)
            {
                await HandleExceptionAsync(context, ex, "/Database-Error");
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, "/General-Error");
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception, string errorPage)
        {
            context.Response.Redirect(errorPage);
            return Task.CompletedTask;
        }
    }
}
