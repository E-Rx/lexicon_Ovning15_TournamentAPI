using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tournament.Core.Dtos
{
    public record TournamentDetailsUpdateDto
    {
        public int Id { get; init; }

        [Required(ErrorMessage = "Title is required")]
        [MaxLength(50, ErrorMessage = "Title cannot be longer than 50 characters")]
        public string? Title { get; set; }
        public DateTime StartDate { get; set; }
    }
}
