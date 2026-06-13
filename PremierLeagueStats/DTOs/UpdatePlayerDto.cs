using System.ComponentModel.DataAnnotations;

namespace PremierLeagueStats.DTOs
{
    public class UpdatePlayerDto
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        public string Position { get; set; }

        [Required]
        public string Nationality { get; set; }

        [Range(0,100)]
        public int Goals { get; set; }

        [Range(0,100)]
        public int Assists { get; set; }

        [Range(0,5000)]
        public int MinutesPlayed { get; set; }

        public int ClubId { get; set; }
    }
}