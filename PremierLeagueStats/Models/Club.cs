using System.ComponentModel.DataAnnotations;

namespace PremierLeagueStats.Models
{
    public class Club
    {
        public int FootballDataId { get; set; }
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string City { get; set; }

        public int FoundedYear { get; set; }

        public int Position { get; set; }

        public string? LogoUrl { get; set; }

        public ICollection<Player> Players { get; set; }
            = new List<Player>();

        public Coach? Coach { get; set; }

        public Stadium? Stadium { get; set; }
    }
}