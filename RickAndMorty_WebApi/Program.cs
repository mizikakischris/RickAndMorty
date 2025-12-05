using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using RickAndMorty.Application;
using RickAndMorty.Infrastructure;
using RickAndMorty.Infrastructure.Persistence;
using RickAndMorty.WebApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(options => {
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1.0",
        Title = "Rick and Morty V1",
        Description = "API to manage Chaacters",
        Contact = new OpenApiContact
        {
            Name = "Brainbay",
            Url = new Uri("https://brainbay.nl/en/")
        },
    });
});

// Layer registrations
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddWebApiServices(builder.Configuration);

// Connection string from appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Register DbContext
builder.Services.AddDbContext<RickAndMortyDbContext>(options =>
    options.UseSqlServer(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options => {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "RickAndMorty1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
