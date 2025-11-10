using FluentValidation;

namespace HospitalOne.Application.Features.Consultorios.Commands.CreateConsultorio
{
    public class CreateConsultorioCommandValidator : AbstractValidator<CreateConsultorioCommand>
    {
        public CreateConsultorioCommandValidator()
        {
            RuleFor(v => v.NumeroConsultorio)
                .NotEmpty().WithMessage("El número de consultorio es requerido.")
                .MaximumLength(20).WithMessage("El número de consultorio no debe exceder 20 caracteres.");

            RuleFor(v => v.Piso)
                .GreaterThanOrEqualTo(0).WithMessage("El piso debe ser un número válido.")
                .LessThan(100).WithMessage("El piso debe ser menor a 100.");

            RuleFor(v => v.EdificioAla)
                .MaximumLength(50).When(v => !string.IsNullOrEmpty(v.EdificioAla))
                .WithMessage("El edificio/ala no debe exceder 50 caracteres.");

            RuleFor(v => v.TipoConsultorio)
                .MaximumLength(50).When(v => !string.IsNullOrEmpty(v.TipoConsultorio))
                .WithMessage("El tipo de consultorio no debe exceder 50 caracteres.");
        }
    }
}