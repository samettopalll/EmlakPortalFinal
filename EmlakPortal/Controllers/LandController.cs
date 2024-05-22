using AutoMapper;
using EmlakPortal.Dtos;
using EmlakPortal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Security.Claims;

namespace EmlakPortal.Controllers
{
    [Route("api/Lands")]
    [ApiController]
    public class LandController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        ResultDto result = new ResultDto();
        public LandController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public List<LandDtoForGet> GetList()
        {
            var lands = _context.Lands
                .Include(l => l.Images)
                .ToList();

            var landDtos = new List<LandDtoForGet>();

            foreach (var land in lands)
            {
                var landDto = _mapper.Map<LandDtoForGet>(land);
                landDto.ImageUrls = land.Images.Select(i => i.PhotoUrl).ToList();
                landDtos.Add(landDto);
            }

            return landDtos;
        }

        [HttpGet]
        [Route("{id}")]
        public LandDtoForGet Get(int id)
        {
            var land = _context.Lands
                .Include(l => l.Images)
                .FirstOrDefault(l => l.Id == id);

            if (land == null)
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                return null;
            }
            var landDto = _mapper.Map<LandDtoForGet>(land);
            landDto.ImageUrls = land.Images.Select(i => i.PhotoUrl).ToList();
            return landDto;
        }

        [HttpGet("UsersLands")]
        public async Task<ActionResult<IEnumerable<LandDto>>> GetUserLands()
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var userLands = await _context.Lands
                .Where(land => land.AppUserId == userId)
                .ToListAsync();

            var userLandDtos = _mapper.Map<List<LandDto>>(userLands);

            return userLandDtos;
        }

        [HttpPost]
        [Authorize]
        public ResultDto Post([FromForm] LandImageDto landImageDto)
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var landDto = landImageDto.Land;
            var land = _mapper.Map<Land>(landDto);
            land.AppUserId = userId;
            land.Created = DateTime.Now;
            land.Updated = DateTime.Now;
            _context.Lands.Add(land);
            _context.SaveChanges();

            foreach (var imageFile in landImageDto.Images)
            {
                if (imageFile != null && imageFile.Length > 0)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                    var filePath = Path.Combine("uploads", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        imageFile.CopyTo(stream);
                    }
                    var image = new Image
                    {
                        PhotoUrl = filePath,
                        LandID = land.Id
                    };
                    _context.Images.Add(image);
                }
            }

            _context.SaveChanges();
            result.Status = true;
            result.Message = "İlan Eklendi";
            return result;
        }

        [HttpPut]
        [Authorize]
        public ResultDto Put([FromBody] LandDto landDto)
        {
            var land = _context.Lands.FirstOrDefault(p => p.Id == landDto.Id);
            if (land == null)
            {
                result.Status = false;
                result.Message = "İlan Bulunamadı!";
                return result;
            }
            land.Title = landDto.Title;
            land.Description = landDto.Description;
            land.Type = landDto.Type;
            land.Price = landDto.Price;
            land.SquareMeters = landDto.SquareMeters;
            land.Location = landDto.Location;
            land.BlockNumber = landDto.BlockNumber;
            land.ParcelNumber = landDto.ParcelNumber;
            land.IsSold = landDto.IsSold;
            land.Updated = DateTime.Now;
            land.CategoryID = landDto.CategoryID;
            _context.Lands.Update(land);
            _context.SaveChanges();
            result.Status = true;
            result.Message = "İlan Düzenlendi";
            return result;
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize]
        public ResultDto Delete(int id)
        {
            var land = _context.Lands
                               .Include(l => l.Images)
                               .FirstOrDefault(l => l.Id == id);

            if (land == null)
            {
                result.Status = false;
                result.Message = "İlan Bulunamadı!";
                return result;
            }
            foreach (var image in land.Images)
            {
                if (System.IO.File.Exists(image.PhotoUrl))
                {
                    System.IO.File.Delete(image.PhotoUrl);
                }
                _context.Images.Remove(image);
            }

            _context.Lands.Remove(land);
            _context.SaveChanges();

            result.Status = true;
            result.Message = "İlan Başarıyla Silindi.";
            return result;
        }
    }
}
