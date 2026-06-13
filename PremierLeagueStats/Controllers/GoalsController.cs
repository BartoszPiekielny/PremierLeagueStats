using Microsoft.AspNetCore.Mvc;
using PremierLeagueStats.Models;
using PremierLeagueStats.Services;
using PremierLeagueStats.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace PremierLeagueStats.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GoalsController : ControllerBase
    {
        private readonly GoalService _service;

        public GoalsController(GoalService service)
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
            var goal = await _service.Get(id);

            if (goal == null)
                return NotFound();

            return Ok(goal);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateGoalDto dto)
        {
            var goal = new Goal
            {
                Minute = dto.Minute,
                PlayerId = dto.PlayerId,
                MatchId = dto.MatchId
            };

            var created = await _service.Create(goal);

            return Ok(created);
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateGoalDto dto)
        {
            var goal = new Goal
            {
                Minute = dto.Minute,
                PlayerId = dto.PlayerId,
                MatchId = dto.MatchId
            };

            var updated = await _service.Update(id, goal);

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