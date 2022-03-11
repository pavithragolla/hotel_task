using Dapper;
using task.DTOs;
using task.Models;

namespace task.Repositories;

public interface IScheduleRepository
{
    Task<Schedule> Create(Schedule Item);

    Task<bool> Delete(long ScheduleId);
    Task<Schedule> GetById(long ScheduleId);
    Task<List<Schedule>> GetList();
    Task<List<ScheduleDTO>> GetAllForGuest(long GuestId);


}
public class ScheduleRepository : BaseRepository, IScheduleRepository
{
    public ScheduleRepository(IConfiguration configuration) : base(configuration)
    {
    }
    public async Task<Schedule> Create(Schedule Item)
    {
        var query = $@"Insert Into Schedule(schedule_id,room_id,guest_id,login,logout,date)VALUES(@ScheduleId,@RoomId,@GuestId,@Login,@Logout,@Date) RETURNING *";
        using (var con = NewConnection)
        {
            var res = await con.QuerySingleOrDefaultAsync<Schedule>(query, Item);
            return res;

        }
    }

    public async Task<bool> Delete(long ScheduleId)
    {
        var query = $@"DELETE FROM Schedule WHERE schedule_id=@ScheduleId";

        using (var con = NewConnection)
        {
            var result = await con.ExecuteAsync(query, new { ScheduleId });
            return result > 0;

        }
    }






    // public Task<bool> Delete(long ScheduleId)
    // {
    //     throw new NotImplementedException();
    // }

    public async Task<Schedule> GetById(long ScheduleId)


    {
        var query = $@"SELECT * FROM Schedule WHERE schedule_id = @ScheduleId";
        using (var con = NewConnection)
            return await con.QuerySingleOrDefaultAsync<Schedule>(query, new { ScheduleId });
    }

    public async Task<List<Schedule>> GetList()
    {
        var query = $@"SELECT * FROM Schedule";
        List<Schedule> result;
        using (var con = NewConnection)
            result = (await con.QueryAsync<Schedule>(query)).AsList();
        return result;
    }

    public async Task<List<ScheduleDTO>> GetAllForGuest(long GuestId)
    {
        var query = $@"SELECT * FROM Schedule WHERE guest_id = @GuestId";
        using (var con = NewConnection)

          return (await con.QueryAsync<ScheduleDTO>(query, new {GuestId})).AsList();
    }


}