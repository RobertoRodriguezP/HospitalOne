using HospitalOne.Application.Common.Exceptions;
using HospitalOne.Application.Common.Interfaces;
using HospitalOne.Domain.Enums;
using HospitalOne.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HospitalOne.Application.Features.Doctores.Commands.CreateDoctor
{
    public class CreateDoctorCommandHandler : IRequestHandler<CreateDoctorCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateDoctorCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateDoctorCommand request, CancellationToken cancellationToken)
        {
            // Validar que la especialidad existe
            var especialidadExiste = await _context.Especialidades
                .AnyAsync(e => e.EspecialidadID == request.EspecialidadID && e.Activo, cancellationToken);

            if (!especialidadExiste)
                throw new NotFoundException("Especialidad", request.EspecialidadID);

            // Validar que no exista otro doctor con el mismo número de licencia
            var licenciaExiste = await _context.Doctores
                .AnyAsync(d => d.NumeroLicencia == request.NumeroLicencia, cancellationToken);

            if (licenciaExiste)
                throw new ValidationException(new[] {
                    new FluentValidation.Results.ValidationFailure("NumeroLicencia",
                        "Ya existe un doctor con este número de licencia.")
                });

            var doctor = new Doctor
            {
                Nombres = request.Nombres,
                Apellidos = request.Apellidos,
                DocumentoIdentidad = request.DocumentoIdentidad,
                Telefono = request.Telefono,
                Email = request.Email,
                NumeroLicencia = request.NumeroLicencia,
                EspecialidadID = request.EspecialidadID,
                FechaContratacion = request.FechaContratacion,
                EstadoDisponibilidad = EstadoDisponibilidad.Disponible,
                Activo = true,
                FechaRegistro = DateTime.Now
            };

            _context.Doctores.Add(doctor);
            await _context.SaveChangesAsync(cancellationToken);

            return doctor.DoctorID;
        }
    }
}