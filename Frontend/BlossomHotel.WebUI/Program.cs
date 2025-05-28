using BlossomHotel.BusinessLayer.Abstract;
using BlossomHotel.BusinessLayer.Concrete;
using BlossomHotel.DataAccessLayer.Abstract;
using BlossomHotel.DataAccessLayer.Concrete;
using BlossomHotel.DataAccessLayer.EntityFramework;
using BlossomHotel.EntityLayer.Concrete;
using BlossomHotel.WebUI.Dtos.ContactDto;
using BlossomHotel.WebUI.Dtos.RoomDto;
using BlossomHotel.WebUI.Identity;
using BlossomHotel.WebUI.Services;
using BlossomHotel.WebUI.ValidationRules.ContactValidationRules;
using BlossomHotel.WebUI.ValidationRules.RoomValidationRules;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<Context>();

builder.Services.AddIdentity<AppUser, AppRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
})
.AddEntityFrameworkStores<Context>()
.AddDefaultTokenProviders();

builder.Services.AddScoped<IRoomDal, EfRoomDal>();
builder.Services.AddScoped<IRoomService, RoomManager>();

builder.Services.AddScoped<IBookingDal, EfBookingDal>();
builder.Services.AddScoped<IBookingService, BookingManager>();

builder.Services.AddScoped<IFavoriteDal, EfFavoriteDal>();
builder.Services.AddScoped<IFavoriteService, FavoriteManager>();
builder.Services.AddScoped<EmailSender>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddTransient<IValidator<CreateRoomDto>, CreateRoomValidator>();
builder.Services.AddTransient<IValidator<CreateContactDto>, CreateContactValidator>();


builder.Services.AddHttpClient();
//builder.Services.AddDbContext<Context>(options =>
//{
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
//});
builder.Services.AddDbContext<Context>();
builder.Services.AddAutoMapper(typeof(Program));

// Global authorize policy
builder.Services.AddMvc(config =>
{
    var policy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
    config.Filters.Add(new AuthorizeFilter(policy));
});

// Cookie ayarlarý
builder.Services.ConfigureApplicationCookie(options =>
{
    //options.LoginPath = "/AdminLogin/Index"; 
    //options.LogoutPath = "/AdminLogin/Index";
    options.LoginPath = "/Default/Index";
    options.AccessDeniedPath = "/ErrorPage/AccessDenied";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/ErrorPage/Error404", "?code={0}");
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); 
app.UseAuthorization();  

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Seed iþlemi
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var roleManager = services.GetRequiredService<RoleManager<AppRole>>();
    var userManager = services.GetRequiredService<UserManager<AppUser>>();
    await IdentityInitializer.SeedAdminAsync(userManager, roleManager);
}

app.Run();
