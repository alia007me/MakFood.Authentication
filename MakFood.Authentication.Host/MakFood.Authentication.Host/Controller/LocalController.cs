using MakFood.Authentication.Application.Command.Command.Handler.DeclaringPermission;
using MakFood.Authentication.Host.Filters;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MakFood.Authentication.Host.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocalController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LocalController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [RequireLocal]
        [HttpPost("{service}/AddPermission")]
        public async Task<IActionResult> AddPermission([FromBody]DeclaringPermissionCommand command, [FromRoute] string service , CancellationToken ct)
        {
            command.Service = service;
            command.Validate();

            var result = await _mediator.Send(command, ct);

            if (result.Success)
                return Ok("Your Permission Added Successfully");
            else
                return BadRequest("Nothing Added !");

        }
    }
}
