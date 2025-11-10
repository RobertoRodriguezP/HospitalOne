using HospitalOne.Application.Features.Consultorios.Common;
using MediatR;

namespace HospitalOne.Application.Features.Consultorios.Queries.GetConsultorios
{
    public record GetConsultoriosQuery : IRequest<List<ConsultorioDto>>;
}