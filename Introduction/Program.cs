using Microsoft.AspNetCore.Rewrite;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var options = new RewriteOptions()
    .AddRedirect("home[/]?$", "home/index")
    .AddRedirect("(.*)/$", "$1")
    .AddRewrite("index", "about", skipRemainingRules: false)
    .AddRewrite("login/(\\w+)/(\\d+)", "login?name=$1&id=$2", false);
app.UseRewriter(options);

app.MapGet("/", () => "Hello World!");
app.MapGet("/home", () => "Home Page");
app.MapGet("/home/index", () => "Home Index Page!");
app.MapGet("/index", () => "Index Page");
app.MapGet("/about", async (HttpContext context) => await context.Response.WriteAsync($"About: {context.Request.Path}"));
app.MapGet("/login", async (HttpContext context) =>
    await context.Response.WriteAsync($"Login Page\nName: {context.Request.Query["name"].ToString()};" +
    $" Id: {context.Request.Query["id"].ToString()}"));

app.Run();
