using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BirdieBook.Models;
using BirdieBook.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BirdieBook.ViewComponents
{
    public class UserScoreFormViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(UserScore userScore)
        {

            // ReSharper disable once Mvc.ViewComponentViewNotResolved
            return View(userScore);

        }
    }
}
