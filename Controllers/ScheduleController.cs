using Microsoft.AspNetCore.Mvc;
using task.DTOs;
using task.Models;
using task.Repositories;

namespace task.Controllers;
[ApiController]
[Route("api/schedule")]

public class ScheduleController : ControllerBase
{
    private readonly ILogger<ScheduleController> _logger;
    private readonly IScheduleRepository _schedule;
    private readonly IRoomRepository _room;
    private readonly IGuestRepository _guest;


    public ScheduleController(ILogger<ScheduleController> logger, IScheduleRepository schedule, IRoomRepository room, IGuestRepository guest)
    {
        _logger = logger;
        _schedule = schedule;
        _room = room;
        _guest = guest;
    }



    
    [HttpGet("{schedule_id}")]
    public async Task<ActionResult<ScheduleDTO>> GetByScheduleId([FromRoute] int schedule_id)
    {
        var schedule = await _schedule.GetById(schedule_id);

        if (schedule is null)
            return NotFound("No schedule found with given schedule_id");

        return Ok(schedule.asDto);
    }

    [HttpPost]
    public async Task<ActionResult<ScheduleDTO>> Create([FromBody] ScheduleCreateDTO Data)
    {
        // var user = await _schedule.GetByScheduleId(Data.ScheduleId);
        // if (user is null)
        //     return NotFound("No user found");

        var toCreateSchedule = new Schedule
        {

            ScheduleId = Data.ScheduleId,
            RoomId = Data.RoomId,
            GuestId = Data.GuestId,
            Login = Data.Login,
            Logout = Data.Logout,
            Date = Data.Date
        };
        var createdItem = await _schedule.Create(toCreateSchedule);

        return StatusCode(StatusCodes.Status201Created, createdItem);

    }
    // [HttpPut("{schedule_id}")]

    // public async Task<ActionResult> Update([FromBody] int schedule_id, [FromBody] ScheduleCreateDTO Data)
    // {
    //     var existing = await _schedule.GetById(schedule_id);
    //     if (existing is null)
    //         return NotFound("No Hardware found with given Id");

    //     var toUpdateItem = existing with
    //     {
    //         ScheduleId = Data.ScheduleId,
    //         RoomId = Data.RoomId,
    //         GuestId = Data.GuestId
    //     };
    //     await _schedule.Update(toUpdateItem);


    //     return NoContent();
    // }
    [HttpDelete("{schedule_id}")]
    public async Task<ActionResult> Delete([FromRoute] int schedule_id)
    {
        var existing = await _schedule.GetById(schedule_id);
        if (existing is null)
            return NotFound("No user found with given schedule_id");

        await _schedule.Delete(schedule_id);

        return NoContent();
    }

    [HttpGet]
    public async Task<ActionResult<List<ScheduleDTO>>> GetAllSchedule()
    {

        var ScheduleList = await _schedule.GetList();


        var dtoList = ScheduleList.Select(x => x.asDto);

        return Ok(dtoList);
    }


}