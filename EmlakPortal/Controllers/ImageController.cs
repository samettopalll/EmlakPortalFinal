using AutoMapper;
using EmlakPortal.Dtos;
using EmlakPortal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace EmlakPortal.Controllers
{
    [Route("api/Images")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _hostEnvironment;
        ResultDto result = new ResultDto();
        public ImageController(AppDbContext context, IMapper mapper, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _mapper = mapper;
            _hostEnvironment = hostEnvironment;
        }
        [HttpGet]
        public List<ImageDto> GetList()
        {
            var images = _context.Images.ToList();
            var imageDto = _mapper.Map<List<ImageDto>>(images);
            return imageDto;
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            var image = _context.Images.FirstOrDefault(i => i.Id == id);
            if (image == null)
            {
                return NotFound("Belirtilen Id'ye sahip resim bulunamadı.");
            }

            var imageDto = _mapper.Map<ImageDto>(image);
            return Ok(imageDto);
        }

        [HttpGet("GetImage/{imageName}")]
        public IActionResult GetImage(string imageName)
        {
            try
            {
                string uploadsFolder = Path.Combine(_hostEnvironment.ContentRootPath, "uploads");
                string imagePath = Path.Combine(uploadsFolder, imageName);

                if (System.IO.File.Exists(imagePath))
                {
                    byte[] imageBytes = System.IO.File.ReadAllBytes(imagePath);
                    string base64String = Convert.ToBase64String(imageBytes);
                    string dataUrl = $"data:image/jpeg;base64,{base64String}";

                    return Ok(new { ImageUrl = dataUrl });
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpPost]
        public IActionResult Post([FromForm] List<IFormFile> images, int? landId, int? propId)
        {
            if (!landId.HasValue && !propId.HasValue)
            {
                return BadRequest("Lütfen bir Arsa veya Konut Id değeri sağlayın.");
            }
            Land land = null;
            Property prop = null;
            if (landId.HasValue)
            {
                land = _context.Lands.FirstOrDefault(l => l.Id == landId);
                if (land == null)
                {
                    return NotFound("Belirtilen Arsa bulunamadı.");
                }
            }
            else
            {
                prop = _context.Properties.FirstOrDefault(p => p.Id == propId);
                if (prop == null)
                {
                    return NotFound("Belirtilen Konut bulunamadı.");
                }
            }

            foreach (var imageFile in images)
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
                        LandID = landId,
                        PropertyID = propId
                    };
                    _context.Images.Add(image);
                }
            }
            _context.SaveChanges();
            return Ok("Resim başarıyla eklendi.");
        }


        [HttpDelete]
        public ResultDto Delete(int id)
        {
            var image = _context.Images.Find(id);
            if (image == null)
            {
                result.Status = false;
                result.Message = "Resim Bulunamadı!";
                return result;
            }

            var filePath = image.PhotoUrl;

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            _context.Images.Remove(image);
            _context.SaveChanges();

            result.Status = true;
            result.Message = "Resim Silindi";
            return result;
        }

    }
}
