using egorDipl.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<SponsorsDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/auth";
    options.AccessDeniedPath = "/auth/denied";
});

builder.Services.AddRazorPages();
builder.Services.AddControllers();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Sponsors API", Version = "v1" });
});

var app = builder.Build();

// Всегда используем Swagger в Development, и можно включить в Production если нужно
if (app.Environment.IsDevelopment() || true) // Убрать "|| true" для продакшена
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sponsors API v1");
        c.RoutePrefix = "swagger"; // Установите "string.Empty" если хотите сделать Swagger главной страницей
    });
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapRazorPages();

// Перенаправление с корня на Swagger
app.MapGet("/", context =>
{
    context.Response.Redirect("/swagger");
    return Task.CompletedTask;
});

app.Run();