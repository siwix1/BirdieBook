using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BirdieBook.Models
{
    public class UserScore
    {
        [HiddenInput(DisplayValue = false)]
        public string UserScoreID { get; set; } //PK in UserScore
        [HiddenInput(DisplayValue = false)]
        public string UserRoundID { get; set; } //FK in UserRound
        [HiddenInput(DisplayValue = false)]
        public string HoleID { get; set; } //FK in Holes
        [Range(1,18)]
        public int HoleNumber { get; set; } //Holenumbers from 1 to 18
        public int Score { get; set; }
        //public bool GIR { get; set; }
        public bool FairwayHit { get; set; }
        //public string DrivePosition { get; set; } //Missed right or left. Fairway center, right or left
        //public int DriveLength { get; set; }
        //public int ApproachLength { get; set; }
        public int PuttCount { get; set; } //Number of putts
        //Putting lengths
        
    }
}