﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Dtos.GenreDto
{
    public class AddGenreDto
    {
        [StringLength(256)]
        public string Name { get; set; }
        public IFormFile Image { get; set; }
    }
}
