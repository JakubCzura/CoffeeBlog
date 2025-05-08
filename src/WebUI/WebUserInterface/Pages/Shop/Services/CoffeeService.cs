using WebUserInterface.Pages.Shop.Models;

namespace WebUserInterface.Pages.Shop.Services;

public class CoffeeService
{
    private readonly List<Coffee> _coffees;
    private readonly List<ShopLocation> _shopLocations;

    public CoffeeService()
    {
        _coffees = GetCoffees();
        _shopLocations = GetShopLocations();
    }

    public Task<List<Coffee>> GetCoffeesAsync(string? searchString = null, string? sortBy = null, bool ascending = true, int page = 1, int pageSize = 6)
    {
        IEnumerable<Coffee> coffees = _coffees;

        // Apply search filter
        if (!string.IsNullOrWhiteSpace(searchString))
        {
            coffees = coffees.Where(c =>
                c.Name.Contains(searchString, System.StringComparison.OrdinalIgnoreCase) ||
                c.Description.Contains(searchString, System.StringComparison.OrdinalIgnoreCase) ||
                c.Species.Contains(searchString, System.StringComparison.OrdinalIgnoreCase) ||
                c.Origin.Contains(searchString, System.StringComparison.OrdinalIgnoreCase) ||
                c.RoastLevel.Contains(searchString, System.StringComparison.OrdinalIgnoreCase) ||
                c.FlavorProfile.Contains(searchString, System.StringComparison.OrdinalIgnoreCase));
        }

        // Apply sorting
        if (!string.IsNullOrWhiteSpace(sortBy))
        {
            coffees = sortBy.ToLower() switch
            {
                "name" => ascending ? coffees.OrderBy(c => c.Name) : coffees.OrderByDescending(c => c.Name),
                "price" => ascending ? coffees.OrderBy(c => c.Price) : coffees.OrderByDescending(c => c.Price),
                "origin" => ascending ? coffees.OrderBy(c => c.Origin) : coffees.OrderByDescending(c => c.Origin),
                "type" => ascending ? coffees.OrderBy(c => c.Type) : coffees.OrderByDescending(c => c.Type),
                "roast" => ascending ? coffees.OrderBy(c => c.RoastLevel) : coffees.OrderByDescending(c => c.RoastLevel),
                _ => coffees
            };
        }

        // Apply pagination
        int skip = (page - 1) * pageSize;
        coffees = coffees.Skip(skip).Take(pageSize);

        return Task.FromResult(coffees.ToList());
    }

    public Task<int> GetTotalCoffeesCountAsync(string? searchString = null)
    {
        IEnumerable<Coffee> coffees = _coffees;

        // Apply search filter for count
        if (!string.IsNullOrWhiteSpace(searchString))
        {
            coffees = coffees.Where(c =>
                c.Name.Contains(searchString, System.StringComparison.OrdinalIgnoreCase) ||
                c.Description.Contains(searchString, System.StringComparison.OrdinalIgnoreCase) ||
                c.Species.Contains(searchString, System.StringComparison.OrdinalIgnoreCase) ||
                c.Origin.Contains(searchString, System.StringComparison.OrdinalIgnoreCase) ||
                c.RoastLevel.Contains(searchString, System.StringComparison.OrdinalIgnoreCase) ||
                c.FlavorProfile.Contains(searchString, System.StringComparison.OrdinalIgnoreCase));
        }

        return Task.FromResult(coffees.Count());
    }

    public Task<List<ShopLocation>> GetShopLocationsAsync()
    {
        return Task.FromResult(_shopLocations);
    }

    private static List<Coffee> GetCoffees()
    {
        return new List<Coffee>
        {
            new Coffee
            {
                Id = 1,
                Name = "Ethiopian Yirgacheffe",
                Species = "Arabica",
                Price = 24.99m,
                Description = "A bright and vibrant coffee with floral notes, citrus acidity, and a delicate sweetness reminiscent of bergamot and jasmine.",
                Origin = "Ethiopia, Yirgacheffe region",
                RoastLevel = "Light",
                GrindType = "Whole Beans",
                FlavorProfile = "Floral, Citrus, Jasmine",
                Weight = "250g",
                HasCaffeine = true,
                ImageUrl = "images/shop/coffee-ethiopia.jpg",
                Type = CoffeeType.Beans
            },
            new Coffee
            {
                Id = 2,
                Name = "Colombian Supremo",
                Species = "Arabica",
                Price = 21.99m,
                Description = "A well-balanced coffee with a medium body, sweet caramel notes, and mild acidity, finishing with hints of nuts and chocolate.",
                Origin = "Colombia, Huila region",
                RoastLevel = "Medium",
                GrindType = "Ground for Filter",
                FlavorProfile = "Caramel, Nuts, Chocolate",
                Weight = "500g",
                HasCaffeine = true,
                ImageUrl = "images/shop/coffee-colombia.jpg",
                Type = CoffeeType.Ground
            },
            new Coffee
            {
                Id = 3,
                Name = "Sumatra Mandheling",
                Species = "Arabica",
                Price = 23.99m,
                Description = "A full-bodied, earthy coffee with low acidity and complex flavors of dark chocolate, cedar, and spices.",
                Origin = "Indonesia, Sumatra",
                RoastLevel = "Dark",
                GrindType = "Whole Beans",
                FlavorProfile = "Earthy, Dark Chocolate, Cedar",
                Weight = "250g",
                HasCaffeine = true,
                ImageUrl = "images/shop/coffee-sumatra.jpg",
                Type = CoffeeType.Beans
            },
            new Coffee
            {
                Id = 4,
                Name = "Arabica Pads Box",
                Species = "Arabica Blend",
                Price = 18.50m,
                Description = "A convenient pack of premium coffee pads. Each pad contains the perfect amount of coffee for a single cup, with a balanced flavor profile and medium body.",
                Origin = "Multi-origin Blend",
                RoastLevel = "Medium",
                GrindType = "Pads",
                FlavorProfile = "Balanced, Smooth, Caramel",
                Weight = "Box of 18 pads",
                HasCaffeine = true,
                ImageUrl = "images/shop/coffee-pads.jpg",
                Type = CoffeeType.Pads
            },
            new Coffee
            {
                Id = 5,
                Name = "Costa Rican Tarrazu",
                Species = "Arabica",
                Price = 26.99m,
                Description = "A clean and crisp coffee with bright acidity, medium body, and notes of honey, orange, and almond.",
                Origin = "Costa Rica, Tarrazu region",
                RoastLevel = "Medium-Light",
                GrindType = "Ground for Espresso",
                FlavorProfile = "Honey, Orange, Almond",
                Weight = "250g",
                HasCaffeine = true,
                ImageUrl = "images/shop/coffee-costa-rica.jpg",
                Type = CoffeeType.Ground
            },
            new Coffee
            {
                Id = 6,
                Name = "Brazilian Santos",
                Species = "Arabica",
                Price = 19.99m,
                Description = "A smooth, mild coffee with nutty notes, low acidity, and a sweet finish with hints of chocolate.",
                Origin = "Brazil, Santos region",
                RoastLevel = "Medium",
                GrindType = "Whole Beans",
                FlavorProfile = "Nutty, Sweet, Chocolate",
                Weight = "500g",
                HasCaffeine = true,
                ImageUrl = "images/shop/coffee-brazil.jpg",
                Type = CoffeeType.Beans
            },
            new Coffee
            {
                Id = 7,
                Name = "Decaf Espresso Blend",
                Species = "Arabica Blend",
                Price = 22.99m,
                Description = "A Swiss Water Process decaffeinated coffee with the rich, bold flavor of traditional espresso without the caffeine.",
                Origin = "Multi-origin Blend",
                RoastLevel = "Dark",
                GrindType = "Ground for Espresso",
                FlavorProfile = "Rich, Cocoa, Caramel",
                Weight = "250g",
                HasCaffeine = false,
                ImageUrl = "images/shop/coffee-decaf.jpg",
                Type = CoffeeType.Ground
            },
            new Coffee
            {
                Id = 8,
                Name = "Kenya AA",
                Species = "Arabica",
                Price = 28.99m,
                Description = "A bright, vibrant coffee with complex acidity, full body, and fruity notes of blackcurrant and blackberry.",
                Origin = "Kenya, Central Highlands",
                RoastLevel = "Medium",
                GrindType = "Whole Beans",
                FlavorProfile = "Blackcurrant, Blackberry, Wine-like",
                Weight = "250g",
                HasCaffeine = true,
                ImageUrl = "images/shop/coffee-kenya.jpg",
                Type = CoffeeType.Beans
            },
            new Coffee
            {
                Id = 9,
                Name = "Premium Coffee Capsules",
                Species = "Arabica Blend",
                Price = 24.50m,
                Description = "Compatible coffee capsules designed for popular machines. Each capsule delivers a perfect espresso with rich crema and balanced flavor.",
                Origin = "Multi-origin Blend",
                RoastLevel = "Medium-Dark",
                GrindType = "Capsules",
                FlavorProfile = "Balanced, Rich, Cocoa",
                Weight = "Box of 20 capsules",
                HasCaffeine = true,
                ImageUrl = "images/shop/coffee-capsules.jpg",
                Type = CoffeeType.Capsules
            },
            new Coffee
            {
                Id = 10,
                Name = "Jamaican Blue Mountain",
                Species = "Arabica",
                Price = 45.99m,
                Description = "A rare, luxurious coffee with exceptional smoothness, mild flavor, and clean finish with notes of nuts and cocoa.",
                Origin = "Jamaica, Blue Mountains",
                RoastLevel = "Medium",
                GrindType = "Whole Beans",
                FlavorProfile = "Smooth, Mild, Nutty",
                Weight = "200g",
                HasCaffeine = true,
                ImageUrl = "images/shop/coffee-jamaica.jpg",
                Type = CoffeeType.Beans
            },
            new Coffee
            {
                Id = 11,
                Name = "Guatemala Antigua",
                Species = "Arabica",
                Price = 25.99m,
                Description = "A refined coffee with complex acidity, full body, and notes of chocolate, spice, and smoke.",
                Origin = "Guatemala, Antigua Valley",
                RoastLevel = "Medium-Dark",
                GrindType = "Ground for Pour Over",
                FlavorProfile = "Chocolate, Spice, Smoke",
                Weight = "250g",
                HasCaffeine = true,
                ImageUrl = "images/shop/coffee-guatemala.jpg",
                Type = CoffeeType.Ground
            },
            new Coffee
            {
                Id = 12,
                Name = "Instant Premium Blend",
                Species = "Arabica/Robusta Blend",
                Price = 15.99m,
                Description = "A high-quality instant coffee made from freeze-dried premium coffee beans. Quick to prepare with surprisingly rich and smooth taste.",
                Origin = "Multi-origin Blend",
                RoastLevel = "Medium",
                GrindType = "Instant",
                FlavorProfile = "Rich, Smooth, Balanced",
                Weight = "100g jar",
                HasCaffeine = true,
                ImageUrl = "images/shop/coffee-instant.jpg",
                Type = CoffeeType.Instant
            },
            new Coffee
            {
                Id = 13,
                Name = "Vietnamese Robusta",
                Species = "Robusta",
                Price = 20.99m,
                Description = "A strong, bold coffee with high caffeine content, intense flavor, and notes of dark chocolate and nuts.",
                Origin = "Vietnam, Central Highlands",
                RoastLevel = "Dark",
                GrindType = "Ground for French Press",
                FlavorProfile = "Bold, Dark Chocolate, Woody",
                Weight = "250g",
                HasCaffeine = true,
                ImageUrl = "images/shop/coffee-vietnam.jpg",
                Type = CoffeeType.Ground
            },
            new Coffee
            {
                Id = 14,
                Name = "Decaf Colombian",
                Species = "Arabica",
                Price = 23.99m,
                Description = "A Swiss Water Process decaffeinated coffee that preserves the distinctive flavor of Colombian beans with sweet caramel notes.",
                Origin = "Colombia",
                RoastLevel = "Medium",
                GrindType = "Whole Beans",
                FlavorProfile = "Caramel, Mild, Clean",
                Weight = "250g",
                HasCaffeine = false,
                ImageUrl = "images/shop/coffee-decaf-colombia.jpg",
                Type = CoffeeType.Beans
            },
            new Coffee
            {
                Id = 15,
                Name = "Espresso Blend",
                Species = "Arabica/Robusta Blend",
                Price = 22.99m,
                Description = "A perfect espresso blend combining the flavor complexity of Arabica with the crema-enhancing properties of Robusta.",
                Origin = "Multi-origin Blend",
                RoastLevel = "Dark",
                GrindType = "Ground for Espresso",
                FlavorProfile = "Rich, Chocolate, Hazelnut",
                Weight = "250g",
                HasCaffeine = true,
                ImageUrl = "images/shop/coffee-espresso.jpg",
                Type = CoffeeType.Ground
            },
            new Coffee
            {
                Id = 16,
                Name = "Honduras Organic",
                Species = "Arabica",
                Price = 24.99m,
                Description = "A certified organic coffee with a balanced profile, medium body, and notes of caramel, nuts, and citrus.",
                Origin = "Honduras, Marcala region",
                RoastLevel = "Medium",
                GrindType = "Whole Beans",
                FlavorProfile = "Caramel, Nuts, Citrus",
                Weight = "250g",
                HasCaffeine = true,
                ImageUrl = "images/shop/coffee-honduras.jpg",
                Type = CoffeeType.Beans
            },
            new Coffee
            {
                Id = 17,
                Name = "Italian Style Coffee Pads",
                Species = "Arabica/Robusta Blend",
                Price = 19.50m,
                Description = "Coffee pads designed for a strong, rich Italian-style coffee experience. Ideal for those who prefer an intense flavor.",
                Origin = "Multi-origin Blend",
                RoastLevel = "Dark",
                GrindType = "Pads",
                FlavorProfile = "Intense, Rich, Cocoa",
                Weight = "Box of 18 pads",
                HasCaffeine = true,
                ImageUrl = "images/shop/coffee-pads-italian.jpg",
                Type = CoffeeType.Pads
            },
            new Coffee
            {
                Id = 18,
                Name = "Mexican Chiapas",
                Species = "Arabica",
                Price = 23.99m,
                Description = "A medium-bodied coffee with a mild acidity and pleasant notes of nuts, chocolate, and a subtle hint of spice.",
                Origin = "Mexico, Chiapas region",
                RoastLevel = "Medium",
                GrindType = "Whole Beans",
                FlavorProfile = "Nutty, Chocolate, Mild Spice",
                Weight = "250g",
                HasCaffeine = true,
                ImageUrl = "images/shop/coffee-mexico.jpg",
                Type = CoffeeType.Beans
            }
        };
    }

    private static List<ShopLocation> GetShopLocations()
    {
        return new List<ShopLocation>
        {
            new ShopLocation
            {
                Id = 1,
                Name = "Coffee Blog Flagship Store",
                Address = "Silent Street 10333/13",
                City = "Warsaw",
                PostalCode = "0300-0361",
                PhoneNumber = "+480 22 123 45 67 99",
                Email = "warsaw@coffeeblog.com",
                OpeningHours = "Mon-Fri: 7:00-20:00, Sat-Sun: 8:00-19:00",
                MapUrl = "https://goo.gl/maps/example1"
            },
            new ShopLocation
            {
                Id = 2,
                Name = "Coffee Blog Cracow",
                Address = "Old Town 900/220",
                City = "Cracow",
                PostalCode = "0031-0429",
                PhoneNumber = "+480 12 987 65 43 99",
                Email = "krakow@coffeeblog.com",
                OpeningHours = "Mon-Fri: 7:30-21:00, Sat-Sun: 8:30-20:00",
                MapUrl = "https://goo.gl/maps/example2"
            },
            new ShopLocation
            {
                Id = 3,
                Name = "Coffee Blog Wroclaw",
                Address = "Smiling Street 12909",
                City = "Wroclaw",
                PostalCode = "0050-0669",
                PhoneNumber = "+480 71 876 54 32 99",
                Email = "wroclaw@coffeeblog.com",
                OpeningHours = "Mon-Sun: 8:00-20:00",
                MapUrl = "https://goo.gl/maps/example3"
            },
            new ShopLocation
            {
                Id = 4,
                Name = "Coffee Blog Gdansk",
                Address = "Long Street 551222",
                City = "Gdansk",
                PostalCode = "9980-8319",
                PhoneNumber = "+480 58 345 67 89 99",
                Email = "gdansk@coffeeblog.com",
                OpeningHours = "Mon-Fri: 7:00-19:00, Sat-Sun: 9:00-18:00",
                MapUrl = "https://goo.gl/maps/example4"
            },
            new ShopLocation
            {
                Id = 5,
                Name = "Coffee Blog Poznan",
                Address = "Old Town 373131",
                City = "Poznan",
                PostalCode = "0061-7729",
                PhoneNumber = "+480 61 234 56 78 99",
                Email = "poznan@coffeeblog.com",
                OpeningHours = "Mon-Sun: 7:30-20:30",
                MapUrl = "https://goo.gl/maps/example5"
            }
        };
    }
}
