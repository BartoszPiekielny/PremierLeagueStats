namespace PremierLeagueStats.DTOs
{
    public class UpdateGoalDto
    {
        public int Minute { get; set; }

        public int PlayerId { get; set; }

        public int MatchId { get; set; }
    }
}