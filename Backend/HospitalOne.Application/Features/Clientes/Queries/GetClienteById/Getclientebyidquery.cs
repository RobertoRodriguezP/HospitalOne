using HospitalOne.Application.Features.Clientes.Common;
using MediatR;

namespace HospitalOne.Application.Features.Clientes.Queries.GetClienteById
{
    public record GetClienteByIdQuery(int ClienteId) : IRequest<ClienteDto>;
}