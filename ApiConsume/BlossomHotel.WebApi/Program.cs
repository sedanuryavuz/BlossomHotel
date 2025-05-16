using BlossomHotel.BusinessLayer.Abstract;
using BlossomHotel.BusinessLayer.Concrete;
using BlossomHotel.DataAccessLayer.Abstract;
using BlossomHotel.DataAccessLayer.Concrete;
using BlossomHotel.DataAccessLayer.EntityFramework;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<Context>();

builder.Services.AddScoped<IRoomDal, EfRoomDal>();
builder.Services.AddScoped<IRoomService, RoomManager>();

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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//Using CORS
app.UseCors("BlossomOtelCors");

app.UseAuthorization();

app.MapControllers();

app.Run();
