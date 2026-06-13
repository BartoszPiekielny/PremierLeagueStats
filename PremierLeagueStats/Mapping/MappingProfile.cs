using AutoMapper;
using PremierLeagueStats.DTOs;
using PremierLeagueStats.Models;

namespace PremierLeagueStats.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreatePlayerDto, Player>();
            CreateMap<UpdatePlayerDto, Player>();
            CreateMap<Player, PlayerDto>();

            CreateMap<CreateClubDto, Club>();
            CreateMap<UpdateClubDto, Club>();
            CreateMap<Club, ClubDto>();

            CreateMap<CreateCoachDto, Coach>();
            CreateMap<UpdateCoachDto, Coach>();

            CreateMap<CreateStadiumDto, Stadium>();
            CreateMap<UpdateStadiumDto, Stadium>();

            CreateMap<CreateMatchDto, Match>();
            CreateMap<UpdateMatchDto, Match>();

            CreateMap<CreateGoalDto, Goal>();
            CreateMap<UpdateGoalDto, Goal>();
        }
    }
}