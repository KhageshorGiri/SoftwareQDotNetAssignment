using BookService.API.ServiceConfigurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Add services to the container.
builder.RegisterDbConfiguration();
builder.ServicesPilelineConfiguration();

var app = builder.Build();
// Configure the HTTP request / Middleware pipeline.
app.MiddleWarePipelIne();
app.Run();
