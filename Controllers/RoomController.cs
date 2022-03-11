using task.DTOs;
using task.Models;
using task.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace task.Controllers;


[ApiController]
[Route("Controller")]

public class RoomController : ControllerBase
{
    private readonly ILogger<RoomController> _logger;
    private readonly IRoomRepository _room;
     private readonly IStaffRepository _Staff;


    public RoomController(ILogger<RoomController> logger, IRoomRepository room, IStaffRepository Staff)
    {
        _logger = logger;
        _room = room;
        _Staff = Staff;
    }
    [HttpGet]
    public async Task<ActionResult<List<RoomDTO>>> GetAllUsers()
    {
        var usersList = await _room.GetList();

        // User -> UserDTO
        var dtoList = usersList.Select(x => x.asDto);

        return Ok(dtoList);
    }
    [HttpPost]
    public async Task<ActionResult<RoomDTO>> CreateRoom([FromBody] RoomCreateDTO Data)
    {
        var Staff = await _Staff.GetById(Data.StaffId);
        if(Staff is null)
        return NotFound("No user found with given staff id");
        var toCreateRoom = new Room
        {
            RoomId = Data.RoomId,
            StaffId = Data.StaffId
        };
        var createdRoom = await _room.Create(toCreateRoom);
        return StatusCode(StatusCodes.Status201Created);
    }
    [HttpGet("{room_id}")]
    public async Task<ActionResult<RoomDTO>> GetRoomById([FromRoute] int room_id)
    {
        var room  = await _room.GetById(room_id);

        if (room is null)
            return NotFound("No user found with given room id");
            var dto = room.asDto;
            dto.Staff = await _room.GetAllForRoom(room.RoomId);
            // dto.Staff = await _Staff.GetAllForRooms(room_id);


        return Ok(dto);
    }


    [HttpDelete("{room_id}")]
    public async Task<ActionResult> DeleteUser([FromRoute] int room_id)
    {
        var existing = await _room.GetById(room_id);
        if (existing is null)
            return NotFound("No user found with given room id");

        var didDelete = await _room.Delete(room_id);

        return NoContent();
    }
}