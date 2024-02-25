﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public class Genre
    {
        public int Id { get; set; }
        [MaxLength(256)]
        public string Name { get; set; }
        public string ImgUrl { get; set; }
        public IEnumerable<BookGenre> BookGenres { get; set; }
    }
}
