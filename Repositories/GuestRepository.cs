using task.Models;
using Dapper;

namespace task.Repositories;

public interface IGuestRepository
{
    Task<Guest> Create(Guest Item);
    Task<bool> Update(Guest Item);
    Task<bool> Delete(int GuestId);
    Task<Guest> GetById(int GuestId);
    Task<List<Guest>> GetList();


}
public class GuestRepository : BaseRepository, IGuestRepository
{
    public GuestRepository(IConfiguration configuration) : base(configuration)
    {
    }

    public async Task<Guest> Create(Guest Item)
    {
        var query = $@"INSERT INTO guest (guest_id,name,details,date) VALUES (@GuestId, @Name, @Details,@Date) RETURNING * ";
        using (var connection = NewConnection)
        {
            var res = await connection.QuerySingleOrDefaultAsync<Guest>(query, Item);
            return res;
        }
    }

    public async Task<bool> Delete(int GuestId)
    {
        var query = $@"DELETE FROM guest WHERE guest_id=@GuestId";

        using (var connection = NewConnection)
        {
            var res = await connection.ExecuteAsync(query, new { GuestId });
            return res > 0;
        }
    }



    public async Task<Guest> GetById(int GuestId)
    {
        var query = $@"SELECT * FROM guest
        WHERE guest_id = @GuestId";
        using (var con = NewConnection)
            return await con.QuerySingleOrDefaultAsync<Guest>(query, new { GuestId });
    }



    public async Task<List<Guest>> GetList()
    {
        var query = $@"SELECT * FROM guest";

        List<Guest> res;
        using (var con = NewConnection)
            res = (await con.QueryAsync<Guest>(query)).AsList();
        return res;

    }

    public async Task<bool> Update(Guest Item)
    {
        var query = $@"UPDATE guest SET  name=@Name,details=@Details WHERE guest_id=@GuestId ";
        using (var connection = NewConnection)
        {
            var Count = await connection.ExecuteAsync(query, Item);
            return Count == 1;
        }
    }



}