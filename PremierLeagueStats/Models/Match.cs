namespace PremierLeagueStats.Models
{
    public class Match
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public int HomeClubId { get; set; }
        public int AwayClubId { get; set; }

        public int HomeScore { get; set; }
        public int AwayScore { get; set; }
    }
}