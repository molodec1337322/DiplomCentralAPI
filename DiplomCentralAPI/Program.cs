var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

/*
app.MapGet("/", () => "Hello World!");

app.Run();
*/

app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

app.Run();
