using HospitalOne.Application.Common.Interfaces;
using HospitalOne.Application.Common.Exceptions;
using HospitalOne.Domain.Enums;
using HospitalOne.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HospitalOne.Application.Features.Consultorios.Commands.CreateConsultorio
{
    public class CreateConsultorioCommandHandler : IRequestHandler<CreateConsultorioCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateConsultorioCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateConsultorioCommand request, CancellationToken cancellationToken)
        {
            // Validar que no exista otro consultorio con el mismo número
            var consultorioExiste = await _context.Consultorios
                .AnyAsync(c => c.NumeroConsultorio == request.NumeroConsultorio, cancellationToken);

            if (consultorioExiste)
                throw new ValidationException(new[] {
                    new FluentValidation.Results.ValidationFailure("NumeroConsultorio",
                        "Ya existe un consultorio con este número.")
                });

            var consultorio = new Consultorio
            {
                NumeroConsultorio = request.NumeroConsultorio,
                Piso = request.Piso,
                EdificioAla = request.EdificioAla,
                TipoConsultorio = request.TipoConsultorio,
                EstadoConsultorio = EstadoConsultorio.Disponible,
                Activo = true,
                FechaCreacion = DateTime.Now,
                UltimaActualizacion = DateTime.Now
            };

            _context.Consultorios.Add(consultorio);
            await _context.SaveChangesAsync(cancellationToken);

            return consultorio.ConsultorioID;
        }
    }
}