using System.ComponentModel.DataAnnotations;

namespace Library.Application.Dtos.AuthDtos
{
    public class ChangePasswordDto
    {
        [StringLength(128)]
        public string CurrentPassword { get; set; }
        [StringLength(128)]
        public string NewPassword { get; set; }
    }
}