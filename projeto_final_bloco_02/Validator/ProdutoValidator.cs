using FluentValidation;
using projeto_final_bloco_02.Models;

namespace projeto_final_bloco_02.Validator
{
    public class ProdutoValidator : AbstractValidator<Produto>
    {
        public ProdutoValidator()
        {
            RuleFor(n => n.Nome)
            .NotEmpty()
            .MinimumLength(5)
            .MaximumLength(255);

            RuleFor(d => d.Descricao)
            .NotEmpty()
            .MinimumLength(20)
            .MaximumLength(700);

            RuleFor(f => f.Fabricante)
            .NotEmpty()
            .MinimumLength(5)
            .MaximumLength(300);

            RuleFor(f => f.Foto)
            .MaximumLength(5000);
        }
    }
}