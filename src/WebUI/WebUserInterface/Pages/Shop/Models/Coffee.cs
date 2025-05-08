namespace WebUserInterface.Pages.Shop.Models;

public class Coffee
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Species { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Origin { get; set; } = string.Empty;
    public string RoastLevel { get; set; } = string.Empty;
    public string GrindType { get; set; } = string.Empty;
    public string FlavorProfile { get; set; } = string.Empty;
    public string Weight { get; set; } = string.Empty;
    public bool HasCaffeine { get; set; } = true;
    public string ImageUrl { get; set; } = string.Empty;
    public CoffeeType Type { get; set; }
}

public enum CoffeeType
{
    Beans,
    Ground,
    Pads,
    Capsules,
    Instant
}
