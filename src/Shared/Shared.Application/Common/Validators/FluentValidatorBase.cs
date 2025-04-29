using FluentValidation;
using FluentValidation.Results;

namespace Shared.Application.Common.Validators;

public class FluentValidatorBase<T> : AbstractValidator<T> where T : class
{
    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        ValidationResult result = await ValidateAsync(ValidationContext<T>.CreateWithOptions((T)model, x => x.IncludeProperties(propertyName)));
        return result.IsValid ? [] : result.Errors.Select(e => e.ErrorMessage);
    };
}