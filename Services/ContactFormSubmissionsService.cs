using OnatrixUmbraco.ViewModels;
using Umbraco.Cms.Core.Services;

namespace OnatrixUmbraco.Services;

public class ContactFormSubmissionsService(IContentService contentService)
{
    private readonly IContentService _contentService = contentService;

    public bool SaveCallbackRequest(ContactFormViewModel viewModel)
    {
        try
        {
            var container = _contentService.GetRootContent().FirstOrDefault(x => x.ContentType.Alias == "questionSubmissions");
            if (container == null)
                return false;

            var requestName = $"{DateTime.Now:yyyy-MM-dd HH:mm} - {viewModel.Name}";
            var request = _contentService.Create(requestName, container, "askAQuestion");

            request.SetValue("contactName", viewModel.Name);
            request.SetValue("contactEmail", viewModel.Email);
            request.SetValue("contactQuestion", viewModel.Question);

            var saveResult = _contentService.Save(request);
            return saveResult.Success;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return false;
        }

    }
}