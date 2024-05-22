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
    [Route("api/Properties")]
    [ApiController]
    public class PropertyController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        ResultDto result = new ResultDto();
        public PropertyController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public List<PropertyDtoForGet> GetList()
        {
            var properties = _context.Properties
                .Include(p => p.Images)
                .ToList();

            var propertyDtos = new List<PropertyDtoForGet>();

            foreach (var property in properties)
            {
                var propertyDto = _mapper.Map<PropertyDtoForGet>(property);
                propertyDto.ImageUrls = property.Images.Select(i => i.PhotoUrl).ToList();
                propertyDtos.Add(propertyDto);
            }

            return propertyDtos;
        }


        [HttpGet]
        [Route("{id}")]
        public PropertyDtoForGet Get(int id)
        {
            var property = _context.Properties
                .Include(p => p.Images)
                .FirstOrDefault(p => p.Id == id);

            if (property == null)
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                return null;
            }

            var propertyDto = _mapper.Map<PropertyDtoForGet>(property);
            propertyDto.ImageUrls = property.Images.Select(i => i.PhotoUrl).ToList();
            return propertyDto;
        }

        [HttpGet("UsersProperties")]
        public async Task<ActionResult<IEnumerable<PropertyDto>>> GetUserProperties()
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var userProperties = await _context.Properties
                .Where(property => property.AppUserId == userId)
                .ToListAsync();

            var userPropertyDtos = _mapper.Map<List<PropertyDto>>(userProperties);

            return userPropertyDtos;
        }

        [HttpPost]
        [Authorize]
        public ResultDto Post([FromForm] PropertyImageDto propertyImageDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var propertyDto = propertyImageDto.Property;
            var property = _mapper.Map<Property>(propertyDto);
            property.AppUserId = userId;
            property.Created = DateTime.Now;
            property.Updated = DateTime.Now;
            _context.Properties.Add(property);
            _context.SaveChanges();

            foreach (var imageFile in propertyImageDto.Images)
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
                        PropertyID = property.Id
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
        public ResultDto Put([FromBody] PropertyDto propertyDto)
        {
            var property = _context.Properties.FirstOrDefault(p => p.Id == propertyDto.Id);
            if (property == null)
            {
                result.Status = false;
                result.Message = "İlan Bulunamadı!";
                return result;
            }
            property.Title = propertyDto.Title;
            property.Description = propertyDto.Description;
            property.Type = propertyDto.Type;
            property.Price = propertyDto.Price;
            property.RoomCount = propertyDto.RoomCount;
            property.SquareMeters = propertyDto.SquareMeters;
            property.Location = propertyDto.Location;
            property.PropertyAge = propertyDto.PropertyAge;
            property.FloorCount = propertyDto.FloorCount;
            property.PropertysFloor = propertyDto.PropertysFloor;
            property.IsSold = propertyDto.IsSold;
            property.Updated = DateTime.Now;
            property.CategoryID = propertyDto.CategoryID;
            _context.Properties.Update(property);
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
            var property = _context.Properties
                                   .Include(p => p.Images)
                                   .FirstOrDefault(p => p.Id == id);

            if (property == null)
            {
                result.Status = false;
                result.Message = "İlan Bulunamadı!";
                return result;
            }
            foreach (var image in property.Images)
            {
                if (System.IO.File.Exists(image.PhotoUrl))
                {
                    System.IO.File.Delete(image.PhotoUrl);
                }
                _context.Images.Remove(image);
            }

            _context.Properties.Remove(property);
            _context.SaveChanges();

            result.Status = true;
            result.Message = "İlan Başarıyla Silindi.";
            return result;
        }

    }
}
