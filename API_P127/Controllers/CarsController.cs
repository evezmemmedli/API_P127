using API_P127.DAL;
using API_P127.DTOs;
using API_P127.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_P127.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly APIDbContext _context;
 
        public CarsController(APIDbContext context)
        {
            _context = context;
        }
        //[HttpGet]
        //[Route("create")]
        //public IActionResult CreateCars()
        //{
        //    _context.Cars.AddRange(_cars);
        //    _context.SaveChanges();
        //    return Ok();
        //}

        [HttpGet]
        [Route("get/{id}")]
        public IActionResult Get(int id)
        {
            Car car = _context.Cars.FirstOrDefault(c => c.Id == id);
            CarGetDto dto = new CarGetDto()
            {
                Id = car.Id,
                Brand = car.Brand,
                Model = car.Model,
                Price = car.Price,
                Color = car.Color,
                Display = car.Display

            };
            if (car is null) return StatusCode(404);
            return Ok(dto);
        }
        [HttpGet]
        [Route("getall")]
        public IActionResult GetAll(int page =1,string search=null) 
        {
            var query = _context.Cars.AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(c => c.Brand.Contains(search));
            }
            CarListDto dto = new CarListDto()
            {
                carListItemDtos = query.Select(c => new CarListItemDto { Id = c.Id, Brand = c.Brand, Model = c.Model, Color = c.Color, Price = c.Price })
                .Skip((page - 1) * 4)
                .Take(4)
                .ToList(),
                TotalCount = query.Count()
            };
            return Ok(dto);
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create(CarPostDto carDto)
        {
            if (carDto == null) return NotFound();
            Car car = new Car
            {
                Brand=carDto.Brand,
                Model=carDto.Model,
                Color=carDto.Color,
                Price=carDto.Price,
                Display=carDto.Display,
            };
            await _context.Cars.AddAsync(car);
            await _context.SaveChangesAsync();
            return StatusCode(201, new {id = car.Id, car=carDto});
        }

        [HttpPut("update/{id}")]
        public IActionResult Update(int id,CarPostDto carDto)
        {
            if(id==0) return BadRequest();
            Car existed = _context.Cars.FirstOrDefault(c=>c.Id==id);
            _context.Entry(existed).CurrentValues.SetValues(carDto);
            if(existed is null) return NotFound();
            //existed.Brand = carDto.Brand;
            //existed.Model = carDto.Model;
            //existed.Price = carDto.Price;
            //existed.Color = carDto.Color;
            _context.SaveChanges();

            return StatusCode(200, carDto);
        }
        [HttpDelete("delete/{id}")]
        public IActionResult Delete (int id)
        {
            if(id==0) return BadRequest();
            Car existed = _context.Cars.FirstOrDefault(c=>c.Id == id);
            if (existed is null) return NotFound();
            _context.Cars.Remove(existed);
            _context.SaveChanges();
            return NoContent();
        }
        [HttpPatch("change/{id}")]
        public IActionResult ChangeDisplay(int id,bool display)
        {
            if (id==0) return BadRequest();
            Car existed = _context.Cars.FirstOrDefault(c => c.Id == id);
            if (existed is null) return NotFound();
            existed.Display = display;
            _context.SaveChanges();
            return NoContent();


        }

    
    }
}
