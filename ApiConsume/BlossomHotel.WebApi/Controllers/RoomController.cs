using BlossomHotel.EntityLayer.Concrete;
using BlossomHotel.BusinessLayer.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using System.Text.Json;
using BlossomHotel.DtoLayer.Dtos.RoomDto;
using System.Net.Http;

namespace BlossomHotel.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;
        private readonly IHotelService _hotelService;
        private readonly IMapper _mapper;
        public RoomController(IRoomService roomService, IMapper mapper, IHotelService hotelService)
        {
            _roomService = roomService;
            _mapper = mapper;
            _hotelService = hotelService;

        }

        [HttpGet()]
        public IActionResult RoomList()
        {
            var values = _roomService.TGetList();
            return Ok(values);
        }
        //[HttpPost]
        //public async Task<IActionResult> AddRoom([FromForm] Room room, IFormFile CoverImage, List<IFormFile> RoomImages)
        //{
        //    if (CoverImage != null)
        //    {
        //        var fileName = Guid.NewGuid() + Path.GetExtension(CoverImage.FileName);
        //        var path = Path.Combine(Directory.GetCurrentDirectory(), fileName);
        //        using (var stream = new FileStream(path, FileMode.Create))
        //        {
        //            await CoverImage.CopyToAsync(stream);
        //        }
        //        room.RoomCoverImage = fileName;
        //    }

        //    if (RoomImages != null && RoomImages.Any())
        //    {
        //        room.RoomGallery = new List<RoomImage>();

        //        foreach (var img in RoomImages)
        //        {
        //            var imgName = Guid.NewGuid() + Path.GetExtension(img.FileName);
        //            var imgPath = Path.Combine(Directory.GetCurrentDirectory(),  imgName);
        //            using (var stream = new FileStream(imgPath, FileMode.Create))
        //            {
        //                await img.CopyToAsync(stream);
        //            }

        //            room.RoomGallery.Add(new RoomImage
        //            {
        //                ImageUrl = imgName
        //            });
        //        }
        //    }

        //    _roomService.TInsert(room);

        //    return Ok(room);
        //}
        [HttpPost]
        public async Task<IActionResult> AddRoom([FromForm] Room room, IFormFile CoverImage, List<IFormFile> RoomImages)
        {
            var imagesPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "rooms");

            if (!Directory.Exists(imagesPath))
                Directory.CreateDirectory(imagesPath);

            if (CoverImage != null)
            {
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(CoverImage.FileName)}";
                var filePath = Path.Combine(imagesPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await CoverImage.CopyToAsync(stream);
                }

                room.RoomCoverImage = fileName; 
            }

            if (RoomImages != null && RoomImages.Any())
            {
                room.RoomGallery = new List<RoomImage>();

                foreach (var img in RoomImages)
                {
                    var imgName = $"{Guid.NewGuid()}{Path.GetExtension(img.FileName)}";
                    var imgPath = Path.Combine(imagesPath, imgName);

                    using (var stream = new FileStream(imgPath, FileMode.Create))
                    {
                        await img.CopyToAsync(stream);
                    }

                    room.RoomGallery.Add(new RoomImage
                    {
                        ImageUrl = imgName 
                    });
                }
            }

            _roomService.TInsert(room);

            return Ok(room);
        }

        //[HttpPut]
        //public async Task<IActionResult> UpdateRoom([FromForm] UpdateRoomDto dto)
        //{
        //    var room = _roomService.TGetRoomWithImages(dto.RoomId); 
        //    if (room == null) return NotFound();

        //    room.RoomNumber = dto.RoomNumber;
        //    room.RoomTitle = dto.RoomTitle;
        //    room.BedCount = dto.BedCount;
        //    room.BathCount = dto.BathCount;
        //    room.Price = dto.Price;
        //    room.Description = dto.Description;
        //    room.HotelId = dto.HotelId;

        //    if (dto.CoverImage != null)
        //    {
        //        var coverFileName = Guid.NewGuid() + Path.GetExtension(dto.CoverImage.FileName);
        //        var coverPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/rooms", coverFileName);
        //        using (var stream = new FileStream(coverPath, FileMode.Create))
        //        {
        //            await dto.CoverImage.CopyToAsync(stream);
        //        }
        //        room.RoomCoverImage = "/images/rooms/" + coverFileName;
        //    }

        //    if (dto.NewRoomImages != null && dto.NewRoomImages.Any())
        //    {
        //        foreach (var image in dto.NewRoomImages)
        //        {
        //            var fileName = Guid.NewGuid() + Path.GetExtension(image.FileName);
        //            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/rooms", fileName);
        //            using (var stream = new FileStream(path, FileMode.Create))
        //            {
        //                await image.CopyToAsync(stream);
        //            }

        //            room.RoomGallery.Add(new RoomImage
        //            {
        //                ImageUrl = "/images/rooms/" + fileName,
        //                RoomId = room.RoomId
        //            });
        //        }
        //    }

        //    _roomService.TUpdate(room);
        //    return Ok();
        //}
        [HttpPut]
        public async Task<IActionResult> UpdateRoom([FromForm] UpdateRoomDto dto)
        {
            var room = _roomService.TGetRoomWithImages(dto.RoomId);
            if (room == null) return NotFound();

            room.RoomNumber = dto.RoomNumber;
            room.RoomTitle = dto.RoomTitle;
            room.BedCount = dto.BedCount;
            room.BathCount = dto.BathCount;
            room.Price = dto.Price;
            room.Description = dto.Description;
            room.HotelId = dto.HotelId;

            var imagesPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "rooms");
            if (!Directory.Exists(imagesPath))
                Directory.CreateDirectory(imagesPath);

            if (dto.CoverImage != null)
            {
                var coverFileName = Guid.NewGuid() + Path.GetExtension(dto.CoverImage.FileName);
                var coverPath = Path.Combine(imagesPath, coverFileName);

                using (var stream = new FileStream(coverPath, FileMode.Create))
                {
                    await dto.CoverImage.CopyToAsync(stream);
                }

                room.RoomCoverImage = coverFileName; 
            }

            if (dto.NewRoomImages != null && dto.NewRoomImages.Any())
            {
                foreach (var image in dto.NewRoomImages)
                {
                    var fileName = Guid.NewGuid() + Path.GetExtension(image.FileName);
                    var path = Path.Combine(imagesPath, fileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                    }

                    room.RoomGallery.Add(new RoomImage
                    {
                        ImageUrl = fileName, 
                        RoomId = room.RoomId
                    });
                }
            }

            _roomService.TUpdate(room);
            return Ok();
        }

        [HttpDelete("DeleteImageByUrl")]
        public IActionResult DeleteImageByUrl(string url)
        {
            if (string.IsNullOrEmpty(url))
                return BadRequest("URL boş olamaz.");

            if (Uri.IsWellFormedUriString(url, UriKind.Absolute))
            {
                var uri = new Uri(url);
                url = uri.AbsolutePath.TrimStart('/');
            }

            var image = _roomService.TGetRoomImageByUrl(url);
            if (image == null) return NotFound("Fotoğraf bulunamadı.");

            _roomService.TDeleteRoomImage(image);

            var physicalPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", url);
            if (System.IO.File.Exists(physicalPath))
                System.IO.File.Delete(physicalPath);

            return Ok("Fotoğraf silindi.");
        }

        [HttpDelete("DeleteCoverImage")]
        public IActionResult DeleteCoverImage(int roomId)
        {
            var room = _roomService.TGetById(roomId);
            if (room == null)
                return NotFound("Oda bulunamadı.");

            if (string.IsNullOrEmpty(room.RoomCoverImage))
                return BadRequest("Kapak resmi zaten yok.");

            var fileName = room.RoomCoverImage;

            room.RoomCoverImage = null;
            _roomService.TUpdate(room);

            var physicalPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "rooms", fileName);
            if (System.IO.File.Exists(physicalPath))
                System.IO.File.Delete(physicalPath);

            return Ok("Kapak resmi silindi.");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteRoom(int id)
        {
            var values = _roomService.TGetById(id);
            _roomService.TDelete(values);
            return Ok();
        }

        //[HttpGet("{id}")]
        //public IActionResult GetRoomById(int id)
        //{
        //    var values = _roomService.TGetById(id);
        //    return Ok(values);
        //}

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoom(int id)
        {
            var room = _roomService.TGetRoomWithImages(id); 
            if (room == null) return NotFound();

            var dto = new UpdateRoomDto
            {
                RoomId = room.RoomId,
                RoomNumber = room.RoomNumber,
                RoomTitle = room.RoomTitle,
                BedCount = room.BedCount,
                BathCount = room.BathCount,
                Description = room.Description,
                Price = room.Price,
                IsAvailable = room.IsAvailable,
                HotelId = room.HotelId,
                RoomCoverImageUrl = room.RoomCoverImage, 
                RoomImageUrls = room.RoomGallery?.Select(r => r.ImageUrl).ToList()

            };

            return Ok(dto);
        }

        [HttpGet("available")]
        public IActionResult GetAvailableRooms(int hotelId, DateTime checkin, DateTime checkout)
        {
            var rooms = _roomService.TGetAvailableRooms(hotelId, checkin, checkout);

            var jsonOptions = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles 
            };

            var json = JsonSerializer.Serialize(rooms, jsonOptions);

            return Content(json, "application/json");
        }

        [HttpGet("ByRoomId/{roomId}")]
        public async Task<IActionResult> GetImagesByRoomId(int roomId)
        {
            var room = _roomService.TGetRoomWithImages(roomId);
            if (room == null)
                return NotFound();

            var images = room.RoomGallery.Select(img => new
            {
                img.RoomImageId,
                img.ImageUrl
            }).ToList();

            return Ok(images);

        }
    }
}
