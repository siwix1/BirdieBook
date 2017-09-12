using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BirdieBook.Models
{
    public class Hole
    {
        [Key]
        public string HoleId { get; set; }
        [ForeignKey("TeeBox")]
        [HiddenInput(DisplayValue = false)]
        [Display(Name = "Tee")]
        public string TeeBoxId { get; set; }
        [Display(Name = "Hole")]
        public int HoleNumber { get; set; }
        public int Par { get; set; }
        public int Length { get; set; }
        [Display(Name ="Hcp Index")]
        public int HcpIndex { get; set; }

        //public virtual Hole hole { get; set; }
    }
}
