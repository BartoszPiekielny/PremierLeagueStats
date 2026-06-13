using Microsoft.EntityFrameworkCore;
using PremierLeagueStats.Data;
using PremierLeagueStats.Models;

namespace PremierLeagueStats.Services
{
    public class ClubService
    {
        private readonly AppDbContext _context;

        public ClubService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Club>> GetAll()
        {
            return await _context.Clubs
            .Include(c => c.Players)
            .ToListAsync();
        }

        public async Task<Club> Get(int id)
        {
            return await _context.Clubs
            .FindAsync(id);
        }

        public async Task<Club> Create(Club club)
        {
            _context.Clubs.Add(club);
            await _context.SaveChangesAsync();
            return club;
        }

        public async Task Delete(int id)
        {
            var club = await _context.Clubs.FindAsync(id);
            if (club != null)
            {
                _context.Clubs.Remove(club);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<Club?> Update(int id, Club club)
        {
            var existing = await _context.Clubs.FindAsync(id);

            if (existing == null)
                return null;

            existing.Name = club.Name;
            existing.City = club.City;
            existing.Position = club.Position;
            existing.FoundedYear = club.FoundedYear;

            await _context.SaveChangesAsync();

            return existing;
        }
    }
}