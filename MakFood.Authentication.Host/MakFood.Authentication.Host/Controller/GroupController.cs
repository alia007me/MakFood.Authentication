using MakFood.Authentication.Application.Command.CommandHandler.DeclaringGroup;
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

        [HttpPost("AddGroup")]
        public async Task<IActionResult> AddGroup([FromBody] DeclaringGroupCommand command , CancellationToken ct)
        {
            command.Validate();

            var result = await _mediator.Send(command);
            if(result.Success) 
                return Ok("Group Added Successfully");
            return BadRequest("Group Didn't Add !");
            
        }
    }
}
