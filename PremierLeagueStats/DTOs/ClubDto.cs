namespace PremierLeagueStats.DTOs
{
    public class ClubDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string City { get; set; }

        public int FoundedYear { get; set; }

        public int Position { get; set; }

        public string? LogoUrl { get; set; }
    }
}