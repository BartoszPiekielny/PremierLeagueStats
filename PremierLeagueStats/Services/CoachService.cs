using Microsoft.EntityFrameworkCore;
using PremierLeagueStats.Data;
using PremierLeagueStats.Models;

namespace PremierLeagueStats.Services
{
    public class CoachService
    {
        private readonly AppDbContext _context;

        public CoachService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Coach>> GetAll()
        {
            return await _context.Coaches
                .Include(c => c.Club)
                .ToListAsync();
        }

        public async Task<Coach?> Get(int id)
        {
            return await _context.Coaches
                .Include(c => c.Club)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Coach> Create(Coach coach)
        {
            _context.Coaches.Add(coach);
            await _context.SaveChangesAsync();
            return coach;
        }

        public async Task<Coach?> Update(int id, Coach coach)
        {
            var existing = await _context.Coaches.FindAsync(id);

            if (existing == null)
                return null;

            existing.Name = coach.Name;
            existing.Nationality = coach.Nationality;
            existing.StartDate = coach.StartDate;
            existing.ClubId = coach.ClubId;

            await _context.SaveChangesAsync();

            return existing;
        }

        public async Task<bool> Delete(int id)
        {
            var coach = await _context.Coaches.FindAsync(id);

            if (coach == null)
                return false;

            _context.Coaches.Remove(coach);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}