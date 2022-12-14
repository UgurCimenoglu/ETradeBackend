using ETradeBackend.Application.ViewModels.Products;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeBackend.Application.Validators.Product
{
    public class CreateProductValidator : AbstractValidator<VM_Create_Product>
    {
        public CreateProductValidator()
        {
            RuleFor(p => p.Name)
                .Must(x => !String.IsNullOrEmpty(x)).WithMessage("Lütfen ürün ismini boş geçmmeyiniz.")
                .MaximumLength(150).MinimumLength(3).WithMessage("Lütfen ürün adını 3-150 karakter aralığında giriniz.");

            RuleFor(p => p.Stock)
                .GreaterThan(0).WithMessage("Stok Bilgisi 0'dan küçük olamaz.");


            RuleFor(p => p.Price)
            .GreaterThan(0).WithMessage("Fiyat bilgisi sıfırdan küçük olamaz.");
        }

    }
}
