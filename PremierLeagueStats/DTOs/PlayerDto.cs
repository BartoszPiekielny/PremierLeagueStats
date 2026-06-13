namespace PremierLeagueStats.DTOs
{
    public class PlayerDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Position { get; set; }

        public string Nationality { get; set; }

        public int Goals { get; set; }

        public int Assists { get; set; }

        public int MinutesPlayed { get; set; }

        public int ClubId { get; set; }
    }
}