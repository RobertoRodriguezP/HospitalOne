using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
//using HospitalOne.Application.

namespace HospitalOne.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DoctoresController : Controller
    {
        private IMediator _mediator;
        public DoctoresController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(Name = "GetDoctores")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> GetDoctores([FromBody] GetDoctoresCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}
