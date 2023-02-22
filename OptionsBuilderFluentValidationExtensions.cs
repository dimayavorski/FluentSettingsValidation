using FluentValidation;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace FluentSettingsValidation
{
    public static class OptionsBuilderFluentValidationExtensions
    {
        public static OptionsBuilder<TOptions> ValidateFluent<TOptions>(this OptionsBuilder<TOptions> optionsBuilder) where TOptions : class
        {
            optionsBuilder.Services.AddSingleton<IValidateOptions<TOptions>>(new FluentValidateOptions<TOptions>(
                optionsBuilder.Name, optionsBuilder.Services.BuildServiceProvider().GetRequiredService<IValidator<TOptions>>()));
            return optionsBuilder;
        }
    }

    public class FluentValidateOptions<TOptions> : IValidateOptions<TOptions> where TOptions : class
    {
        private readonly IValidator<TOptions> _validator;
        public FluentValidateOptions(string? name, IValidator<TOptions> validator)
        {
            Name = name;
            _validator = validator;
        }

        public string? Name { get; }

        public ValidateOptionsResult Validate(string? name, TOptions options)
        {
            // Null name is used to configure all named options.
            if (Name != null && Name != name)
            {
                // Ignored if not validating this instance.
                return ValidateOptionsResult.Skip;
            }

            ArgumentNullException.ThrowIfNull(options);

            var validationResult = _validator.Validate(options);
            if(validationResult.IsValid)
            {
                return ValidateOptionsResult.Success;
            }

            var errors = new List<string>();
            foreach (var result in validationResult.Errors)
            {
                errors.Add($"Settings validation failed for {result.PropertyName} with error: {result.ErrorMessage}");
            }

            return ValidateOptionsResult.Fail(errors);
        }
    }
}
