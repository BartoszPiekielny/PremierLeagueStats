using Microsoft.EntityFrameworkCore;
using PremierLeagueStats.Data;
using PremierLeagueStats.Models;

namespace PremierLeagueStats.Services
{
    public class GoalService
    {
        private readonly AppDbContext _context;

        public GoalService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Goal>> GetAll()
        {
            return await _context.Goals
                .Include(g => g.Player)
                .Include(g => g.Match)
                .ToListAsync();
        }

        public async Task<Goal?> Get(int id)
        {
            return await _context.Goals
                .Include(g => g.Player)
                .Include(g => g.Match)
                .FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<Goal> Create(Goal goal)
        {
            _context.Goals.Add(goal);
            await _context.SaveChangesAsync();
            return goal;
        }

        public async Task<Goal?> Update(int id, Goal goal)
        {
            var existing = await _context.Goals.FindAsync(id);

            if (existing == null)
                return null;

            existing.Minute = goal.Minute;
            existing.PlayerId = goal.PlayerId;
            existing.MatchId = goal.MatchId;

            await _context.SaveChangesAsync();

            return existing;
        }

        public async Task<bool> Delete(int id)
        {
            var goal = await _context.Goals.FindAsync(id);

            if (goal == null)
                return false;

            _context.Goals.Remove(goal);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}