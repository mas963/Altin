using Altin.Application.Models.Product;
using FluentValidation;

namespace Altin.Application.Models.Validators.Product;

public class ProductUpdateReqValidator : AbstractValidator<ProductUpdateReq>
{
    public ProductUpdateReqValidator()
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
    }
}