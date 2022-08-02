using API_P127.DAL;
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
        //private List<Car> _cars = new List<Car>()
        //{
        //     new Car
        //     {
        //      Brand = "Mercedes",
        //      Model = "S-500",
        //      Price = 130000,
        //      Color = "white"
        //     },
        //     new Car
        //     {
        //      Brand = "BMW",
        //      Model = "F-90",
        //      Price = 180000,
        //      Color = "gray"
        //     },
        //      new Car
        //      {
        //      Brand = "Bugatti",
        //      Model = "Chiron",
        //      Price = 3000000,
        //      Color = "royal-blue"
        //      },
        //       new Car
        //       {
        //      Brand = "Pagani",
        //      Model = "Huayra",
        //      Price = 1300000,
        //      Color = "black"
        //       }

        //};
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
            if (car is null) return StatusCode(404);
            return Ok(car);
        }
        [HttpGet]
        [Route("getall")]
        public IActionResult GetAll()
        {
            return Ok(_context.Cars.ToList());
        }

        [HttpPost]
        [Route("create")]
        public IActionResult Create(Car car)
        {
            if(car == null) return NotFound();
            _context.Cars.Add(car);
            _context.SaveChanges();
            return StatusCode(201, car);
        }

    }
}
