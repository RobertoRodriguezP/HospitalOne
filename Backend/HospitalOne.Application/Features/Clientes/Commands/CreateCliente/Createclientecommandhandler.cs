using HospitalOne.Application.Common.Interfaces;
using HospitalOne.Domain.Models;
using MediatR;

namespace HospitalOne.Application.Features.Clientes.Commands.CreateCliente
{
    public class CreateClienteCommandHandler : IRequestHandler<CreateClienteCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateClienteCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateClienteCommand request, CancellationToken cancellationToken)
        {
            var cliente = new Cliente
            {
                Nombres = request.Nombres,
                Apellidos = request.Apellidos,
                FechaNacimiento = request.FechaNacimiento,
                Genero = request.Genero,
                Telefono = request.Telefono,
                Email = request.Email,
                Direccion = request.Direccion,
                DocumentoIdentidad = request.DocumentoIdentidad,
                TipoDocumento = request.TipoDocumento,
                FechaRegistro = DateTime.Now,
                Activo = true
            };

            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync(cancellationToken);

            return cliente.ClienteID;
        }
    }
}