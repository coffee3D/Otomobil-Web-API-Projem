using Microsoft.AspNetCore.Mvc;
using NetCoreAPIMSSQL.Data.Repositories;
using NetCoreAPIMSSQL.Model;

namespace NetCoreAPIMSSQL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : Controller
    {
        private readonly ICarRepository _carRepository;

        public CarController(ICarRepository carRepository)
        {
            _carRepository = carRepository; 
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCars()
        {
            return Ok(await _carRepository.GetAllCars());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCarDetails(int id)
        {
            return Ok(await _carRepository.GetCarDetails(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateCar([FromBody] Car car)
        {
            if (car == null)
                return BadRequest();

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var created = await _carRepository.InsertCar(car);

            return Created("created",created);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCar([FromBody] Car car)
        {
            if (car == null)
                return BadRequest();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var created = await _carRepository.UpdateCar(car);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            var created = await _carRepository.DeleteCar(id);

            return NoContent();
        }

    }
}
