using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BirdieBook.Models;
using BirdieBook.ViewModels;

namespace BirdieBook.ViewComponents
{
    public class TeeBoxSelectorViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(List<TeeBox> teeBoxes)
        {
            
            return View(teeBoxes);
        }
    }
}
