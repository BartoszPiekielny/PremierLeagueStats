namespace PremierLeagueStats.Models
{
    public class Coach
    {
        public int FootballDataId { get; set; }
        public int Id { get; set; }

        public string Name { get; set; }

        public string Nationality { get; set; }

        public DateTime StartDate { get; set; }

        public int ClubId { get; set; }
        public Club? Club { get; set; }
    }
}