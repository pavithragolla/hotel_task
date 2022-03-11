using Dapper;
using task.DTOs;
using task.Models;

namespace task.Repositories;

public interface IStaffRepository
{
    Task<Staff> Create(Staff Item);
    Task<bool> Update(Staff Item);
    Task<bool> Delete(int StaffId);
    Task<Staff> GetById(int StaffId);
    Task<List<Staff>> GetList();
    Task<List<Staff>> GetAllForRooms(int RoomId);



}
public class StaffRepository : BaseRepository, IStaffRepository

{
    public StaffRepository(IConfiguration configuration) : base(configuration)
    {
    }

    public async Task<Staff> Create(Staff Item)
    {
        var query = $@"Insert Into room_service_staff(staff_id,name,mobile,address,gender)VALUES(@StaffId,@Name,@Mobile,@Address,@Gender) RETURNING *";
        using (var con = NewConnection)
        {
            var res = await con.QuerySingleOrDefaultAsync<Staff>(query, Item);
            return res;

        }
    }


    public async Task<bool> Delete(int StaffId)
    {
        var query = $@"DELETE FROM room_service_staff WHERE staff_id=@StaffId";

        using (var con = NewConnection)
        {
            var result = await con.ExecuteAsync(query, new { StaffId });
            return result > 0;

        }
    }

    public async Task<List<Staff>> GetAllForRooms(int RoomId)
    {
        var query = $@"SELECT * FROM room_service_staff WHERE staff_id = StaffId";
        using (var con = NewConnection)
        return (await con.QueryAsync<Staff>(query,new{RoomId})).AsList();
    }

    public async Task<Staff> GetById(int StaffId)

    {
        var query = $@"SELECT * FROM room_service_staff WHERE staff_id = @StaffId";
        using (var con = NewConnection)
            return await con.QuerySingleOrDefaultAsync<Staff>(query, new { StaffId });
    }

    public async Task<List<Staff>> GetList()
    {
        var query = $@"SELECT * FROM room_service_staff";
        List<Staff> result;
        using (var con = NewConnection)
            result = (await con.QueryAsync<Staff>(query)).AsList();
        return result;
    }

    public async Task<bool> Update(Staff Item)
    {
        var query = $@"UPDATE staff SET name=@Name,staff_id=@StaffId,address=@Address, WHERE staff_id=@StaffId";
        using (var con = NewConnection)
        {
            var rowCount = await con.ExecuteAsync(query, Item);
            return rowCount == 1;
        }
    }

    // Task<Staff> IStaffRepository.GetById(int StaffId)
    // {
    //     throw new NotImplementedException();
    // }



    //     Task<Staff> IStaffRepository.GetById(int StaffId)
    // {
    //         throw new NotImplementedException();
    // }
}