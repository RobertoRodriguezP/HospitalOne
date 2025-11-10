using HospitalOne.Application.Features.Especialidades.Common;
using MediatR;

namespace HospitalOne.Application.Features.Especialidades.Queries.GetEspecialidades
{
    public record GetEspecialidadesQuery : IRequest<List<EspecialidadDto>>;
}