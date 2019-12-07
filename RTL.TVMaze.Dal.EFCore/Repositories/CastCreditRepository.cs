using RTL.TVMaze.BLL.Repositories;
using RTL.TVMaze.Generic.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RTL.TVMaze.Dal.EFCore.Repositories
{
    public class CastCreditRepository : ICastCreditRepository
    {
        private readonly ApplicationDbContext DbContext;

        public CastCreditRepository(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public async Task<bool> AddCastCredit(CastCredit castCredit)
        {
            await DbContext.CastCredits.AddAsync(castCredit);
            return true;
        }

        public async Task<bool> AddCastCredits(IEnumerable<CastCredit> castCredits)
        {
            await DbContext.CastCredits.AddRangeAsync(castCredits);
            return true;
        }
    }
}
