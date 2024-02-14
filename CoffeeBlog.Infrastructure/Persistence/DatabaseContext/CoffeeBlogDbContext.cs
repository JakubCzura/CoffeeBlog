using Microsoft.EntityFrameworkCore;

namespace CoffeeBlog.Infrastructure.Persistence.DatabaseContext;

public class CoffeeBlogDbContext(DbContextOptions<CoffeeBlogDbContext> options) : DbContext(options)
{
}