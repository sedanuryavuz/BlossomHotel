using BlossomHotel.BusinessLayer.Abstract;
using BlossomHotel.BusinessLayer.Concrete;
using BlossomHotel.DataAccessLayer.Abstract;
using BlossomHotel.DataAccessLayer.Concrete;
using BlossomHotel.DataAccessLayer.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<Context>();
builder.Services.AddScoped<IRoomDal, EfRoomDal>();
builder.Services.AddScoped<IRoomService, RoomManager>();

builder.Services.AddScoped<IHotelDal, EfHotelDal>();
builder.Services.AddScoped<IHotelService, HotelManager>();

builder.Services.AddScoped<IBookingDal, EfBookingDal>();
builder.Services.AddScoped<IBookingService, BookingManager>();

builder.Services.AddScoped<IStaffDal, EfStaffDal>();
builder.Services.AddScoped<IStaffService, StaffManager>();

builder.Services.AddScoped<ITestimonialDal, EfTestimonialDal>();
builder.Services.AddScoped<ITestimonialService, TestimonialManager>();

builder.Services.AddScoped<IServicesDal, EfServiceDal>();
builder.Services.AddScoped<IServiceService, ServiceManager>();

builder.Services.AddScoped<IAboutDal, EfAboutDal>();
builder.Services.AddScoped<IAboutService, AboutManager>();

builder.Services.AddScoped<IContactDal, EfContactDal>();
builder.Services.AddScoped<IContactService, ContactManager>();

builder.Services.AddScoped<IFavoriteDal, EfFavoriteDal>();
builder.Services.AddScoped<IFavoriteService, FavoriteManager>();

builder.Services.AddScoped<IAppUserDal, EfAppUserDal>(); 
builder.Services.AddScoped<IAppUserService, AppUserManager>();

//AutoMapper
builder.Services.AddAutoMapper(typeof(Program));


//ApiConsume
builder.Services.AddCors(options =>
{
    options.AddPolicy("BlossomOtelCors", opts =>
        { opts.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
        });
});


var app = builder.Build();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")),
    RequestPath = ""
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// API Program.cs veya Startup.cs içine:

app.UseStaticFiles();
app.UseRouting();
app.UseHttpsRedirection();

//Using CORS
app.UseCors("BlossomOtelCors");

app.UseAuthorization();

app.MapControllers();

app.Run();
