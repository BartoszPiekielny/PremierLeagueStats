using Microsoft.EntityFrameworkCore;
using PremierLeagueStats.Data;
using PremierLeagueStats.Models;

namespace PremierLeagueStats.Services
{
    public class StadiumService
    {
        private readonly AppDbContext _context;

        public StadiumService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Stadium>> GetAll()
        {
            return await _context.Stadiums
                .Include(s => s.Club)
                .ToListAsync();
        }

        public async Task<Stadium?> Get(int id)
        {
            return await _context.Stadiums
                .Include(s => s.Club)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Stadium> Create(Stadium stadium)
        {
            _context.Stadiums.Add(stadium);
            await _context.SaveChangesAsync();
            return stadium;
        }

        public async Task<Stadium?> Update(int id, Stadium stadium)
        {
            var existing = await _context.Stadiums.FindAsync(id);

            if (existing == null)
                return null;

            existing.Name = stadium.Name;
            existing.Capacity = stadium.Capacity;
            existing.City = stadium.City;
            existing.BuildYear = stadium.BuildYear;
            existing.ClubId = stadium.ClubId;

            await _context.SaveChangesAsync();

            return existing;
        }

        public async Task<bool> Delete(int id)
        {
            var stadium = await _context.Stadiums.FindAsync(id);

            if (stadium == null)
                return false;

            _context.Stadiums.Remove(stadium);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}