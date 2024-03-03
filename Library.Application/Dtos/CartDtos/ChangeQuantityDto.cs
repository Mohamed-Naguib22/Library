using Library.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Dtos.CartDtos
{
    public class ChangeQuantityDto
    {
        public string ChangeQuantity { get; set; }
        public int BookId { get; set; }
    }
}
