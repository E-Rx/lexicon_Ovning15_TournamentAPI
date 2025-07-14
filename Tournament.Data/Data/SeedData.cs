using Bogus;
using Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Tournament.Data.Data
{

    public static class SeedData
    {
        public static async Task InitAsync(TournamentAPIContext context)
        {
            // Skip seeding if data already exists
            if (await context.TournamentDetails.AnyAsync())
            {
                return;
            }

            var tournaments = GenerateFakeTournaments(75);

            context.TournamentDetails.AddRange(tournaments);
            await context.SaveChangesAsync();
        }

        private static List<TournamentDetails> GenerateFakeTournaments(int count = 10)
        {
            var gameFaker = new Faker<Game>()
                .RuleFor(g => g.Title, f => f.PickRandom(
                    "Opening Match", "Quarter Final", "Semi Final", "Final",
                    "Group Stage A", "Group Stage B", "Elimination Match",
                    "Qualifier Round", "Championship", "Grand Final",
                    "Round of 16", "Round of 32", "Bronze Match",
                    "Playoff", "Wildcard Match", "Third Place Match"
                ))
                .RuleFor(g => g.Time, f => f.Date.Future(1));

            var tournamentFaker = new Faker<TournamentDetails>()
                .RuleFor(t => t.Title, f => f.PickRandom(
                    "Winter Clash", "Spring Showdown", "Summer Cup", "Autumn Arena",
                    "Champions League", "Masters Tournament", "Grand Prix",
                    "Elite Championship", "Pro League", "Victory Cup",
                    "Thunder Tournament", "Lightning Cup", "Storm Championship",
                    "Blaze Tournament", "Frost Cup", "Solar Championship"
                ) + " " + f.Date.Between(DateTime.Now, DateTime.Now.AddYears(2)).Year)
                .RuleFor(t => t.StartDate, f => f.Date.Future())
                .RuleFor(t => t.Games, (f, t) =>
                {
                    // Generate between 3 and 8 games per tournament
                    var gameCount = f.Random.Int(3, 8);
                    var games = gameFaker.Generate(gameCount);

                    // Adjust game dates to be consistent with tournament start date
                    var currentDate = t.StartDate;
                    foreach (var game in games)
                    {
                        game.Time = currentDate.AddDays(f.Random.Int(0, 2)).AddHours(f.Random.Int(9, 20));
                        currentDate = game.Time.AddDays(1);
                    }

                    return games;
                });

            return tournamentFaker.Generate(count);
        }
    }
}







    //public static class SeedData
    //{
    //    public static async Task InitAsync(TournamentAPIContext context)
    //    {
    //        if (await context.TournamentDetails.AnyAsync())
    //        {
    //            return;
    //        }

    //        var tournaments = new List<TournamentDetails>
    //        {
    //            new TournamentDetails
    //            {
    //                Title = "Winter Clash 2025",
    //                StartDate = new DateTime(2025, 1, 10),
    //                Games = new List<Game>
    //                {
    //                    new Game { Title = "Opening Match", Time = new DateTime(2025, 1, 11, 9, 0, 0) },
    //                    new Game { Title = "Quarter Final", Time = new DateTime(2025, 1, 15, 13, 0, 0) },
    //                    new Game { Title = "Semi Final", Time = new DateTime(2025, 1, 19, 17, 0, 0) },
    //                    new Game { Title = "Final", Time = new DateTime(2025, 1, 20, 18, 0, 0) }
    //                }
    //            },

    //            new TournamentDetails
    //            {
    //                Title = "Spring Showdown 2025",
    //                StartDate = new DateTime(2025, 4, 4),
    //                Games = new List<Game>
    //                {
    //                    new Game { Title = "Opening Match", Time = new DateTime(2025, 4, 4, 9, 0, 0) },
    //                    new Game { Title = "Quarter Final", Time = new DateTime(2025, 4, 6, 11, 0, 0) },
    //                    new Game { Title = "Semi Final", Time = new DateTime(2025, 4, 10, 15, 0, 0) },
    //                    new Game { Title = "Championship", Time = new DateTime(2025, 4, 12, 19, 0, 0) }
    //                }
    //            },

    //            new TournamentDetails
    //            {
    //                Title = "Summer Cup 2025",
    //                StartDate = new DateTime(2025, 6, 1),
    //                Games = new List<Game>
    //                {
    //                    new Game { Title = "Match 1", Time = new DateTime(2024, 6, 2, 10, 0, 0) },
    //                    new Game { Title = "Match 2", Time = new DateTime(2024, 6, 4, 14, 30, 0) },
    //                    new Game { Title = "Match 3", Time = new DateTime(2024, 6, 6, 18, 0, 0) },
    //                    new Game { Title = "Match 4", Time = new DateTime(2024, 6, 8, 12, 0, 0) }
    //                }
    //            },

    //            new TournamentDetails
    //            {
    //                Title = "Autumn Arena 2025",
    //                StartDate = new DateTime(2025, 9, 15),
    //                Games = new List<Game>
    //                {
    //                    new Game { Title = "Opening Clash", Time = new DateTime(2025, 9, 16, 10, 0, 0) },
    //                    new Game { Title = "Group Stage A", Time = new DateTime(2025, 9, 17, 14, 0, 0) },
    //                    new Game { Title = "Group Stage B", Time = new DateTime(2025, 9, 18, 16, 0, 0) },
    //                    new Game { Title = "Final Duel", Time = new DateTime(2025, 9, 20, 19, 30, 0) }
    //                }
    //            },
    //            
    //        };
    //        context.TournamentDetails.AddRange(tournaments);
    //        await context.SaveChangesAsync();
    //    }
    //}


