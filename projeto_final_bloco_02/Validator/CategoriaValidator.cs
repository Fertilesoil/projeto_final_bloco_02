using FluentValidation;
using projeto_final_bloco_02.Models;

namespace projeto_final_bloco_02.Validator
{
    public class CategoriaValidator : AbstractValidator<Categoria>
    {
        public CategoriaValidator()
        {
            RuleFor(d => d.Descricao)
            .NotEmpty()
            .MaximumLength(255);

            RuleFor(s => s.Setor)
            .NotEmpty()
            .MaximumLength(255);
        }
    }
}