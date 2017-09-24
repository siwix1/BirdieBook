using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        [DisplayName("Tee")]
        public string TeeBoxId { get; set; } //FK to Teebox table
        [DisplayName("Tee Time")]
        public DateTime TeeTime { get; set; }
        public VisibilityType Visibility { get; set; }

        public enum VisibilityType
        {
            Show = 0,
            HideWhilePlaying = 1,
            HidePermanent = 2,
            ShowFriends = 3
        }

        public bool RegulateHandicap { get; set; }
        [DisplayFormat(DataFormatString = "{0:0.0}")]
        [DisplayName("Hcp before round")]
        public decimal UserHcp { get; set; } //Hcp = Golfers Handicap
        public decimal NewUserHcp { get; set; } //Calculated after round
        public int GivenShots { get; set; }
        public bool Tournament { get; set; }
        
        public int? DailyScratchRating { get; set; } //Usually only used with tournaments
        public string WeatherCondition { get; set; } //Description of weather during round

        public IEnumerable<UserScore> Score { get; set; } //Users gross score per hole from score card


        




    }


}
