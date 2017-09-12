using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BirdieBook.Models
{
    public class Tag
    {
        public Tag()
        {
            TagMaps = new List<TagMap>();
        }
        [Key]
        public string TagId { get; set; }
        public string Label { get; set; } //Languages?
        public TagTable TagType { get; set; } //Referring to the table

        public enum TagTable //Should be set automatically by system. Maybe select from user tables
        {
            ClubType, //Bag Contents
            ShotConditions, //Lie and other conditions
            UserScore,
            UserRound,
            Hole,
            TeeBox,
            GolfCourse
        }

        public ICollection<TagMap> TagMaps { get; set; }

    }
}
