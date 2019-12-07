using RTL.TVMaze.Generic.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RTL.TVMaze.BLL.Repositories
{
    public interface ICastCreditRepository
    {
        Task<bool> AddCastCredit(CastCredit castCredit);
        Task<bool> AddCastCredits(IEnumerable<CastCredit> castCredits);
    }
}
