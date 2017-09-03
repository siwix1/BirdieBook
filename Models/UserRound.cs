using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BirdieBook.Models
{
    public class UserRound
    {
        public UserRound()
        {
            Score = new List<UserScore>();
        }
        //[HiddenInput(DisplayValue=false)]
        public string UserRoundID { get; set; } //Primary Key

        public string UserID { get; set; } //FK to AspNetUsearLogins table
        public string TeeBoxID { get; set; } //FK to Teebox table
        public DateTime TeeTime { get; set; }
        public decimal UserHCP { get; set; } //HCP = Golfers Handicap
        public int DailyScratchRating { get; set; } //Usually only used with tournaments
                                                    //public bool RegulateHCP { get; set; } //If no, UserNewHCP = UserHCP
                                                    //public decimal UserNewHCP { get; set; } //Calculated new handicap
        public string WeatherCondition { get; set; } //Description of weather during round

        public virtual IEnumerable<UserScore> Score { get; set; } //Users gross score per hole from score card
   }


}
