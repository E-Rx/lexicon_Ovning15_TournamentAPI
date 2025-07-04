using System.ComponentModel.DataAnnotations;

namespace Tournament.Core.Dtos
{
    public record GameCreateDto
    {
        [Required(ErrorMessage = "Title is required")]
        [MaxLength(30, ErrorMessage = "Title cannot be longer than 30 characters")]
        public string? Title { get; init; }

        public DateTime Time { get; init; }

        public int TournamentId { get; init; }
    }
}