using System.Text.Json;
using PremierLeagueStats.Data;
using PremierLeagueStats.Models;

namespace PremierLeagueStats.Services
{
    public class FootballDataService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _context;

        public FootballDataService(
            HttpClient httpClient,
            IConfiguration configuration,
            AppDbContext context)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _context = context;
        }

        public async Task<int> ImportClubs()
        {
            var apiKey =
                _configuration["FootballData:ApiKey"];

            _httpClient.DefaultRequestHeaders.Clear();

            _httpClient.DefaultRequestHeaders.Add(
                "X-Auth-Token",
                apiKey);

            var response =
                await _httpClient.GetAsync(
                    "https://api.football-data.org/v4/competitions/PL/teams");

            response.EnsureSuccessStatusCode();

            var json =
                await response.Content.ReadAsStringAsync();

            using var document =
                JsonDocument.Parse(json);

            var teams =
                document.RootElement.GetProperty("teams");

            int imported = 0;

            foreach (var team in teams.EnumerateArray())
            {
                var clubName =
                    team.GetProperty("name").GetString();

                if (string.IsNullOrWhiteSpace(clubName))
                    continue;

                bool exists =
                    _context.Clubs.Any(c => c.Name == clubName);

                if (exists)
                    continue;

                int foundedYear = 0;

                if (team.TryGetProperty("founded", out var founded))
                {
                    foundedYear = founded.GetInt32();
                }

                string? crestUrl = null;

                if (team.TryGetProperty("crest", out var crest))
                {
                    crestUrl = crest.GetString();
                }

                string? areaName = null;

                if (team.TryGetProperty("area", out var area))
                {
                    if (area.TryGetProperty("name", out var areaProperty))
                    {
                        areaName = areaProperty.GetString();
                    }
                }

                var club = new Club
                {
                    FootballDataId =
                        team.GetProperty("id").GetInt32(),
                    Name = clubName,
                    City = areaName ?? "Unknown",
                    FoundedYear = foundedYear,
                    Position = 0,
                    LogoUrl = crestUrl
                };

                _context.Clubs.Add(club);

                imported++;
            }

            await _context.SaveChangesAsync();

            return imported;
        } //temporary   
        public async Task<string> TestTeam(int footballDataId)
        {
        var apiKey =
            _configuration["FootballData:ApiKey"];

        _httpClient.DefaultRequestHeaders.Clear();

        _httpClient.DefaultRequestHeaders.Add(
            "X-Auth-Token",
            apiKey);

        var response =
            await _httpClient.GetAsync(
                $"https://api.football-data.org/v4/teams/{footballDataId}");

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync();
        }
        public async Task<int> ImportPlayers()
{
    var apiKey =
        _configuration["FootballData:ApiKey"];

    _httpClient.DefaultRequestHeaders.Clear();

    _httpClient.DefaultRequestHeaders.Add(
        "X-Auth-Token",
        apiKey);

    int imported = 0;

    var clubs = _context.Clubs.ToList();

    foreach (var club in clubs)
    {
        Console.WriteLine(
            $"Importing players from {club.Name} ({club.FootballDataId})");

        var response =
            await _httpClient.GetAsync(
                $"https://api.football-data.org/v4/teams/{club.FootballDataId}");

        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine(
                $"Skipping {club.Name} - Status: {response.StatusCode}");

            continue;
        }

        var json =
            await response.Content.ReadAsStringAsync();

        using var document =
            JsonDocument.Parse(json);

        if (!document.RootElement.TryGetProperty(
                "squad",
                out var squad))
        {
            Console.WriteLine(
                $"No squad found for {club.Name}");

            continue;
        }

        foreach (var player in squad.EnumerateArray())
        {
            int footballDataId =
                player.GetProperty("id").GetInt32();

            bool exists =
                _context.Players.Any(p =>
                    p.FootballDataId == footballDataId);

            if (exists)
                continue;

            string fullName =
                player.GetProperty("name").GetString()
                ?? "";

            string firstName = fullName;
            string lastName = "";

            var parts =
                fullName.Split(
                    ' ',
                    StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length > 1)
            {
                firstName = parts[0];

                lastName =
                    string.Join(
                        " ",
                        parts.Skip(1));
            }

            string position = "";

            if (player.TryGetProperty(
                    "position",
                    out var positionProperty))
            {
                position =
                    positionProperty.GetString() ?? "";
            }

            string nationality = "";

            if (player.TryGetProperty(
                    "nationality",
                    out var nationalityProperty))
            {
                nationality =
                    nationalityProperty.GetString() ?? "";
            }

            var newPlayer = new Player
            {
                FootballDataId = footballDataId,

                FirstName = firstName,

                LastName = lastName,

                Position = position,

                Nationality = nationality,

                Goals = 0,

                Assists = 0,

                MinutesPlayed = 0,

                ClubId = club.Id
            };

            _context.Players.Add(newPlayer);

            imported++;
        }

        Console.WriteLine(
            $"Finished {club.Name}");
    }

    await _context.SaveChangesAsync();

    Console.WriteLine(
        $"Imported players: {imported}");

    return imported;
        }
        public async Task<int> ImportCoaches()
{
    var apiKey =
        _configuration["FootballData:ApiKey"];

    _httpClient.DefaultRequestHeaders.Clear();

    _httpClient.DefaultRequestHeaders.Add(
        "X-Auth-Token",
        apiKey);

    int imported = 0;

    var clubs = _context.Clubs.ToList();

    foreach (var club in clubs)
    {
        var response =
            await _httpClient.GetAsync(
                $"https://api.football-data.org/v4/teams/{club.FootballDataId}");

        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine(
                $"Skipping {club.Name}");

            continue;
        }

        var json =
            await response.Content.ReadAsStringAsync();

        using var document =
            JsonDocument.Parse(json);

        if (!document.RootElement.TryGetProperty(
        "coach",
        out var coachElement))
        continue;

        if (coachElement.ValueKind == JsonValueKind.Null)
        continue;

        if (!coachElement.TryGetProperty("id", out var coachIdElement))
        continue;

        if (coachIdElement.ValueKind == JsonValueKind.Null)
            continue;

        int footballDataId =
            coachIdElement.GetInt32();

        bool exists =
            _context.Coaches.Any(c =>
                c.FootballDataId == footballDataId);

        if (exists)
            continue;

        string name =
            coachElement.GetProperty("name")
                .GetString() ?? "";

        string nationality =
            coachElement.GetProperty("nationality")
                .GetString() ?? "";

        DateTime startDate = DateTime.Now;

        if (coachElement.TryGetProperty(
                "contract",
                out var contract))
        {
            if (contract.TryGetProperty(
                    "start",
                    out var start))
            {
                DateTime.TryParse(
                    start.GetString(),
                    out startDate);
            }
        }

        var coach = new Coach
        {
            FootballDataId = footballDataId,
            Name = name,
            Nationality = nationality,
            StartDate = startDate,
            ClubId = club.Id
        };

        _context.Coaches.Add(coach);

        imported++;
    }

    await _context.SaveChangesAsync();

    return imported;
        }
    }
}