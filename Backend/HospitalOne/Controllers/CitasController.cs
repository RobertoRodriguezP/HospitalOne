using HospitalOne.Application.Features.Citas.Commands.CreateCita;
using HospitalOne.Application.Features.Citas.Commands.UpdateEstadoCita;
using HospitalOne.Application.Features.Citas.Common;
using HospitalOne.Application.Features.Citas.Queries.GetCitasByCliente;
using HospitalOne.Application.Features.Citas.Queries.GetCitasByDoctorYFecha;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HospitalOne.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CitasController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CitasController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Obtiene todas las citas de un cliente
        /// </summary>
        [HttpGet("cliente/{clienteId}")]
        [ProducesResponseType(typeof(List<CitaDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<CitaDto>>> GetByCliente(int clienteId)
        {
            var result = await _mediator.Send(new GetCitasByClienteQuery(clienteId));
            return Ok(result);
        }

        /// <summary>
        /// Obtiene las citas de un doctor en una fecha específica
        /// </summary>
        [HttpGet("doctor/{doctorId}/fecha/{fecha}")]
        [ProducesResponseType(typeof(List<CitaDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<CitaDto>>> GetByDoctorYFecha(int doctorId, DateTime fecha)
        {
            var result = await _mediator.Send(new GetCitasByDoctorYFechaQuery(doctorId, fecha));
            return Ok(result);
        }

        /// <summary>
        /// Crea una nueva cita
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<int>> Create([FromBody] CreateCitaCommand command)
        {
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetByCliente), new { clienteId = command.ClienteID }, id);
        }

        /// <summary>
        /// Actualiza el estado de una cita
        /// </summary>
        [HttpPut("{id}/estado")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateEstado(int id, [FromBody] UpdateEstadoCitaRequest request)
        {
            var command = new UpdateEstadoCitaCommand
            {
                CitaID = id,
                NuevoEstado = request.NuevoEstado,
                FechaInicioReal = request.FechaInicioReal,
                FechaFinReal = request.FechaFinReal,
                Diagnostico = request.Diagnostico,
                Observaciones = request.Observaciones
            };

            await _mediator.Send(command);
            return NoContent();
        }
    }

    public record UpdateEstadoCitaRequest(
        HospitalOne.Domain.Enums.EstadoCita NuevoEstado,
        DateTime? FechaInicioReal,
        DateTime? FechaFinReal,
        string? Diagnostico,
        string? Observaciones
    );
}