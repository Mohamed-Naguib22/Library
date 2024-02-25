using System.ComponentModel.DataAnnotations;

namespace Library.Application.Dtos.AuthModels
{
    public class ChangePasswordModel
    {
        [StringLength(128)]
        public string CurrentPassword { get; set; }
        [StringLength(128)]
        public string NewPassword { get; set; }
    }
}