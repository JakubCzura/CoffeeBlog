using AutoMapper;

namespace CoffeeBlog.Application.Handlers.Basics;

/// <summary>
/// Base command handler for command handlers that require mapping.
/// </summary>
/// <param name="_mapper">AutoMapper to map classes.</param>
public class CommandHandlerBase(IMapper _mapper)
{
    /// <summary>
    /// Mapper to map classes.
    /// </summary>
    protected readonly IMapper Mapper = _mapper;
}