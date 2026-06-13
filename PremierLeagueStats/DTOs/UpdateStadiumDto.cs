using System.ComponentModel.DataAnnotations;

namespace PremierLeagueStats.DTOs
{
    public class UpdateStadiumDto
    {
        [Required]
        public string Name { get; set; }

        public int Capacity { get; set; }

        public string City { get; set; }

        public int BuildYear { get; set; }

        public int ClubId { get; set; }
    }
}