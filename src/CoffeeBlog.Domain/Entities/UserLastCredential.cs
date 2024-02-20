using CoffeeBlog.Domain.Entities.DbEntitiesBase;

namespace CoffeeBlog.Domain.Entities;

/// <summary>
/// Class to store the last credentials of the user. 
/// It is used to prevent the user from using the same credentials again.
/// </summary>
public class UserLastCredential : DbEntityBase
{
    public string? LastPassword1 { get; set; }
    public string? LastPassword2 { get; set; }
    public string? LastPassword3 { get; set; }

    public int UserId { get; set; }
}