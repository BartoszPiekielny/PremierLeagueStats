using Microsoft.EntityFrameworkCore;
using PremierLeagueStats.Data;
using PremierLeagueStats.Models;

namespace PremierLeagueStats.Services
{
    public class MatchService
    {
        private readonly AppDbContext _context;

        public MatchService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Match>> GetAll()
        {
            return await _context.Matches.ToListAsync();
        }

        public async Task<Match?> Get(int id)
        {
            return await _context.Matches.FindAsync(id);
        }

        public async Task<Match> Create(Match match)
        {
            _context.Matches.Add(match);
            await _context.SaveChangesAsync();
            return match;
        }

        public async Task<Match?> Update(int id, Match match)
        {
            var existing = await _context.Matches.FindAsync(id);

            if (existing == null)
                return null;

            existing.Date = match.Date;
            existing.HomeClubId = match.HomeClubId;
            existing.AwayClubId = match.AwayClubId;
            existing.HomeScore = match.HomeScore;
            existing.AwayScore = match.AwayScore;

            await _context.SaveChangesAsync();

            return existing;
        }

        public async Task<bool> Delete(int id)
        {
            var match = await _context.Matches.FindAsync(id);

            if (match == null)
                return false;

            _context.Matches.Remove(match);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}