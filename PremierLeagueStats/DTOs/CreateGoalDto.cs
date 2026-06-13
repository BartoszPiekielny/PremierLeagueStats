namespace PremierLeagueStats.DTOs
{
    public class CreateGoalDto
    {
        public int Minute { get; set; }

        public int PlayerId { get; set; }

        public int MatchId { get; set; }
    }
}