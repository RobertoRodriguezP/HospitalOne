using HospitalOne.Application.Features.Doctores.Common;
using MediatR;

namespace HospitalOne.Application.Features.Doctores.Queries.GetDoctores
{
    public record GetDoctoresQuery : IRequest<List<DoctorDto>>;
}