var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var app = builder.Build();

/*
app.MapGet("/", () => "Hello World!");

app.Run();
*/

app.UseHttpsRedirection();

app.UseRouting();

/*
app.MapControllerRoute(
    name: "api",
    pattern: "api/{controller=Camera}/");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
*/

app.MapControllers();

app.Run();
