using System.ComponentModel.DataAnnotations;

namespace PremierLeagueStats.DTOs
{
    public class CreateCoachDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Nationality { get; set; }

        public DateTime StartDate { get; set; }

        public int ClubId { get; set; }
    }
}