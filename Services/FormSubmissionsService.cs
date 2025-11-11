using OnatrixUmbraco.ViewModels;
using Umbraco.Cms.Core.Services;

namespace OnatrixUmbraco.Services;

public class FormSubmissionsService(IContentService contentService)
{
    private readonly IContentService _contentService = contentService;

    public bool SaveCallbackRequest(CallbackFormViewModel viewModel)
    {
        try
        {
            var container = _contentService.GetRootContent().FirstOrDefault(x => x.ContentType.Alias == "formSubmissions");
            if (container == null)
                return false;

            var requestName = $"{DateTime.Now:yyyy-MM-dd HH:mm} - {viewModel.Name}";
            var request = _contentService.Create(requestName, container, "callbackRequest");

            request.SetValue("callbackRequestName", viewModel.Name);
            request.SetValue("callbackRequestEmail", viewModel.Email);
            request.SetValue("callbackRequestPhone", viewModel.Phone);
            request.SetValue("callbackRequestOption", viewModel.SelectedOption);

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