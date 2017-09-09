using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BirdieBook.Models
{
    public class Hole
    {
        [Key]
        public string HoleID { get; set; }
        [ForeignKey("TeeBox")]
        [HiddenInput(DisplayValue = false)]
        [Display(Name = "Tee")]
        public string TeeBoxID { get; set; }
        [Display(Name = "Hole")]
        public int HoleNumber { get; set; }
        public int Par { get; set; }
        public int Length { get; set; }
        [Display(Name ="HCP Index")]
        public int HCPIndex { get; set; }

        //public virtual Hole hole { get; set; }
    }
}
