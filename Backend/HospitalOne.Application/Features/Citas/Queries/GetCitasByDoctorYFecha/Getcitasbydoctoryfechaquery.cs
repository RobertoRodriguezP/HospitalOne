using HospitalOne.Application.Features.Citas.Common;
using MediatR;

namespace HospitalOne.Application.Features.Citas.Queries.GetCitasByDoctorYFecha
{
    public record GetCitasByDoctorYFechaQuery(int DoctorId, DateTime Fecha) : IRequest<List<CitaDto>>;
}