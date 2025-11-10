using FluentValidation;

namespace HospitalOne.Application.Features.Citas.Commands.CreateCita
{
    public class CreateCitaCommandValidator : AbstractValidator<CreateCitaCommand>
    {
        public CreateCitaCommandValidator()
        {
            RuleFor(v => v.ClienteID)
                .GreaterThan(0).WithMessage("El ID del cliente debe ser válido.");

            RuleFor(v => v.DoctorID)
                .GreaterThan(0).WithMessage("El ID del doctor debe ser válido.");

            RuleFor(v => v.ConsultorioID)
                .GreaterThan(0).WithMessage("El ID del consultorio debe ser válido.");

            RuleFor(v => v.EspecialidadID)
                .GreaterThan(0).WithMessage("El ID de la especialidad debe ser válido.");

            RuleFor(v => v.FechaCita)
                .NotEmpty().WithMessage("La fecha de la cita es requerida.")
                .GreaterThan(DateTime.Now).WithMessage("La fecha de la cita debe ser futura.");

            RuleFor(v => v.DuracionEstimadaMinutos)
                .GreaterThan(0).WithMessage("La duración debe ser mayor a 0.")
                .LessThanOrEqualTo(480).WithMessage("La duración no puede exceder 8 horas.");

            RuleFor(v => v.Motivo)
                .MaximumLength(500).When(v => !string.IsNullOrEmpty(v.Motivo))
                .WithMessage("El motivo no debe exceder 500 caracteres.");
        }
    }
}