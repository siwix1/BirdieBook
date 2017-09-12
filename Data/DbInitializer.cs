using BirdieBook.Models;
using System.Linq;

namespace BirdieBook.Data
{
    public static class DbInitializer
    {
        public static void Initialize(BirdieBookContext context)
        {
            context.Database.EnsureCreated();

            // Look for any Golf holes.
            if (!context.Hole.Any())
            {


                var holes = new[]
                {
                    new Hole{ HoleNumber = 1, Par = 4, Length = 365, HcpIndex = 1},
                    new Hole{ HoleNumber = 2, Par = 5, Length = 471, HcpIndex = 12},
                    new Hole{ HoleNumber = 3, Par = 3, Length = 141, HcpIndex = 14},
                    new Hole{ HoleNumber = 4, Par = 4, Length = 325, HcpIndex = 2},
                    new Hole{ HoleNumber = 5, Par = 4, Length = 264, HcpIndex = 18},
                    new Hole{ HoleNumber = 6, Par = 4, Length = 312, HcpIndex = 3},
                    new Hole{ HoleNumber = 7, Par = 3, Length = 149, HcpIndex = 8},
                    new Hole{ HoleNumber = 8, Par = 5, Length = 459, HcpIndex = 13},
                    new Hole{ HoleNumber = 9, Par = 4, Length = 339, HcpIndex = 7},
                    new Hole{ HoleNumber = 10, Par = 5, Length = 458, HcpIndex = 9},
                    new Hole{ HoleNumber = 11, Par = 3, Length = 136, HcpIndex = 11},
                    new Hole{ HoleNumber = 12, Par = 4, Length = 362, HcpIndex = 5},
                    new Hole{ HoleNumber = 13, Par = 4, Length = 325, HcpIndex = 10},
                    new Hole{ HoleNumber = 14, Par = 4, Length = 341, HcpIndex = 6},
                    new Hole{ HoleNumber = 15, Par = 4, Length = 294, HcpIndex = 17},
                    new Hole{ HoleNumber = 16, Par = 3, Length = 131, HcpIndex = 15},
                    new Hole{ HoleNumber = 17, Par = 5, Length = 451, HcpIndex = 16},
                    new Hole{ HoleNumber = 18, Par = 4, Length = 353, HcpIndex = 4}
                };


                var teeBoxes = new[]
                {
                    new TeeBox
                    {
                        Name = "White",
                        MensCourseRating =70,
                        MensSlope = 129,
                        Holes =holes,
                        WomensCourseRating =0,
                        WomensSlope =0,
                        UnitOfMeasure = TeeBox.DistanceType.Meters
                    }
                };

                var golfCourses = new[]
                {
                    new GolfCourse{
                        Name = "Gardiners Run",
                        TeeBox = teeBoxes,
                    }
                };
                foreach (var c in golfCourses)
                {
                    context.GolfCourse.Add(c);
                }
            }
                
            //TODO: This is per shot, might be too much
            if (!context.Tag.Any())
            {
                string[] clubTypes = { 
                    "Driver",
                    "2 Wood",
                    "3 Wood",
                    "4 Wood",
                    "5 Wood",
                    "6 Wood",
                    "7 Wood",
                    "8 Wood",
                    "9 Wood",
                    "10 Wood",
                    "11 Wood",
                    "12 Wood",
                    "13 Wood",
                    "14 Wood",
                    "15 Wood",
                    "2 Hybrid",
                    "3 Hybrid",
                    "4 Hybrid",
                    "5 Hybrid",
                    "6 Hybrid",
                    "7 Hybrid",
                    "8 Hybrid",
                    "9 Hybrid",
                    "1 Iron",
                    "2 Iron",
                    "3 Iron",
                    "4 Iron",
                    "5 Iron",
                    "6 Iron",
                    "7 Iron",
                    "8 Iron",
                    "9 Iron",
                    "10 Iron",
                    "Pitching Wedge",
                    "Gap Wedge",
                    "Sand Wedge",
                    "Lob Wedge",
                    "60 Degree Wedge",
                    "64 Degree Wedge",
                    "Chipper",
                    "Putter"
                    };

                foreach (var clubType in clubTypes)
                {
                    context.Tag.Add(new Tag { Label=clubType, TagType=Tag.TagTable.ClubType });
                }
                //TODO: This is per shot, might be too much
                var shotConditionTypes = new[] 
                {
                    "Tee",
                    "Fairway",
                    "Rough",
                    "Water",
                    "Bunker",
                    "Plugged",
                    "Green",
                    "Out of bounds",
                    "Cart path",
                    "Other fairway",
                    "In hole",
                    "Casual Water",
                    "Behind Tree",
                    "Behind Rock",
                    "Wet",
                    "Mud",
                    "No grass",
                    "Dry",
                    "Hay",
                    "Sun in eyes",
                    "Rain",
                    "Snow",
                    "Ice",
                    "Sand",
                    "Hail",
                    "In tree",
                    "Windy",
                    "Blind shot",
                    "Muddy ball",
                    "Cracked ball",
                    "Lost ball",
                    "Near wild life"
                };
                //TODO: This is per shot, might be too much
                var shotTypes = new[] 
                {
                    "Nearest to pin",
                    "Longest drive",
                    "Straightest teeshot",
                    "180 around the cup",
                    "360 around the cup",
                    "Hit flag stick",
                    "Hit people",
                    "Hit golf cart",
                    "Hit golf bag",
                    "Provisional shot",
                    "Draw",
                    "Fade",
                    "Hook",
                    "Slice",
                    "Fat", //chunk, duff
                    "Thin", //Skinny
                    "Low stinger",
                    "Flyer",
                    "Snap hook",
                    "Shank",
                    "High Draw",
                    "High Fade", 
                    "Low Draw",
                    "Low Fade",
                    "Bladed",
                    "Off the toe",
                    "Off the heel",
                    "Sweetspot",
                    "Back spin",
                    "Penetrating ball",
                    "Straight in the cup",
                    "Sky",
                    "Chip'n'run",
                    "Flop",
                    "Cut"

                };

                foreach (var shotType in shotTypes)
                {

                }

            }

            context.SaveChanges();

        }
    }
}