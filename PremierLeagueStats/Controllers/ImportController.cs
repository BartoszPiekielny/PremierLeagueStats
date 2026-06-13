using Microsoft.AspNetCore.Mvc;
using PremierLeagueStats.Services;
using PremierLeagueStats.DTOs;

namespace PremierLeagueStats.Controllers
{
    [ApiController]
    [Route("api/import")]
    public class ImportController : ControllerBase
    {
        private readonly FootballDataService _service;

        public ImportController(FootballDataService service)
        {
            _service = service;
        }

        [HttpPost("clubs")]
        public async Task<IActionResult> ImportClubs()
        {
            var imported = await _service.ImportClubs();

            return Ok(new
            {
                imported
            });
        }
        [HttpGet("team/{id}")]
        public async Task<IActionResult> TestTeam(int id)
        {
            var json = await _service.TestTeam(id);

            return Content(json, "application/json");
        }
        [HttpPost("players")]
        public async Task<IActionResult> ImportPlayers()
        {
            var imported =
                await _service.ImportPlayers();

            return Ok(new
            {
                imported
            });
        }
        [HttpPost("coaches")]
        public async Task<IActionResult> ImportCoaches()
        {
            var imported =
                await _service.ImportCoaches();

            return Ok(new
            {
                imported
            });
        }
    }
}