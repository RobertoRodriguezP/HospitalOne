using MediatR;

namespace HospitalOne.Application.Features.Consultorios.Commands.AsignarDoctorConsultorio
{
    public record AsignarDoctorConsultorioCommand : IRequest<Unit>
    {
        public int ConsultorioID { get; init; }
        public int DoctorID { get; init; }
    }
}