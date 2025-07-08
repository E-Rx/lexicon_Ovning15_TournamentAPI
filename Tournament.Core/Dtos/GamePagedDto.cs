using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tournament.Core.Dtos
{
    namespace Tournament.Core.Dtos
    {
        public class GamePagedDto
        {
            public List<GameDto>? Items { get; set; }
            public int TotalCount { get; set; }
            public int TotalPages { get; set; }
            public int CurrentPage { get; set; }
            public int PageSize { get; set; }
        }
    }
}
