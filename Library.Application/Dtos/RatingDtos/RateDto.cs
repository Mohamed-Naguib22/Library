using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Dtos.RatingDtos
{
    public class RateDto
    {
        [Range(1, 5, ErrorMessage = "The rating must be between 1 and 5")]
        public int Rate { get; set; }
        public int BookId { get; set; }
    }
}
