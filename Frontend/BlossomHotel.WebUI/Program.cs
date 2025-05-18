using BlossomHotel.DataAccessLayer.Concrete;
using BlossomHotel.EntityLayer.Concrete;
using BlossomHotel.WebUI.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<Context>();
builder.Services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<Context>();
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient(); // Program.cs içine

//Giriþ iþlemi authorize
builder.Services.AddMvc(config =>
{
    var policy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
        config.Filters.Add(new AuthorizeFilter(policy));
});
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/AdminLogin/Index"; // Giriþ sayfasý
    options.LogoutPath = "/AdminLogin/Index"; // Çýkýþ sayfasý
    options.AccessDeniedPath = "/AdminLogin/Index"; // Yetkisiz eriþim sayfasý
    options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Oturum süresi
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/ErrorPage/Error404","?code={0}");
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthentication();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
    await IdentityInitializer.SeedAdminAsync(userManager);
}

app.Run();
