using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace FineBlog.ViewModels
{
    public class ResetPasswordVM
    {
        public string? Id { get; set; }
        public string? UserName { get; set; }
        [Required] 
        public string? NewPassword { get; set;}
        [Compare(nameof(NewPassword))]
        [Required] 
        public string? ConfirmPassword { get; set;}
    }
}
