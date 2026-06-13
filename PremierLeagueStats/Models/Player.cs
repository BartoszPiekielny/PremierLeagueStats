using System.ComponentModel.DataAnnotations;

namespace PremierLeagueStats.Models
{
    public class Player
    {
        public int FootballDataId { get; set; }
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        public string Position { get; set; }

        public string Nationality { get; set; }

        [Range(0, 100)]
        public int Goals { get; set; }

        [Range(0, 100)]
        public int Assists { get; set; }

        public int MinutesPlayed { get; set; }

        public int ClubId { get; set; }

        public Club? Club { get; set; }
    }
}