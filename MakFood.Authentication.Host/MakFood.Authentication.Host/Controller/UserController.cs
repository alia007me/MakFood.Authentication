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

        [HttpPost("{userId}/groups/{groupId}/assign")]
        public async Task<IActionResult> AddGroupToUser([FromBody] AssignGroupToUserCommand command, [FromRoute] Guid userId , [FromRoute] uint groupId, CancellationToken ct)
        {
            command.UserId = userId;
            command.groupId = groupId;
            command.Validate();

            var result = await _mediator.Send(command);
            if (result.Success)
                return Ok("Group Added To This User Successfully !");
            return BadRequest("Group Didn't Add !");
        }
    }
}
