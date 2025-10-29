using MakFood.Authentication.Application.Command.CommandHandler.AssignGroupPermissonToUser;
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

        [HttpPost("SuperAdmin/{userId}/AddGroup")]
        public async Task<IActionResult> AddGroupToUser([FromBody] AssignGroupToUserCommand command, CancellationToken ct)
        {
            command.Validate();

            var result = await _mediator.Send(command);
            if (result.Success)
                return Ok("Group Added To This User Successfully !");
            return BadRequest("Group Didn't Add !");
        }
    }
}
