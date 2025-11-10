using HospitalOne.Application.Features.Clientes.Commands.CreateCliente;
using HospitalOne.Application.Features.Clientes.Common;
using HospitalOne.Application.Features.Clientes.Queries.GetClienteById;
using HospitalOne.Application.Features.Clientes.Queries.GetClientes;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HospitalOne.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ClientesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Obtiene todos los clientes activos
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(List<ClienteDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<ClienteDto>>> GetAll()
        {
            var result = await _mediator.Send(new GetClientesQuery());
            return Ok(result);
        }

        /// <summary>
        /// Obtiene un cliente por su ID
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ClienteDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ClienteDto>> GetById(int id)
        {
            var result = await _mediator.Send(new GetClienteByIdQuery(id));
            return Ok(result);
        }

        /// <summary>
        /// Crea un nuevo cliente
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<int>> Create([FromBody] CreateClienteCommand command)
        {
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, id);
        }
    }
}