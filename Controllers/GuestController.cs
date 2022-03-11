using Microsoft.AspNetCore.Mvc;
using task.DTOs;
using task.Models;
using task.Repositories;


namespace task.Controllers;
[ApiController]
[Route("Api/guest")]

public class GuestController : ControllerBase
{
    private readonly ILogger<GuestController> _logger;
    private readonly IGuestRepository _guest;
    private readonly IScheduleRepository _schedule;

    public GuestController(ILogger<GuestController> logger, IGuestRepository guest, IScheduleRepository schedule)
    {
        _logger = logger;
        _guest = guest;
        _schedule = schedule;
    }
    [HttpGet]
    public async Task<ActionResult<List<GuestDTO>>> GetAllUser()
    {
        var usersList = await _guest.GetList();


        var dtoList = usersList.Select(x => x.asDto);

        return Ok(dtoList);
    }
    [HttpGet("{guest_id}")]

    public async Task<ActionResult<GuestDTO>> GetUserById([FromRoute] int guest_id)
    {

        var guest = await _guest.GetById(guest_id);

        if (guest is null)
            return NotFound("No Guest found with given guest_id");


        var dto = guest.asDto;
        dto.Schedule = await _schedule.GetAllForGuest(guest.GuestId);
        return Ok(dto);
    }

    [HttpPost]
    public async Task<ActionResult<GuestDTO>> CreateGuest([FromBody] GuestCreateDTO Data)
    {
        var ToCreateGuest = new Guest
        {
            GuestId = Data.GuestId,
            Name = Data.Name,
            Details = Data.Details,
            Date = Data.Date
        };
        var CreatedGuest = await _guest.Create(ToCreateGuest);

        return StatusCode(StatusCodes.Status201Created, CreatedGuest.asDto);
    }
    [HttpPut("{guest_id}")]

    public async Task<ActionResult> UpdateGuest([FromRoute] int guest_id,
    [FromBody] GuestUpdateDTO Data)
    {
        var existing = await _guest.GetById(guest_id);
        if (existing is null)
            return NotFound("No Guest found with given Guest Id");

        var toUpdateGuest = existing with
        {
            Name = Data.Name,
            Details = Data.Details,
            Date = Data.Date

        };

        var didUpdate = await _guest.Update(toUpdateGuest);

        if (!didUpdate)
            return StatusCode(StatusCodes.Status500InternalServerError, "Could not update guest");

        return NoContent();
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteGuest([FromRoute] int guest_id)
    {
        var existing = await _guest.GetById(guest_id);
        if (existing is null)
            return NotFound("No user found with given guest id");

        var didDelete = await _guest.Delete(guest_id);

        return NoContent();
    }
}