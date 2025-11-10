using FluentValidation;

namespace HospitalOne.Application.Features.Doctores.Commands.CreateDoctor
{
    public class CreateDoctorCommandValidator : AbstractValidator<CreateDoctorCommand>
    {
        public CreateDoctorCommandValidator()
        {
            RuleFor(v => v.Nombres)
                .NotEmpty().WithMessage("El nombre es requerido.")
                .MaximumLength(100).WithMessage("El nombre no debe exceder 100 caracteres.");

            RuleFor(v => v.Apellidos)
                .NotEmpty().WithMessage("Los apellidos son requeridos.")
                .MaximumLength(100).WithMessage("Los apellidos no deben exceder 100 caracteres.");

            RuleFor(v => v.DocumentoIdentidad)
                .NotEmpty().WithMessage("El documento de identidad es requerido.")
                .MaximumLength(20).WithMessage("El documento no debe exceder 20 caracteres.");

            RuleFor(v => v.NumeroLicencia)
                .NotEmpty().WithMessage("El número de licencia es requerido.")
                .MaximumLength(50).WithMessage("El número de licencia no debe exceder 50 caracteres.");

            RuleFor(v => v.EspecialidadID)
                .GreaterThan(0).WithMessage("Debe seleccionar una especialidad válida.");

            RuleFor(v => v.FechaContratacion)
                .NotEmpty().WithMessage("La fecha de contratación es requerida.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("La fecha de contratación no puede ser futura.");

            RuleFor(v => v.Email)
                .EmailAddress().When(v => !string.IsNullOrEmpty(v.Email))
                .WithMessage("El formato del email no es válido.");

            RuleFor(v => v.Telefono)
                .MaximumLength(20).When(v => !string.IsNullOrEmpty(v.Telefono))
                .WithMessage("El teléfono no debe exceder 20 caracteres.");
        }
    }
}