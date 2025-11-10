using HospitalOne.Application.Features.Citas.Common;
using MediatR;

namespace HospitalOne.Application.Features.Citas.Queries.GetCitasByCliente
{
    public record GetCitasByClienteQuery(int ClienteId) : IRequest<List<CitaDto>>;
}