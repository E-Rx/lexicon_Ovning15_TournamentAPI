using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tournament.Core.Dtos
{
   
    public record TournamentDetailsCreateDto
    {
        [Required]
        [MaxLength(50)]
        public string? Title { get; init; }

        public DateTime StartDate { get; init; }

        public ICollection<GameDto>? Games { get; init; } = new List<GameDto>();
    }

    
}
