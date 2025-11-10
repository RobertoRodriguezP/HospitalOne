using HospitalOne.Application.Common.Exceptions;
using HospitalOne.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HospitalOne.Application.Features.Consultorios.Commands.AsignarDoctorConsultorio
{
    public class AsignarDoctorConsultorioCommandHandler : IRequestHandler<AsignarDoctorConsultorioCommand, Unit>
    {
        private readonly IApplicationDbContext _context;

        public AsignarDoctorConsultorioCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(AsignarDoctorConsultorioCommand request, CancellationToken cancellationToken)
        {
            // Validar que el consultorio existe
            var consultorio = await _context.Consultorios
                .FirstOrDefaultAsync(c => c.ConsultorioID == request.ConsultorioID, cancellationToken);

            if (consultorio == null || !consultorio.Activo)
                throw new NotFoundException("Consultorio", request.ConsultorioID);

            // Validar que el doctor existe
            var doctorExiste = await _context.Doctores
                .AnyAsync(d => d.DoctorID == request.DoctorID && d.Activo, cancellationToken);

            if (!doctorExiste)
                throw new NotFoundException("Doctor", request.DoctorID);

            // Asignar el doctor al consultorio
            consultorio.DoctorAsignadoID = request.DoctorID;
            consultorio.FechaAsignacionDoctor = DateTime.Now;
            consultorio.UltimaActualizacion = DateTime.Now;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}