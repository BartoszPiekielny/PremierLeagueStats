using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using PremierLeagueStats.Data;
using PremierLeagueStats.Models;
using PremierLeagueStats.Services;
using Xunit;

namespace PremierLeagueStats.Tests.Services
{
    public class PlayerServiceTests
    {
        private AppDbContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new AppDbContext(options);
        }

        [Fact]
        public async Task GetAll_Should_Return_All_Players()
        {
            var context = CreateContext();

            var club = new Club
            {
                Name = "Arsenal",
                City = "London"
            };

            context.Clubs.Add(club);
            await context.SaveChangesAsync();

            context.Players.Add(new Player
            {
                FirstName = "Bruno",
                LastName = "Fernandes",
                Position = "CAM",
                Nationality = "Portugal",
                ClubId = club.Id
            });

            context.Players.Add(new Player
            {
                FirstName = "Erling",
                LastName = "Haaland",
                Position = "ST",
                Nationality = "Norway",
                ClubId = club.Id
            });

            await context.SaveChangesAsync();

            var service = new PlayerService(context);

            var result = await service.GetAll();

            result.Should().HaveCount(2);
        }

        [Fact]
        public async Task Get_Should_Return_Player_By_Id()
        {
            var context = CreateContext();

            var club = new Club
            {
                Name = "Arsenal",
                City = "London"
            };

            context.Clubs.Add(club);
            await context.SaveChangesAsync();

            var player = new Player
            {
                FirstName = "Bukayo",
                LastName = "Saka",
                Position = "RW",
                Nationality = "England",
                ClubId = club.Id
            };

            context.Players.Add(player);
            await context.SaveChangesAsync();

            var service = new PlayerService(context);

            var result = await service.Get(player.Id);

            result.Should().NotBeNull();
            result!.LastName.Should().Be("Saka");
        }

        [Fact]
        public async Task Create_Should_Add_Player()
        {
            var context = CreateContext();

            var service = new PlayerService(context);

            await service.Create(new Player
            {
                FirstName = "Cole",
                LastName = "Palmer",
                Position = "CAM",
                Nationality = "England"
            });

            context.Players.Count().Should().Be(1);
        }

        [Fact]
        public async Task Delete_Should_Remove_Player()
        {
            var context = CreateContext();

            var player = new Player
            {
                FirstName = "Kevin",
                LastName = "De Bruyne",
                Position = "CM",
                Nationality = "Belgium"
            };

            context.Players.Add(player);
            await context.SaveChangesAsync();

            var service = new PlayerService(context);

            var deleted = await service.Delete(player.Id);

            deleted.Should().BeTrue();
            context.Players.Count().Should().Be(0);
        }
    }
}