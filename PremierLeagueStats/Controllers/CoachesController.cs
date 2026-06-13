using Microsoft.AspNetCore.Mvc;
using PremierLeagueStats.Models;
using PremierLeagueStats.Services;
using PremierLeagueStats.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace PremierLeagueStats.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoachesController : ControllerBase
    {
        private readonly CoachService _service;

        public CoachesController(CoachService service)
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
            var coach = await _service.Get(id);

            if (coach == null)
                return NotFound();

            return Ok(coach);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateCoachDto dto)
        {
            var coach = new Coach
            {
                Name = dto.Name,
                Nationality = dto.Nationality,
                StartDate = dto.StartDate,
                ClubId = dto.ClubId
            };

            var created = await _service.Create(coach);

            return Ok(created);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateCoachDto dto)
        {
            var coach = new Coach
            {
                Name = dto.Name,
                Nationality = dto.Nationality,
                StartDate = dto.StartDate,
                ClubId = dto.ClubId
            };

            var updated = await _service.Update(id, coach);

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