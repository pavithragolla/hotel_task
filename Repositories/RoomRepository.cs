using Dapper;
using task.DTOs;
using task.Models;

namespace task.Repositories;

public interface IRoomRepository
{
    Task<Room> Create(Room Item);
    // Task<bool> Update(Room Item);
    Task<bool> Delete(int RoomId);
    Task<Room> GetById(int RoomId);
    Task<List<Room>> GetList();
    Task<List<StaffDTO>> GetAllForRoom(int RoomId);

}
public class RoomRepository : BaseRepository, IRoomRepository
{
    public RoomRepository(IConfiguration configuration) : base(configuration)
    {
    }

    public async Task<Room> Create(Room Item)
    {
        var query = $@"Insert Into rooms(room_id,staff_id)VALUES(@RoomId,@StaffId) RETURNING *";
        using (var con = NewConnection)
        {
            var res = await con.QuerySingleOrDefaultAsync<Room>(query, Item);
            return res;

        }
    }

    public async Task<bool> Delete(int RoomId)
    {
        var query = $@"DELETE FROM rooms WHERE room_id=@RoomId";

        using (var con = NewConnection)
        {
            var result = await con.ExecuteAsync(query, new { RoomId });
            return result > 0;

        }
    }

    public async Task<List<StaffDTO>> GetAllForRoom(int RoomId)
    {
        var query =$@"SELECT * FROM room_service_staff WHERE staff_id = @RoomId";
        using (var con = NewConnection)
        return (await con.QueryAsync<StaffDTO>(query, new {RoomId})).AsList();
       
    }

    
    public async Task<Room> GetById(int RoomId)

    {
        var query = $@"SELECT * FROM rooms WHERE room_id = @RoomId";
        using (var con = NewConnection)
            return await con.QuerySingleOrDefaultAsync<Room>(query, new { RoomId });
    }

    public async Task<List<Room>> GetList()
    {
        var query = $@"SELECT * FROM rooms ";
        List<Room> result;
        using (var con = NewConnection)
            result = (await con.QueryAsync<Room>(query)).AsList();
        return result;
    }

    // public async Task<bool> Update(Room Item)
    // {
    //     var query = $@"UPDATE rooms SET room_type = @RoomType, room_no = @RoomNo, staff_id = @StaffId WHERE room_id = @RoomId";
    //     using (var con = NewConnection)
    //     {
    //         var rowCount = await con.ExecuteAsync(query, Item);
    //         return rowCount == 1;
    //     }
    // }
}