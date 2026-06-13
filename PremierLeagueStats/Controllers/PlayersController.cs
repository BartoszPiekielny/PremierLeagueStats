using Microsoft.AspNetCore.Mvc;
using PremierLeagueStats.Models;
using PremierLeagueStats.Services;
using PremierLeagueStats.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace PremierLeagueStats.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayersController : ControllerBase
    {
        private readonly PlayerService _service;
        private readonly IMapper _mapper;

        public PlayersController(PlayerService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _service.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var player = await _service.Get(id);

            if (player == null)
                return NotFound();

            var playerDto = _mapper.Map<PlayerDto>(player);
            
            return Ok(playerDto);
        }
        [HttpGet("position/{position}")]
        public async Task<ActionResult<List<Player>>> GetByPosition(string position)
        {
            return await _service.GetByPosition(position);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(CreatePlayerDto dto)
        {
            var player = _mapper.Map<Player>(dto);
            var created = await _service.Create(player);
            return Ok(created);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdatePlayerDto dto)
        {
            var player = _mapper.Map<Player>(dto);
            var updated = await _service.Update(id, player);

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
        [HttpGet("search")]
        public async Task<IActionResult> Search(string name)
        {
            return Ok(await _service.Search(name));
        }
        [HttpGet("nationality/{country}")]
        public async Task<IActionResult> GetByNationality(string country)
        {
            var players = await _service.GetByNationality(country);

            return Ok(players);
        }
        [HttpGet("club/{clubId}")]
        public async Task<IActionResult> GetByClub(int clubId)
        {
            var players = await _service.GetByClub(clubId);

            return Ok(players);
        }
        [HttpGet("clubname/{clubName}")]
        public async Task<IActionResult> GetByClubName(string clubName)
        {
            var players = await _service.GetByClubName(clubName);

            return Ok(players);
        }
    }
}