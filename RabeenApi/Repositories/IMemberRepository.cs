using DataAccess.Models;

namespace RabeenApi.Repositories;

public interface IMemberRepository : IGenericRepository<Member>
{
    public Task<Member> ChangeMemberPrimaryAsync(int id, bool isPrimary);
}