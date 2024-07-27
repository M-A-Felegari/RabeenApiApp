using DataAccess.Models;

namespace RabeenApi.Repositories;

public interface IMemberRepository : IGenericRepository<Member>
{
    public Task<List<Member>> GetAllMainMembersAsync();

}