using task.DTOs;
using task.Models;
using task.Repositories;
using Microsoft.AspNetCore.Mvc;
using static task.DTOs.StaffDTO;

namespace task.Controllers;


[ApiController]
[Route("Api/Controller")]

public class StaffController : ControllerBase
{
    private readonly ILogger<StaffController> _logger;
    private readonly IStaffRepository _Staff;

    public StaffController(ILogger<StaffController> logger, IStaffRepository Staff)
    {
        _logger = logger;
        _Staff = Staff;
    }
    [HttpGet]
    public async Task<ActionResult<List<StaffDTO>>> GetAllUsers()
    {
        var usersList = await _Staff.GetList();


        var dtoList = usersList.Select(x => x.asDto);

        return Ok(dtoList);
    }
    [HttpPost]
    public async Task<ActionResult<StaffDTO>> CreateStaff([FromBody] StaffCreateDTO Data)
    {
        var toCreateStaff = new Staff
        {
            StaffId = Data.StaffId,
            Name = Data.Name,
            Address = Data.Address,
            Mobile = Data.Mobile,
            Gender = Data.Gender
        };
        var createdStaff = await _Staff.Create(toCreateStaff);
        return StatusCode(StatusCodes.Status201Created);
    }
    [HttpGet("{staff_id}")]
    public async Task<ActionResult<StaffDTO>> GetUserById([FromRoute] int staff_id)
    {
        var user = await _Staff.GetById(staff_id);

        if (user is null)
            return NotFound("No user found with given Staff id");

        return Ok(user.asDto);
    }
    [HttpPut("{staff_id}")]
    public async Task<ActionResult> UpdateStaff([FromRoute] int staff_id,
       [FromBody] StaffUpdateDTO Data)
    {
        var existing = await _Staff.GetById(staff_id);
        if (existing is null)
            return NotFound("No user found with given staff id");

        var toUpdateStaff = existing with
        {
            Name = Data.Name,
            Address = Data.Address,

        };

        var didUpdate = await _Staff.Update(toUpdateStaff);

        if (!didUpdate)
            return StatusCode(StatusCodes.Status500InternalServerError, "Could not update Staff");

        return NoContent();
    }

    [HttpDelete("{staff_id}")]
    public async Task<ActionResult> DeleteUser([FromRoute] int staff_id)
    {
        // var existing = await _Staff.Delete(staff_id);
        // if (existing is null)
        //     return NotFound("No user found with given staff id");

        var didDelete = await _Staff.Delete(staff_id);

        return NoContent();
    }
}