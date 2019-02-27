using FluentValidation;

namespace KatlaSport.Services.ProductManagement
{
    /// <summary>
    /// Represents a validator for <see cref="UpdateRequestRequest"/>.
    /// </summary>
    public class UpdateRequestRequestValidator : AbstractValidator<UpdateRequestRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateRequestRequestValidator"/> class.
        /// </summary>
        public UpdateRequestRequestValidator()
        {
            RuleFor(r => r.HiveSectionId).GreaterThan(0);
            RuleFor(r => r.ProductId).GreaterThan(0);
            RuleFor(r => r.Quantity).GreaterThan(0);
        }
    }
}
