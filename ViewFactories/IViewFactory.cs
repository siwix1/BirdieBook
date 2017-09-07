using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BirdieBook.ViewFactories
{
    //http://benfoster.io/blog/using-the-view-factory-pattern-in-aspnet-mvc
    public interface IViewFactory 
    {
        TView CreateView<TView>();
        TView CreateView<TInput, TView>(TInput input);
    }
}
