using FluentValidation;

namespace Altin.Web.Areas.Admin.Models.Validators;

public class ProductUploadViewModelValidator : AbstractValidator<ProductUploadViewModel>
{
    public ProductUploadViewModelValidator()
    {
        RuleFor(x => x.ProductName)
            .NotEmpty()
            .WithMessage("Ürün ismi boş olamaz")
            .MaximumLength(45)
            .WithMessage("Ürün ismi maximum 45 karakter olmalıdır");

        RuleFor(x => x.ProductDescription)
            .NotEmpty()
            .WithMessage("Ürün açıklaması boş olamaz")
            .MaximumLength(200)
            .WithMessage("Ürün tanımı maximum 200 karakter olmalıdır");

        RuleFor(x => x.ProductImage)
            .NotEmpty()
            .WithMessage("Ürün resmi boş olamaz");
    }
}