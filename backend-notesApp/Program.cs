using Microsoft.EntityFrameworkCore;
using backend_notesApp.Data;
using backend_notesApp.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Ensure you're using the correct DbContext and connection string
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("backend_notesAppContext")
                         ?? throw new InvalidOperationException("Connection string 'backend_notesAppContext' not found.")));

// Add services to the container
builder.Services.AddControllers();



// Add Swagger for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

