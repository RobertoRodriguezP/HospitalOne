using HospitalOne.Application.Common.Interfaces;
using HospitalOne.Application.Features.Clientes.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HospitalOne.Application.Features.Clientes.Queries.GetClientes
{
    public class GetClientesQueryHandler : IRequestHandler<GetClientesQuery, List<ClienteDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetClientesQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ClienteDto>> Handle(GetClientesQuery request, CancellationToken cancellationToken)
        {
            return await _context.Clientes
                .AsNoTracking() // No tracking porque solo leemos
                .Where(c => c.Activo)
                .OrderBy(c => c.Apellidos)
                .ThenBy(c => c.Nombres)
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
                .ToListAsync(cancellationToken);
        }
    }
}