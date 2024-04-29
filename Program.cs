using Microsoft.EntityFrameworkCore;
using SecurePass.Auth.Services;
using SecurePass.Auth.User.Domain;
using SecurePass.Auth.User.Services;
using SecurePass.Registers.Domain;
using SecurePass.Repository;
using SecurePass.Utils;
using SecurePass.Vaults.Domain;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration["connectionString"];

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddScoped<IRepository<UserEntity>, Repository<UserEntity>>();
builder.Services.AddScoped<IRepository<VaultEntity>, Repository<VaultEntity>>();
builder.Services.AddScoped<IRepository<RecordEntity>, Repository<RecordEntity>>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddSingleton<IEncrypt, Encrypt>();

builder.Services.AddDbContext<ApiDbContext>(options => options.UseNpgsql(connectionString));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
  var context = scope.ServiceProvider.GetRequiredService<ApiDbContext>();
  context.Database.Migrate();
}

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