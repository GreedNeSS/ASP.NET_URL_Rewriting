using Microsoft.AspNetCore.Rewrite;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

IHostEnvironment? env = app.Services.GetService<IHostEnvironment>();

if (env is not null)
{
    var options = new RewriteOptions()
        .AddIISUrlRewrite(env.ContentRootFileProvider, "Urlrewrite.xml");
    app.UseRewriter(options);
}

app.MapGet("/home/index", () => "Home Index.");
app.MapGet("/main/user", async (HttpContext context) =>
    await context.Response.WriteAsync($"Name: {context.Request.Query["name"]}; Id: {context.Request.Query["id"]}"));

app.Run();
