using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BirdieBook.Models
{
    public class HCPHistory
    {
        public string UserID { get; set; }
        public DateTime HCPTime { get; set; }
        public bool ManualUpdate { get; set; }
        public decimal OldHandicap { get; set; }
        public decimal NewHandicap { get; set; }


    }
}
