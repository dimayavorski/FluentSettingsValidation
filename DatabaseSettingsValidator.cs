using FluentValidation;
using System.Data;

namespace FluentSettingsValidation
{
    public class DatabaseSettingsValidator: AbstractValidator<DatabaseSettings>
    {
        public DatabaseSettingsValidator()
        {
            RuleFor(p => p.Name).NotNull().NotEmpty();
            RuleFor(p => p.RetryInterval).InclusiveBetween(1, 10);
        }
    }
}
