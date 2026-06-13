using Microsoft.AspNetCore.Mvc;
using PremierLeagueStats.Models;
using PremierLeagueStats.Services;
using PremierLeagueStats.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace PremierLeagueStats.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClubsController : ControllerBase
    {
        private readonly ClubService _service;

        public ClubsController(ClubService service)
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
            var club = await _service.Get(id);

            if (club == null)
                return NotFound();

            return Ok(club);
        }
        [Authorize(Roles = "Admin")]    
        [HttpPost]
       public async Task<IActionResult> Create(CreateClubDto dto)
        {
            var club = new Club
            {
                Name = dto.Name,
                City = dto.City,
                FoundedYear = dto.FoundedYear,
                Position = dto.Position,
                LogoUrl = dto.LogoUrl
            };

            var created = await _service.Create(club);

            return Ok(created);
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateClubDto dto)
        {
            var club = new Club
            {
                Name = dto.Name,
                City = dto.City,
                FoundedYear = dto.FoundedYear,
                Position = dto.Position,
                LogoUrl = dto.LogoUrl
            };

            var updated = await _service.Update(id, club);

            if (updated == null)
                return NotFound();

            return Ok(updated);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id);
            return NoContent();
        }
    }
}