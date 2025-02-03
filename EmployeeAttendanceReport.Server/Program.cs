using System;
using EmployeeAttendanceReport.Server.Common;
using EmployeeAttendanceReport.Server.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<LocalDbContext>(options => options.UseInMemoryDatabase("EmpAttnReportDB"));
builder.Services.AddControllers();
builder.Services.AddScoped<IRepository, Repository>();
// Add CORS services
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularDevClient", // Policy name
        policy =>
        {
            policy.WithOrigins("https://localhost:61256") // Allow requests from this origin
                   .AllowAnyMethod() // Allow any HTTP method (GET, POST, PUT, DELETE, etc.)
                   .AllowAnyHeader(); // Allow any headers                  
        });
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<LocalDbContext>();
    dbContext.Database.EnsureDeleted(); // Delete database on startup
    dbContext.Database.EnsureCreated(); // Recreate database on startup
}

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAngularDevClient"); 
app.UseAuthorization();
app.MapControllers();
app.MapFallbackToFile("/index.html");
app.Run();
