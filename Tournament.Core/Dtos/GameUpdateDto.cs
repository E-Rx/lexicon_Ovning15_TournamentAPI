using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tournament.Core.Dtos
{
    public record GameUpdateDto
    {
        
        [Required(ErrorMessage = "Title is required")]
        [MaxLength(30, ErrorMessage = "Title cannot be longer than 30 characters")]
        public string? Title { get; set; }
        public DateTime Time { get; set; }
    }
}
