using TopicManagementService.API.Filters;
using TopicManagementService.API.Swagger;

var builder = WebApplication.CreateBuilder(args);

var app = ConfigureServices(builder).Build();
ConfigureApp(app).Run();

WebApplicationBuilder ConfigureServices(WebApplicationBuilder builder)
{
    builder.ConfigureSerilog();
    
    builder.RegisterServicesFromReferencedAssemblies();

    builder.Services.AddRateLimiter(options =>
    {
        options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
    });

    builder.Services.AddControllers(options =>
    {
        options.Filters.Add<GlobalExceptionFilter>();
    });

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c => 
    {
        c.OperationFilter<LowercaseRouteParameterFilter>();
    });

    return builder;
}

WebApplication ConfigureApp(WebApplication app)
{
    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }

    app.UseRateLimiter();
    
    app.UseHttpsRedirection();

    app.UseRouting();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.MapControllers();

    return app;
}