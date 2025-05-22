using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

// This line allows your app to handle requests using controllers.
var builder = WebApplication.CreateBuilder(args);

// Registers controllers as services in the application.
builder.Services.AddControllers(); 

builder.Services.AddDbContext<TodoContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddDbContext<UserContext>(opt =>
    opt.UseInMemoryDatabase("UserContext"));
// Builds the application using the configuration set up in the builder.
 var app = builder.Build();

// Tells the app to use the controllers to handle incoming requests.
app.MapControllers();

// Starts the application and begins listening for incoming requests.
    app.Run();


