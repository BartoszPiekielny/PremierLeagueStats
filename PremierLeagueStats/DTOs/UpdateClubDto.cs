using System.ComponentModel.DataAnnotations;

namespace PremierLeagueStats.DTOs
{
    public class UpdateClubDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string City { get; set; }

        [Range(1800, 2100)]
        public int FoundedYear { get; set; }

        [Range(1, 20)]
        public int Position { get; set; }

        public string? LogoUrl { get; set; }
    }
}