using HospitalOne.Application.Features.Especialidades.Common;
using HospitalOne.Application.Features.Especialidades.Queries.GetEspecialidades;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HospitalOne.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EspecialidadesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EspecialidadesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Obtiene todas las especialidades activas con conteo de doctores
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(List<EspecialidadDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<EspecialidadDto>>> GetAll()
        {
            var result = await _mediator.Send(new GetEspecialidadesQuery());
            return Ok(result);
        }
    }
}