using MakFood.Authentication.Application.Command.CommandHandler.AssignPermissionToGroup;
using MakFood.Authentication.Application.Command.CommandHandler.DeclaringGroup;
using MakFood.Authentication.Host.Activator.Constants;
using MakFood.FBI.ActionFilter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MakFood.Authentication.Host.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class GroupController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GroupController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HasPermission(Permissions.AddGroup)]
        [HttpPost()]
        public async Task<IActionResult> AddGroup([FromBody] DeclaringGroupCommand command , CancellationToken ct)
        {
            command.Validate();

            var result = await _mediator.Send(command);
            if (result.Success)
                return Ok("Group Added Successfully");
            return BadRequest("Group Didn't Add !");

        }
        [HasPermission(Permissions.AssignPermissionToGroup)]
        [HttpPost("{GroupId}/Permissions")]
        public async Task<IActionResult> AddPermissionToGroup([FromBody] AssignPermissionToGroupCommand command, [FromRoute]uint GroupId, CancellationToken ct)
        {
            command.groupId = GroupId;
            command.Validate();
            var result = await _mediator.Send(command);
            if(result.Success)
                return Ok("Permission Successfully Added To Group !");
            return BadRequest("Permissions Didn't Add !");


        }
    }
}
