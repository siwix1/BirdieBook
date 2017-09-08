using BirdieBook.Models;
using System;
using System.Linq;

namespace BirdieBook.Data
{
    public static class DbInitializer
    {
        public static void Initialize(BirdieBookContext context)
        {
            context.Database.EnsureCreated();

            // Look for any Golf holes.
            if (context.Hole.Any())
            {
                return;   // DB has been seeded
            }


            var holes = new Hole[]
            {
            new Hole{ HoleNumber = 1, Par = 4, Length = 365, HCPIndex = 1},
            new Hole{ HoleNumber = 2, Par = 5, Length = 471, HCPIndex = 12},
            new Hole{ HoleNumber = 3, Par = 3, Length = 141, HCPIndex = 14},
            new Hole{ HoleNumber = 4, Par = 4, Length = 325, HCPIndex = 2},
            new Hole{ HoleNumber = 5, Par = 4, Length = 264, HCPIndex = 18},
            new Hole{ HoleNumber = 6, Par = 4, Length = 312, HCPIndex = 3},
            new Hole{ HoleNumber = 7, Par = 3, Length = 149, HCPIndex = 8},
            new Hole{ HoleNumber = 8, Par = 5, Length = 459, HCPIndex = 13},
            new Hole{ HoleNumber = 9, Par = 4, Length = 339, HCPIndex = 7},
            new Hole{ HoleNumber = 10, Par = 5, Length = 458, HCPIndex = 9},
            new Hole{ HoleNumber = 11, Par = 3, Length = 136, HCPIndex = 11},
            new Hole{ HoleNumber = 12, Par = 4, Length = 362, HCPIndex = 5},
            new Hole{ HoleNumber = 13, Par = 4, Length = 325, HCPIndex = 10},
            new Hole{ HoleNumber = 14, Par = 4, Length = 341, HCPIndex = 6},
            new Hole{ HoleNumber = 15, Par = 4, Length = 294, HCPIndex = 17},
            new Hole{ HoleNumber = 16, Par = 3, Length = 131, HCPIndex = 15},
            new Hole{ HoleNumber = 17, Par = 5, Length = 451, HCPIndex = 16},
            new Hole{ HoleNumber = 18, Par = 4, Length = 353, HCPIndex = 4},

            };

            var teeBoxes = new TeeBox[]
            {
                new TeeBox
                {
                    Name = "White",
                    MensCourseRating =70,
                    MensSlope = 129,
                    holes =holes,
                    WomensCourseRating =0,
                    WomensSlope =0,
                    UnitOfMeasure = TeeBox.DistanceType.Meters
                }
            };

            var GolfCourses = new GolfCourse[]
            {
                new GolfCourse{
                    Name = "Gardiners Run",
                    teeBox = teeBoxes,
                }
            };
            foreach (GolfCourse c in GolfCourses)
            {
                context.GolfCourse.Add(c);
            }
            context.SaveChanges();

        }
    }
}