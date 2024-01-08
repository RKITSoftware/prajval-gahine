namespace ConsoleToWebAPIProject
{
    public class CustomeMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            await context.Response.WriteAsync("Hello  from middleware file-1 1 \n");

            await next(context);

            await context.Response.WriteAsync("Hello from middleware file-1 2 \n");
        }
    }
}
