using AutoMapper;
using DataAccess.Contexts;
using DataAccess.Models;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.Extensions;
using SocialNetwork.Mappers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnectionString");
builder.Services
    .AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString))
    .AddUnitOfWork()
    .AddCustomRepository<Friend, FriendRepository>()
    .AddIdentity<User, IdentityRole>(options =>
        {
            options.Password.RequiredLength = 4;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
        })
    .AddEntityFrameworkStores<ApplicationDbContext>();

var mapperConfig = new MapperConfiguration(v =>
v.AddProfile(new MapperProfile()));

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddScoped<IMapper>(provider => 
    new Mapper(mapperConfig, provider.GetService));

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
