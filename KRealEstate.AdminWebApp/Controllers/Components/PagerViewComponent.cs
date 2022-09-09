using KRealEstate.ViewModels.Common;
using Microsoft.AspNetCore.Mvc;

namespace KRealEstate.AdminWebApp.Controllers.Components
{
    public class PagerViewComponent : ViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync(PageResultPage result)
        {
            return Task.FromResult((IViewComponentResult)View("Default", result));
        }
    }
}
