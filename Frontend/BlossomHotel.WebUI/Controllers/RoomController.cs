using BlossomHotel.BusinessLayer.Abstract;
using BlossomHotel.EntityLayer.Concrete;
using BlossomHotel.WebUI.Models.Room;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlossomHotel.WebUI.Dtos.RoomDto;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization; 
[AllowAnonymous]

public class RoomController : Controller
{

    private readonly IHttpClientFactory _httpClientFactory;
    public RoomController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    [HttpPost]
    public async Task<IActionResult> RoomList(int HotelId, DateTime Checkin, DateTime CheckOut, int AdultCount, int ChildCount)
    {
        var client = _httpClientFactory.CreateClient();

        var url = $"https://localhost:7071/api/Room/Available?hotelId={HotelId}&checkin={Checkin:yyyy-MM-dd}&checkout={CheckOut:yyyy-MM-dd}&adults={AdultCount}&children={ChildCount}";

        var rooms = await client.GetFromJsonAsync<List<ResultRoomDto>>(url);

        return View(rooms);
    }
    [HttpGet]
    public async Task<IActionResult> RoomDetails(int id)
    {
        var client = _httpClientFactory.CreateClient();

        var url = $"https://localhost:7071/api/Room/{id}";

        var room = await client.GetFromJsonAsync<ResultRoomDto>(url);

        return View(room);
    }
}

