namespace HomeManagement.Business.Common.Interfaces;
public interface IDialogService
{
    public Task DisplayAlert(string title, string message, string buttonText);
    public Task<bool> DisplayAlert(string title, string message, string acceptButtonText, string cancelButtonText);
}
