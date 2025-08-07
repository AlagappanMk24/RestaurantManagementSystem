using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Restaurant.API.Extensions;
using Restaurant.API.Middlewares;
using Restaurant.Application.Extensions;
using Restaurant.Infrastructure.Extensions;
using Restaurant.Infrastructure.Seeders;

var builder = WebApplication.CreateBuilder(args);

// ===== Services =====
builder.AddPresentation();                     // ? םÍזל Controllers + Swagger + Versioning
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// ===== Seed roles =====
using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<IRoleSeeder>();
    await seeder.Seed();
}


// ===== Middleware pipeline =====
app.UseRouting();

app.UseMiddleware<ErrorHandlingMiddleware>();
// app.UseMiddleware<RequestTimeLoggingMiddleware>();

var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    foreach (var description in provider.ApiVersionDescriptions)
    {
        options.SwaggerEndpoint(
            $"/swagger/{description.GroupName}/swagger.json",
            description.GroupName.ToUpperInvariant());
    }
});

app.UseStaticFiles();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
