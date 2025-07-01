using Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Tournament.Data.Data
{
    public static class SeedData
    {
        public static async Task InitAsync(TournamentAPIContext context)
        {
            if (await context.TournamentDetails.AnyAsync())
            {
                return; 
            }

            var tournaments = new List<TournamentDetails>
            {
                new TournamentDetails
                {
                    Title = "Winter Clash 2025",
                    StartDate = new DateTime(2025, 1, 10),
                    Games = new List<Game>
                    {
                        new Game { Title = "Opening Match", Time = new DateTime(2025, 1, 11, 9, 0, 0) },
                        new Game { Title = "Quarter Final", Time = new DateTime(2025, 1, 15, 13, 0, 0) },
                        new Game { Title = "Semi Final", Time = new DateTime(2025, 1, 19, 17, 0, 0) },
                        new Game { Title = "Final", Time = new DateTime(2025, 1, 20, 18, 0, 0) }
                    }
                },

                new TournamentDetails
                {
                    Title = "Spring Showdown 2025",
                    StartDate = new DateTime(2025, 4, 5),
                    Games = new List<Game>
                    {
                        new Game { Title = "Opening Match", Time = new DateTime(2025, 4, 4, 9, 0, 0) },
                        new Game { Title = "Quarter Final", Time = new DateTime(2025, 4, 6, 11, 0, 0) },
                        new Game { Title = "Semi Final", Time = new DateTime(2025, 4, 10, 15, 0, 0) },
                        new Game { Title = "Championship", Time = new DateTime(2025, 4, 12, 19, 0, 0) }
                    }
                },

                new TournamentDetails
                {
                    Title = "Summer Cup 2025",
                    StartDate = new DateTime(2025, 6, 1),
                    Games = new List<Game>
                    {
                        new Game { Title = "Match 1", Time = new DateTime(2024, 6, 2, 10, 0, 0) },
                        new Game { Title = "Match 2", Time = new DateTime(2024, 6, 4, 14, 30, 0) },
                        new Game { Title = "Match 3", Time = new DateTime(2024, 6, 6, 18, 0, 0) },
                        new Game { Title = "Match 4", Time = new DateTime(2024, 6, 8, 12, 0, 0) }
                    }
                },

            };



            context.TournamentDetails.AddRange(tournaments);
            await context.SaveChangesAsync();
        }
    }
}

