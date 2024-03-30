using System.Net.Mime;
using Daily_Planner.Domain.Dto.Role;
using Daily_Planner.Domain.Entity;
using Daily_Planner.Domain.Interfaces.Services;
using Daily_Planner.Domain.Result;
using Microsoft.AspNetCore.Mvc;

namespace Daily_Planner.Api.Properties.Controllers;
[ApiController]
[Route("api/[controller]")]
[Consumes(MediaTypeNames.Application.Json)]
public class RoleController : Controller
{
    private readonly IRoleService _roleService;

    public RoleController(IRoleService roleService)
    {
        _roleService = roleService;
    }

    /// <summary>
    /// Create user role method
    /// </summary>
    /// <param name="id"></param>
    /// <remarks>
    /// Request for getting user role
    ///
    ///     POST
    ///     {
    ///         "name" : "Admin",
    ///     }
    ///
    /// </remarks>
    /// <response code="200">Success response</response>
    /// <response code="400">Bad request</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<Role>>> Create([FromBody] CreateRoleDto dto)
    {
        var response = await _roleService.CreateRoleAsync(dto);
        if (response.IsSuccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }
    
    /// <summary>
    /// Delete user role method
    /// </summary>
    /// <param name="id"></param>
    /// <remarks>
    /// Request for deleting user role
    ///
    ///     DELETE
    ///     {
    ///         "userRoleId" : 1,
    ///     }
    ///
    /// </remarks>
    /// <response code="200">Success response</response>
    /// <response code="400">Bad request</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<Role>>> Delete(long id)
    {
        var response = await _roleService.DeleteRoleAsync(id);
        if (response.IsSuccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }
    
    /// <summary>
    /// Update user role method
    /// </summary>
    /// <param name="dto"></param>
    /// <remarks>
    /// Request for updating user role
    ///
    ///     PUT
    ///     {
    ///         "userRoleId" : 1,
    ///         "name" : "Admin"
    ///     }
    /// 
    /// </remarks>
    /// <response code="200">Success response</response>
    /// <response code="400">Bad request</response>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<Role>>> Update([FromBody] RoleDto dto)
    {
        var response = await _roleService.UpdateRoleAsync(dto);
        if (response.IsSuccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }
    
    /// <summary>
    /// Addr role for user method
    /// </summary>
    /// <param name="dto"></param>
    /// <remarks>
    /// Request for adding role to user
    ///
    ///     POST
    ///     {
    ///         "login": "User"
    ///         "role name" : "Admin",
    ///     }
    ///
    /// </remarks>
    /// <response code="200">Success response</response>
    /// <response code="400">Bad request</response>
    [HttpPost("addRole")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<Role>>> AddRoleForUser([FromBody] UserRoleDto dto)
    {
        var response = await _roleService.AddRoleForUserAsync(dto);
        if (response.IsSuccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }
}