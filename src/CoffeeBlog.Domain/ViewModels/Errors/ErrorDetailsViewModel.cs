using CoffeeBlog.Domain.ViewModels.Basics;
using System.Text.Json;

namespace CoffeeBlog.Domain.ViewModels.Errors;

public class ErrorDetailsViewModel : ViewModelBase
{
    public int StatusCode { get; set; }

    public ErrorDetailsViewModel() : base()
    {
    }

    public ErrorDetailsViewModel(int statusCode, string message) : base(message) => StatusCode = statusCode;

    public override string ToString()
        => JsonSerializer.Serialize(this, GetType());
}