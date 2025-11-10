using HospitalOne.Application.Common.Exceptions;
using HospitalOne.Application.Common.Interfaces;
using HospitalOne.Application.Features.Clientes.Common;
using HospitalOne.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HospitalOne.Application.Features.Clientes.Queries.GetClienteById
{
    public class GetClienteByIdQueryHandler : IRequestHandler<GetClienteByIdQuery, ClienteDto>
    {
        private readonly IApplicationDbContext _context;

        public GetClienteByIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ClienteDto> Handle(GetClienteByIdQuery request, CancellationToken cancellationToken)
        {
            var cliente = await _context.Clientes
                .AsNoTracking()
                .Where(c => c.ClienteID == request.ClienteId)
                .Select(c => new ClienteDto
                {
                    ClienteID = c.ClienteID,
                    Nombres = c.Nombres,
                    Apellidos = c.Apellidos,
                    NombreCompleto = c.NombreCompleto,
                    FechaNacimiento = c.FechaNacimiento,
                    Edad = c.Edad,
                    Genero = c.Genero,
                    Telefono = c.Telefono,
                    Email = c.Email,
                    Direccion = c.Direccion,
                    DocumentoIdentidad = c.DocumentoIdentidad,
                    TipoDocumento = c.TipoDocumento,
                    FechaRegistro = c.FechaRegistro,
                    Activo = c.Activo
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (cliente == null)
            {
                throw new NotFoundException(nameof(Cliente), request.ClienteId);
            }

            return cliente;
        }
    }
}