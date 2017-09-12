using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace BirdieBook.Models
{
    public class UserScore
    {
        public UserScore()
        {
            Tags = new List<Tag>();
        }
        [HiddenInput(DisplayValue = false)]
        public string UserScoreId { get; set; } //PK in UserScore
        [HiddenInput(DisplayValue = false)]
        public string UserRoundId { get; set; } //FK in UserRound
        [HiddenInput(DisplayValue = false)]
        public string HoleId { get; set; } //FK in Holes
        [Range(1,18)]
        public int HoleNumber { get; set; } //TODO: Holenumbers from 1 to 18 --not needed?

        public int Score { get; set; }
        public int Points { get; set; }
        public bool FairwayHit { get; set; } //TODO: Users to set their own default value?
        public bool Bunker { get; set; }
        public bool Water { get; set; }
        public bool OutOfBounds { get; set; }
        public bool LostBall { get; set; } 
        public bool PickedUp { get; set; }
        [Range(0, int.MaxValue)]
        [DefaultValue(2)]
        public int PuttCount { get; set; } //Number of putts
        [Range(0, int.MaxValue)]
        public int ChipCount { get; set; } //Number of Chip shots taken
        [Range(0, int.MaxValue)]
        public int BunkershotCount { get; set; } //Number of bunker shots per hole
        [Range(0, int.MaxValue)]
        public int PenaltyCount { get; set; } //TODO: Number of penalties taken per hole. Automatic?
        

        public ICollection<Tag> Tags { get; set; }

    }
}