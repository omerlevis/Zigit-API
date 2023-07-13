using Microsoft.EntityFrameworkCore;
using MySql.EntityFrameworkCore.Extensions;
using Microsoft.Extensions.DependencyInjection;
using ZigitApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEntityFrameworkMySQL()
                .AddDbContext<DBContext>(options =>
                {
                    options.UseMySQL(builder.Configuration.GetConnectionString("Default"));
                });
builder.Services.AddControllers();
// Enable CORS, in our case for sendig reqeset from localhost to local host
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors();

app.MapControllers();

app.Run();
