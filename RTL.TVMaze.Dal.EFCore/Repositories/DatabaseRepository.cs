using RTL.TVMaze.BLL.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RTL.TVMaze.Dal.EFCore.Repositories
{
    public class DatabaseRepository : IDatabaseRepository
    {
        private readonly ApplicationDbContext dbContext;

        public DatabaseRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<int> SaveChanges()
        {
            var tracking = dbContext.ChangeTracker;
            return await dbContext.SaveChangesAsync();
        }
    }
}
