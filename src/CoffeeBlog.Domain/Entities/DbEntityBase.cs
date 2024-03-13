using System.ComponentModel.DataAnnotations;

namespace CoffeeBlog.Domain.Entities.DbEntitiesBase;

/// <summary>
/// Represents a base class for database entities.
/// <para>It contains the <see cref="Id"/> property, which is the primary key of the entity.</para>
/// <para>It is abstract. All entities should inherit from this class.</para>
/// </summary>
public abstract class DbEntityBase
{
    /// <summary>
    /// Primary key of the entity.
    /// </summary>
    [Key]
    public int Id { get; set; }
}