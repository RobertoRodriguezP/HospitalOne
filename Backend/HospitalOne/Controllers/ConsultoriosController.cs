using HospitalOne.Application.Features.Consultorios.Commands.AsignarDoctorConsultorio;
using HospitalOne.Application.Features.Consultorios.Commands.CreateConsultorio;
using HospitalOne.Application.Features.Consultorios.Common;
using HospitalOne.Application.Features.Consultorios.Queries.GetConsultorios;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HospitalOne.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConsultoriosController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ConsultoriosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Obtiene todos los consultorios activos
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(List<ConsultorioDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<ConsultorioDto>>> GetAll()
        {
            var result = await _mediator.Send(new GetConsultoriosQuery());
            return Ok(result);
        }

        /// <summary>
        /// Crea un nuevo consultorio
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<int>> Create([FromBody] CreateConsultorioCommand command)
        {
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetAll), new { id }, id);
        }

        /// <summary>
        /// Asigna un doctor a un consultorio
        /// </summary>
        [HttpPut("{consultorioId}/asignar-doctor")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> AsignarDoctor(int consultorioId, [FromBody] AsignarDoctorRequest request)
        {
            var command = new AsignarDoctorConsultorioCommand
            {
                ConsultorioID = consultorioId,
                DoctorID = request.DoctorID
            };

            await _mediator.Send(command);
            return NoContent();
        }
    }

    public record AsignarDoctorRequest(int DoctorID);
}