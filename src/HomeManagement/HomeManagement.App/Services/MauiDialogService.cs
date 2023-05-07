using HomeManagement.Business.Common.Interfaces;

namespace HomeManagement.App.Services;
public class MauiDialogService : IDialogService
{
    public Task DisplayAlert(string title, string message, string buttonText) => Application.Current.MainPage.DisplayAlert(title, message, buttonText);

    public Task<bool> DisplayAlert(string title, string message, string acceptButtonText, string cancelButtonText) => Application.Current.MainPage.DisplayAlert(title, message, acceptButtonText, cancelButtonText);
}
