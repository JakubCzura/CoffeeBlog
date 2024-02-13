namespace CoffeeBlog.API.ExtensionMethods;

public static class RequestExtensions
{
    public static async Task<string> ReadAsStringAsync(this Stream body)
        => await new StreamReader(body).ReadToEndAsync();
}