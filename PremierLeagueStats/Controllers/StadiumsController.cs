using Microsoft.AspNetCore.Mvc;
using PremierLeagueStats.Models;
using PremierLeagueStats.Services;
using PremierLeagueStats.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace PremierLeagueStats.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StadiumsController : ControllerBase
    {
        private readonly StadiumService _service;

        public StadiumsController(StadiumService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _service.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var stadium = await _service.Get(id);

            if (stadium == null)
                return NotFound();

            return Ok(stadium);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateStadiumDto dto)
        {
            var stadium = new Stadium
            {
                Name = dto.Name,
                Capacity = dto.Capacity,
                City = dto.City,
                BuildYear = dto.BuildYear,
                ClubId = dto.ClubId
            };

            var created = await _service.Create(stadium);

            return Ok(created);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateStadiumDto dto)
        {
            var stadium = new Stadium
            {
                Name = dto.Name,
                Capacity = dto.Capacity,
                City = dto.City,
                BuildYear = dto.BuildYear,
                ClubId = dto.ClubId
            };

            var updated = await _service.Update(id, stadium);

            if (updated == null)
                return NotFound();

            return Ok(updated);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.Delete(id);

            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}