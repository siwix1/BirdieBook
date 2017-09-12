using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace BirdieBook.Models
{
    public sealed class UserRound
    {
        public UserRound()
        {
            Score = new List<UserScore>();
        }

        [HiddenInput(DisplayValue=false)]
        public string UserRoundId { get; set; } //Primary Key

        [HiddenInput(DisplayValue = false)]
        public string UserId { get; set; } //FK to AspNetUsearLogins table

        public GameType Game { get; set; }

        public enum GameType
        {
            Stroke=0,
            Stableford=1,
            Par=2
            //Scramble,
            //Fourball,
            //Foursome
        }

        public string TeeBoxId { get; set; } //FK to Teebox table
        public DateTime TeeTime { get; set; }
        public decimal UserHcp { get; set; } //Hcp = Golfers Handicap
        public int DailyScratchRating { get; set; } //Usually only used with tournaments
                                                    //public bool RegulateHcp { get; set; } //If no, UserNewHcp = UserHcp
                                                    //public decimal UserNewHcp { get; set; } //Calculated new handicap
        public string WeatherCondition { get; set; } //Description of weather during round

        public IEnumerable<UserScore> Score { get; set; } //Users gross score per hole from score card
   }


}
