using MakFood.Authentication.Application.Command.Command.Handler.DeclaringPermission;
using MakFood.Authentication.Host.Filters;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MakFood.Authentication.Host.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [RequireLocal]
        [HttpPost]
        public async Task<IActionResult> AddPermission([FromBody]DeclaringPermissionCommand command , CancellationToken ct)
        {
            command.Validate();

            var result = await _mediator.Send(command, ct);

            if (result.Success)
                return Ok("Your Permission Added Successfully");
            else
                return BadRequest("Nothing Added !");

        }
    }
}
