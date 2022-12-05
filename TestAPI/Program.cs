using Microsoft.EntityFrameworkCore;
using TestAPI;
using TestAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TestContext>(options =>
{
    // options.UseSqlServer("Data Source=E5530-3MXLVL3;Initial Catalog=Test;Integrated Security=True");
    // options.UseSqlServer(builder.Configuration.GetSection("ConnectionStrings")["Database"]);
    options.UseSqlServer(builder.Configuration.GetConnectionString("Database"));
});

builder.Services.AddScoped<ColorRepository>();

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
