using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BirdieBook.Models
{
    public class Hole
    {
        public string HoleID { get; set; }
        public string TeeBoxID { get; set; }
        public int HoleNumber { get; set; }
        public int Par { get; set; }
        public int Length { get; set; }
        public int HCPIndex { get; set; }

        //public virtual Hole hole { get; set; }
    }
}
