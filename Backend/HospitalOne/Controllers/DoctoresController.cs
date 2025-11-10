using HospitalOne.Application.Features.Doctores.Commands.CreateDoctor;
using HospitalOne.Application.Features.Doctores.Common;
using HospitalOne.Application.Features.Doctores.Queries.GetDoctores;
using HospitalOne.Application.Features.Doctores.Queries.GetDoctoresDisponiblesPorEspecialidad;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HospitalOne.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctoresController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DoctoresController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Obtiene todos los doctores activos
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(List<DoctorDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<DoctorDto>>> GetAll()
        {
            var result = await _mediator.Send(new GetDoctoresQuery());
            return Ok(result);
        }

        /// <summary>
        /// Obtiene doctores disponibles por especialidad
        /// </summary>
        [HttpGet("disponibles/especialidad/{especialidadId}")]
        [ProducesResponseType(typeof(List<DoctorDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<DoctorDto>>> GetDisponiblesPorEspecialidad(int especialidadId)
        {
            var result = await _mediator.Send(new GetDoctoresDisponiblesPorEspecialidadQuery(especialidadId));
            return Ok(result);
        }

        /// <summary>
        /// Crea un nuevo doctor
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<int>> Create([FromBody] CreateDoctorCommand command)
        {
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetAll), new { id }, id);
        }
    }
}