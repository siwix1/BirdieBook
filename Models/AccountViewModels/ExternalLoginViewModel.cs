using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BirdieBook.Models.AccountViewModels
{
    public class ExternalLoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public ApplicationUser.GenderType Gender { get; set; }

        [Display(Name = "Exact handicap")]
        [Range(-54, 10)]
        public decimal Handicap { get; set; }
    }
}
