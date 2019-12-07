using Microsoft.EntityFrameworkCore;
using RTL.TVMaze.BLL.Repositories;
using RTL.TVMaze.Generic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RTL.TVMaze.Dal.EFCore.Repositories
{
    public class ShowRepository : IShowRepository
    {
        private readonly ApplicationDbContext DbContext;

        public ShowRepository(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public async Task<bool> AddShow(Show show)
        {
            await DbContext.Shows.AddAsync(show);
            return true;
        }

        public async Task<bool> AddShows(IEnumerable<Show> shows)
        {
            await DbContext.Shows.AddRangeAsync(shows);
            return true;
        }

        public async Task<int> GetHighestShowId()
        {
            var ShowWithHighestId = await DbContext.Shows
                .OrderByDescending(s => s.Id)
                .FirstOrDefaultAsync();
            if (ShowWithHighestId != null) {
                return ShowWithHighestId.Id;
            }

            return 0;
        }

        public IEnumerable<Show> GetShows(int skip, int top)
        {
            return DbContext.Shows
                .Include(s => s.Cast)
                    .ThenInclude(c => c.Person)
                .Skip(skip)
                .Take(top)
                .AsNoTracking();
        }
    }
}
