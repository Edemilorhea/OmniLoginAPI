namespace LoginAPI.MiddleWare;

public class JWTMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IServiceProvider _serviceProvider;

    public JWTMiddleware(RequestDelegate next, IServiceProvider serviceProvider)
    {
        _next = next;
        _serviceProvider = serviceProvider;
    }

    public async Task Invoke(HttpContext context)
    {
        var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

        using (var scope = _serviceProvider.CreateScope()){
            var jwtService = scope.ServiceProvider.GetRequiredService<IJWTService>();

            if (token != null)
            {
                var result = await jwtService.CheckTokenInBlackList(token);

                if (result.Data)
                {
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsync("Token is blacklisted");
                    return;
                }
            }

            await _next(context);

        }
    }
}
