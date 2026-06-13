namespace PremierLeagueStats.Models
{
    public class Stadium
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Capacity { get; set; }

        public string City { get; set; }

        public int BuildYear { get; set; }

        public int ClubId { get; set; }
        public Club? Club { get; set; }
    }
}