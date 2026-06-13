using Microsoft.EntityFrameworkCore;
using PremierLeagueStats.Data;
using PremierLeagueStats.Models;

namespace PremierLeagueStats.Services
{
    public class PlayerService
    {
        private readonly AppDbContext _context;

        public PlayerService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Player>> GetAll()
        {
            return await _context.Players
                .Include(p => p.Club)
                .ToListAsync();
        }

        public async Task<Player?> Get(int id)
        {
            return await _context.Players
                .Include(p => p.Club)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Player> Create(Player player)
        {
            _context.Players.Add(player);
            await _context.SaveChangesAsync();
            return player;
        }

        public async Task<Player?> Update(int id, Player player)
        {
            var existing = await _context.Players.FindAsync(id);

            if (existing == null)
                return null;

            existing.FirstName = player.FirstName;
            existing.LastName = player.LastName;
            existing.Position = player.Position;
            existing.Nationality = player.Nationality;
            existing.Goals = player.Goals;
            existing.Assists = player.Assists;
            existing.MinutesPlayed = player.MinutesPlayed;
            existing.ClubId = player.ClubId;

            await _context.SaveChangesAsync();

            return existing;
        }

        public async Task<bool> Delete(int id)
        {
            var player = await _context.Players.FindAsync(id);

            if (player == null)
                return false;

            _context.Players.Remove(player);

            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<List<Player>> Search(string name)
        {
            return await _context.Players
                .Where(p =>
                    p.FirstName.Contains(name) ||
                    p.LastName.Contains(name))
                .ToListAsync();
        }
        public async Task<List<Player>> GetByNationality(string country)
        {
            return await _context.Players
                .Where(p => p.Nationality.Contains(country))
                .ToListAsync();
        }
        public async Task<List<Player>> GetByClub(int clubId)
        {
            return await _context.Players
                .Where(p => p.ClubId == clubId)
                .ToListAsync();
        }
        public async Task<List<Player>> GetByClubName(string clubName)
        {
            clubName = clubName.ToLower();

            return await _context.Players
                .Include(p => p.Club)
                .Where(p => p.Club != null &&
                            p.Club.Name.ToLower().Contains(clubName))
                .ToListAsync();
        }
        public async Task<List<Player>> GetByPosition(string position)
        {
            position = position.ToLower();

            return await _context.Players
                .Where(p => p.Position.ToLower().Contains(position))
                .ToListAsync();
        }
    }
}