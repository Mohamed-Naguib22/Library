using Library.Application.Dtos.BookDtos;
using Library.Application.Interfaces;
using Library.Application.Interfaces.Repositories;
using Library.Application.Services;
using Library.Domain.Models;
using Library.Infrastructure.Helpers;
using Library.Infrastructure.Services;
using Library.Persistence;
using Library.Persistence.Data;
using Library.Persistence.Repositories;
using Library.Persistence.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;
    });

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString, b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>
    (options => {
        options.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultProvider;
        options.SignIn.RequireConfirmedAccount = true;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

var assemblies = Assembly.Load("Library.Application");
builder.Services.AddAutoMapper(assemblies);
builder.Services.AddMediatR(configuration => configuration.RegisterServicesFromAssemblies(assemblies));
builder.Services.AddCors();
builder.Services.AddMemoryCache();

builder.Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));
builder.Services.Configure<Sender>(builder.Configuration.GetSection("Sender"));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidAudience = builder.Configuration["JWT:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])),
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddTransient(typeof(IUnitOfWork), typeof(UnitOfWork));
builder.Services.AddTransient(typeof(IImageService), typeof(ImageService));
builder.Services.AddTransient(typeof(IIdentityService), typeof(IdentityService));
builder.Services.AddTransient(typeof(IJwtService), typeof(JwtService));
builder.Services.AddTransient(typeof(IPaymentService), typeof(PaymentService));
builder.Services.AddTransient(typeof(IEmailSender), typeof(EmailSender));
builder.Services.AddTransient(typeof(ICacheService), typeof(MemoryCacheService));
builder.Services.AddTransient(typeof(IFilterFactory<Book, BookFilterDto>), typeof(BookFilterFactory));


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
