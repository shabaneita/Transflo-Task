using Transflo_Task.Models;

namespace Transflo_Task.Repository.IRepostiory
{
    public interface IDriverRepository : IRepository<Driver>
    {

        Task<Driver> UpdateAsync(Driver entity);

    }
}
