using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Dtos
{
    public class BinlistResponse
    {
        public string Scheme { get; set; }
        public string Type { get; set; }
        public string Brand { get; set; }
        public Bank Bank { get; set; }
    }
}
