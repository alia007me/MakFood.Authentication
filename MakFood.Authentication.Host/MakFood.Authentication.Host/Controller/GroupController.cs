﻿using MakFood.Authentication.Application.Command.CommandHandler.AssignPermissionToGroup;
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

        [HttpPost("SuperAdmin/Group/AddGroup")]
        public async Task<IActionResult> AddGroup([FromBody] DeclaringGroupCommand command, CancellationToken ct)
        {
            command.Validate();

            var result = await _mediator.Send(command);
            if (result.Success)
                return Ok("Group Added Successfully");
            return BadRequest("Group Didn't Add !");

        }
        [HttpPost("SuperAdmin/Group/{GroupName}/Permission")]
        public async Task<IActionResult> AddPermissionToGroup([FromBody] AssignPermissionToGroupCommand command, CancellationToken ct)
        {
            command.Validate();

            var result = await _mediator.Send(command);
            if(result.Success)
                return Ok("Permission Successfully Added To Group !");
            return BadRequest("Permissions Didn't Add !");


        }
    }
}
