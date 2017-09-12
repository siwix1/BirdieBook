using System;


namespace BirdieBook.Models
{
    public class HcpHistory
    {
        public string UserId { get; set; }
        public DateTime HcpTime { get; set; }
        public bool ManualUpdate { get; set; }
        public decimal OldHandicap { get; set; }
        public decimal NewHandicap { get; set; }


    }
}
