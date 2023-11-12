using HomeManagement.Core.Localization;
using Microsoft.Extensions.Localization;

namespace HomeManagement.UI.Features.Localization;
public class LocalizationService : ILocalizationService
{
    private readonly IStringLocalizer<AppCulture> _localizer;

    public LocalizationService(IStringLocalizer<AppCulture> localizer)
    {
        _localizer = localizer;
    }

    public string GetLocalizedString(string name)
    {
        return _localizer[name];
    }
}
