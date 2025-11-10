using HospitalOne.Application.Common.Exceptions;
using HospitalOne.Application.Common.Interfaces;
using HospitalOne.Domain.Enums;
using HospitalOne.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HospitalOne.Application.Features.Citas.Commands.CreateCita
{
    public class CreateCitaCommandHandler : IRequestHandler<CreateCitaCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateCitaCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateCitaCommand request, CancellationToken cancellationToken)
        {
            // Validar que el cliente existe y está activo
            var clienteExiste = await _context.Clientes
                .AnyAsync(c => c.ClienteID == request.ClienteID && c.Activo, cancellationToken);

            if (!clienteExiste)
                throw new NotFoundException("Cliente", request.ClienteID);

            // Validar que el doctor existe, está activo y disponible
            var doctor = await _context.Doctores
                .FirstOrDefaultAsync(d => d.DoctorID == request.DoctorID, cancellationToken);

            if (doctor == null || !doctor.Activo)
                throw new NotFoundException("Doctor", request.DoctorID);

            if (doctor.EstadoDisponibilidad != EstadoDisponibilidad.Disponible)
                throw new ValidationException(new[] {
                    new FluentValidation.Results.ValidationFailure("DoctorID", "El doctor no está disponible.")
                });

            // Validar que el consultorio existe y está disponible
            var consultorio = await _context.Consultorios
                .FirstOrDefaultAsync(c => c.ConsultorioID == request.ConsultorioID, cancellationToken);

            if (consultorio == null || !consultorio.Activo)
                throw new NotFoundException("Consultorio", request.ConsultorioID);

            if (consultorio.EstadoConsultorio != EstadoConsultorio.Disponible)
                throw new ValidationException(new[] {
                    new FluentValidation.Results.ValidationFailure("ConsultorioID", "El consultorio no está disponible.")
                });

            // Validar que la especialidad existe
            var especialidadExiste = await _context.Especialidades
                .AnyAsync(e => e.EspecialidadID == request.EspecialidadID && e.Activo, cancellationToken);

            if (!especialidadExiste)
                throw new NotFoundException("Especialidad", request.EspecialidadID);

            // Validar que no haya conflictos de horario para el doctor
            var fechaFin = request.FechaCita.AddMinutes(request.DuracionEstimadaMinutos);
            var tieneConflictoDoctor = await _context.Citas
                .AnyAsync(c =>
                    c.DoctorID == request.DoctorID &&
                    c.EstadoCita != EstadoCita.Cancelada &&
                    c.EstadoCita != EstadoCita.NoAsistio &&
                    ((c.FechaCita < fechaFin && c.FechaFinEstimada > request.FechaCita)),
                    cancellationToken);

            if (tieneConflictoDoctor)
                throw new ValidationException(new[] {
                    new FluentValidation.Results.ValidationFailure("FechaCita",
                        "El doctor ya tiene una cita programada en ese horario.")
                });

            // Validar que no haya conflictos de horario para el consultorio
            var tieneConflictoConsultorio = await _context.Citas
                .AnyAsync(c =>
                    c.ConsultorioID == request.ConsultorioID &&
                    c.EstadoCita != EstadoCita.Cancelada &&
                    c.EstadoCita != EstadoCita.NoAsistio &&
                    ((c.FechaCita < fechaFin && c.FechaFinEstimada > request.FechaCita)),
                    cancellationToken);

            if (tieneConflictoConsultorio)
                throw new ValidationException(new[] {
                    new FluentValidation.Results.ValidationFailure("ConsultorioID",
                        "El consultorio ya está ocupado en ese horario.")
                });

            // Crear la cita
            var cita = new Cita
            {
                ClienteID = request.ClienteID,
                DoctorID = request.DoctorID,
                ConsultorioID = request.ConsultorioID,
                EspecialidadID = request.EspecialidadID,
                FechaCita = request.FechaCita,
                FechaFinEstimada = fechaFin,
                TipoCita = request.TipoCita,
                EstadoCita = EstadoCita.Programada,
                Motivo = request.Motivo,
                FechaRegistro = DateTime.Now,
                UsuarioRegistro = request.UsuarioRegistro
            };

            _context.Citas.Add(cita);
            await _context.SaveChangesAsync(cancellationToken);

            return cita.CitaID;
        }
    }
}