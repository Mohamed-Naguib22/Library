using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Library.Domain.Models
{
    public class SearchQuery
    {
        public int Id { get; set; }
        [MaxLength(128)]
        public string Query { get; set; }
        public DateTime TimSpan { get; set; }
        [JsonIgnore]
        public string ApplicationUserId { get; set; }
        [JsonIgnore]
        public ApplicationUser ApplicationUser { get; set; }
    }
}
