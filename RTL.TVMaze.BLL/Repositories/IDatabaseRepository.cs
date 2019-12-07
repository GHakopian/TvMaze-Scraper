using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RTL.TVMaze.BLL.Repositories
{
    public interface IDatabaseRepository
    {
        Task<int> SaveChanges();
    }
}
