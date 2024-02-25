using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Dtos.AuthorDtos
{
    public class AddAuthorDto
    {
        public string Name { get; set; }
        [StringLength(1024)]
        public string Biography { get; set; }
        public IFormFile Image { get; set; }
    }
}
