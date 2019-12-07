using RTL.TVMaze.Generic.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RTL.TVMaze.BLL.Repositories
{
    public interface IShowRepository
    {
        Task<bool> AddShow(Show show);
        Task<bool> AddShows(IEnumerable<Show> shows);
        Task<int> GetHighestShowId();
        IEnumerable<Show> GetShows(int skip, int top);
    }
}
