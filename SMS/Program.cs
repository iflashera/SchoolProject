using API;
using DataContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models.Models.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDependencies();

builder.Services.AddDbContext<SmsDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(x =>
{
    x.Password.RequireDigit = true;
    x.Password.RequiredLength = 8;
    x.Password.RequireUppercase = true;
    x.Password.RequireLowercase = true;
}).AddEntityFrameworkStores<SmsDbContext>().AddDefaultTokenProviders();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var context = services.GetRequiredService<SmsDbContext>();
var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
var roleManager = services.GetRequiredService<RoleManager<ApplicationRole>>();
DataInitializer.SeedData(userManager, roleManager, context);
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
