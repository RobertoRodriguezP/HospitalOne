using FluentValidation;

namespace HospitalOne.Application.Features.Clientes.Commands.CreateCliente
{
    public class CreateClienteCommandValidator : AbstractValidator<CreateClienteCommand>
    {
        public CreateClienteCommandValidator()
        {
            RuleFor(v => v.Nombres)
                .NotEmpty().WithMessage("El nombre es requerido.")
                .MaximumLength(100).WithMessage("El nombre no debe exceder 100 caracteres.");

            RuleFor(v => v.Apellidos)
                .NotEmpty().WithMessage("Los apellidos son requeridos.")
                .MaximumLength(100).WithMessage("Los apellidos no deben exceder 100 caracteres.");

            RuleFor(v => v.FechaNacimiento)
                .NotEmpty().WithMessage("La fecha de nacimiento es requerida.")
                .LessThan(DateTime.Now).WithMessage("La fecha de nacimiento debe ser anterior a hoy.")
                .GreaterThan(DateTime.Now.AddYears(-120)).WithMessage("La fecha de nacimiento no es válida.");

            RuleFor(v => v.DocumentoIdentidad)
                .NotEmpty().WithMessage("El documento de identidad es requerido.")
                .MaximumLength(20).WithMessage("El documento no debe exceder 20 caracteres.");

            RuleFor(v => v.Email)
                .EmailAddress().When(v => !string.IsNullOrEmpty(v.Email))
                .WithMessage("El formato del email no es válido.");

            RuleFor(v => v.Telefono)
                .MaximumLength(20).When(v => !string.IsNullOrEmpty(v.Telefono))
                .WithMessage("El teléfono no debe exceder 20 caracteres.");
        }
    }
}