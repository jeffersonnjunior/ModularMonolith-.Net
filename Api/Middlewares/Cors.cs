namespace Api.Middlewares;

public static class Cors
{
    public static IServiceCollection AddCustomCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("Policy", builder =>
            {
                builder.WithOrigins("http://localhost:5183", "https://localhost:7190")
                       .AllowAnyMethod()
                       .AllowAnyHeader()
                       .AllowCredentials();
            });
        });

        return services;
    }


    public static IApplicationBuilder UseCustomCors(this IApplicationBuilder app)
    {
        app.UseCors("Policy");
        return app;
    }
}