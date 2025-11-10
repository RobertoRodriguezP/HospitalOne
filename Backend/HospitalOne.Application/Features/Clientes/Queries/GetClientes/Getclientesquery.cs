using HospitalOne.Application.Features.Clientes.Common;
using MediatR;

namespace HospitalOne.Application.Features.Clientes.Queries.GetClientes
{
    public record GetClientesQuery : IRequest<List<ClienteDto>>;
}