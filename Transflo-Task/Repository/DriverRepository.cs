using Transflo_Task.Data;
using Transflo_Task.Models;
using Transflo_Task.Repository.IRepostiory;

namespace Transflo_Task.Repository
{
    public class DriverRepository : Repository<Driver>, IDriverRepository
    {
        private readonly ApplicationDbContext _db;
        public DriverRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public async Task<Driver> UpdateAsync(Driver entity)
        {
            _db.Drivers.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
