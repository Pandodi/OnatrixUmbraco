using Microsoft.AspNetCore.Mvc;
using OnatrixUmbraco.Services;
using OnatrixUmbraco.ViewModels;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Web.Website.Controllers;

namespace OnatrixUmbraco.Controllers;

public class ContactController(IUmbracoContextAccessor umbracoContextAccessor, IUmbracoDatabaseFactory databaseFactory, ServiceContext services, AppCaches appCaches, IProfilingLogger profilingLogger, IPublishedUrlProvider publishedUrlProvider, ContactFormSubmissionsService contactFormSubmissionsService) : SurfaceController(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
{
    private readonly ContactFormSubmissionsService _contactFormSubmissionsService = contactFormSubmissionsService;

    public IActionResult HandleQuestionForm(ContactFormViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return CurrentUmbracoPage();
        }

        var result = _contactFormSubmissionsService.SaveCallbackRequest(viewModel);
        if (!result)
        {
            TempData["FormError"] = "Something went wrong. Try again later.";
            return RedirectToCurrentUmbracoPage();
        }

        TempData["FormSuccess"] = "Thank you for your request! We will get back to you as soon as possible.";

        return RedirectToCurrentUmbracoPage();
    }
}
