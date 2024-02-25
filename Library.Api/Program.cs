using Library.Application.Interfaces;
using Library.Application.Services;
using Library.Persistence;
using Library.Persistence.Data;
using Library.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString, b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

var assemblies = Assembly.Load("Library.Application");
builder.Services.AddAutoMapper(assemblies);
builder.Services.AddMediatR(configuration => configuration.RegisterServicesFromAssemblies(assemblies));
builder.Services.AddCors();
builder.Services.AddMemoryCache();

builder.Services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddTransient(typeof(IUnitOfWork), typeof(UnitOfWork));
builder.Services.AddTransient(typeof(IImageService), typeof(ImageService));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(c => c.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
app.UseStaticFiles();
app.UseRouting();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();
