using HospitalOne.Application.Features.Doctores.Common;
using MediatR;

namespace HospitalOne.Application.Features.Doctores.Queries.GetDoctoresDisponiblesPorEspecialidad
{
    public record GetDoctoresDisponiblesPorEspecialidadQuery(int EspecialidadId) : IRequest<List<DoctorDto>>;
}