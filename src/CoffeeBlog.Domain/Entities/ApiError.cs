using CoffeeBlog.Domain.Entities.DbEntitiesBase;

namespace CoffeeBlog.Domain.Entities;

public class ApiError(string exception,
                            string message,
                            string description,
                            DateTime createdAt) : DbEntityBase
{
    public string Exception { get; set; } = exception;
    public string Message { get; set; } = message;
    public string Description { get; set; } = description;
    public DateTime CreatedAt { get; set; } = createdAt;
}