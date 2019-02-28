using FluentValidation;

namespace KatlaSport.Services.ProductManagement
{
    /// <summary>
    /// Represents a validator for <see cref="UpdateProductStoreItemRequest"/>.
    /// </summary>
    public class UpdateProductStoreItemRequestValidator : AbstractValidator<UpdateProductStoreItemRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateProductStoreItemRequestValidator"/> class.
        /// </summary>
        public UpdateProductStoreItemRequestValidator()
        {
            RuleFor(r => r.HiveSectionId).GreaterThan(0);
            RuleFor(r => r.ProductId).GreaterThan(0);
            RuleFor(r => r.Quantity).GreaterThan(0);
        }
    }
}
