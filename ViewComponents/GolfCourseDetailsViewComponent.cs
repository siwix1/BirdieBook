using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BirdieBook.Models;
using BirdieBook.ViewModels;


namespace BirdieBook.ViewComponents
{
    public class GolfCourseDetailsViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(GolfCourseDetails golfCourseDetails)
        {

            // ReSharper disable once Mvc.ViewComponentViewNotResolved
            return View(golfCourseDetails);

        }
    }
}
