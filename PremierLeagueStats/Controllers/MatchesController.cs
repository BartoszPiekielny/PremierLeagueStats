using Microsoft.AspNetCore.Mvc;
using PremierLeagueStats.Models;
using PremierLeagueStats.Services;
using PremierLeagueStats.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace PremierLeagueStats.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MatchesController : ControllerBase
    {
        private readonly MatchService _service;

        public MatchesController(MatchService service)
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
            var match = await _service.Get(id);

            if (match == null)
                return NotFound();

            return Ok(match);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateMatchDto dto)
        {
            var match = new Match
            {
                Date = dto.Date,
                HomeClubId = dto.HomeClubId,
                AwayClubId = dto.AwayClubId,
                HomeScore = dto.HomeScore,
                AwayScore = dto.AwayScore
            };

            var created = await _service.Create(match);

            return Ok(created);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateMatchDto dto)
        {
            var match = new Match
            {
                Date = dto.Date,
                HomeClubId = dto.HomeClubId,
                AwayClubId = dto.AwayClubId,
                HomeScore = dto.HomeScore,
                AwayScore = dto.AwayScore
            };

            var updated = await _service.Update(id, match);

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