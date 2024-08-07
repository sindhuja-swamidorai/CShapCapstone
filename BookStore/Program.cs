using BookStore.Controllers;
using BookStore.DbContexts;
using BookStore.Services;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionsString = "Server=WSAMZN-4I5F00IJ;Initial Catalog=book_store;User ID=sa;Password=Password@123; TrustServerCertificate=True";

/*
builder.Services.AddDbContext<BookStoreContext>(
    DbContextOptions => DbContextOptions.UseSqlServer(
        builder.Configuration["ConnectionStrings:BookStoreDBConnectionString"]));
*/

builder.Services.AddDbContext<BookStoreContext>(
    DbContextOptions => DbContextOptions.UseSqlServer(connectionsString));


builder.Services.AddScoped<IBookStoreRepository, BookStoreRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
