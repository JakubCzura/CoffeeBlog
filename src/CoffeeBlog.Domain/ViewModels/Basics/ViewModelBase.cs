namespace CoffeeBlog.Domain.ViewModels.Basics;

public class ViewModelBase
{
    public string? ResponseMessage { get; set; } = null;

    public ViewModelBase()
    {
    }

    public ViewModelBase(string? responseMessage) => ResponseMessage = responseMessage;
}