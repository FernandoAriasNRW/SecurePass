using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SecurePass.Auth.Services;
using SecurePass.Auth.User.Domain;
using SecurePass.Auth.User.Services;
using SecurePass.Folders.Domain;
using SecurePass.Folders.Services;
using SecurePass.Records.Services;
using SecurePass.Registers.Domain;
using SecurePass.Repository;
using SecurePass.Utils;
using SecurePass.Vaults.Domain;
using SecurePass.Vaults.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

string connectionString = builder.Configuration["connectionString"];
string key = builder.Configuration["jwtSecret"];

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
  {
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "Secure Pass API", Version = "v1" });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
      In = ParameterLocation.Header,
      Description = "Please enter a valid token",
      Name = "Authorization",
      Type = SecuritySchemeType.Http,
      BearerFormat = "JWT",
      Scheme = "Bearer"
    });
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
  }
);
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddScoped<IRepository<User>, Repository<User>>();
builder.Services.AddScoped<IRepository<Vault>, Repository<Vault>>();
builder.Services.AddScoped<IRepository<Folder>, Repository<Folder>>();
builder.Services.AddScoped<IRepository<Record>, Repository<Record>>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IRecordService, RecordService>();
builder.Services.AddScoped<IVaultService, VaultService>();
builder.Services.AddScoped<IUploadService, UploadService>();
builder.Services.AddScoped<IFolderService, FolderService>();
builder.Services.AddSingleton<IEncrypt, Encrypt>();

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(opt =>
{
  opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
  opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(opt =>
{
  var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

  opt.RequireHttpsMetadata = false;
  opt.SaveToken = true;

  opt.TokenValidationParameters = new TokenValidationParameters()
  {
    ValidateIssuerSigningKey = true,
    ValidateAudience = false,
    ValidateIssuer = false,
    IssuerSigningKey = signingKey,
    ValidateLifetime = true,
  };
});

builder.Services.AddDbContext<ApiDbContext>(options => options.UseNpgsql(connectionString));

builder.Services.AddCors(options =>
{
  options.AddPolicy(name: "main",
                    policy =>
                    {
                      policy.WithOrigins("http://localhost:4200", "https://e650bae6.sercure-password.pages.dev/", "https://securespass.com")
                      .AllowAnyHeader()
                      .AllowAnyMethod();
                    });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
  var context = scope.ServiceProvider.GetRequiredService<ApiDbContext>();
  context.Database.Migrate();
}

app.UseHttpsRedirection();

app.UseCors("main");

app.UseAuthentication();

app.UseAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseAuthentication();
  app.UseAuthorization();
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.MapControllers();

app.Run();