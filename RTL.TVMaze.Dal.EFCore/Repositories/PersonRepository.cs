using Microsoft.EntityFrameworkCore;
using RTL.TVMaze.BLL.Repositories;
using RTL.TVMaze.Generic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RTL.TVMaze.Dal.EFCore.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly ApplicationDbContext DbContext;

        public PersonRepository(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public async Task<bool> AddPerson(Person person)
        {
            try {
                await DbContext.People.AddAsync(person);
            }
            catch (Exception ex) {
                throw ex;
            }
            return true;
        }

        public async Task<bool> AddPeople(IEnumerable<Person> people)
        {
            await DbContext.People.AddRangeAsync(people);
            return true;
        }

        public async Task<bool> PersonExists(int id)
        {
            var existsInDb = await DbContext.People.AnyAsync(p => p.Id == id);
            var existsInLocal = DbContext.ChangeTracker.Entries<Person>().Any(p => p.Entity.Id == id);
            if (id == 890)
            {
                var b = 11;
            }
            return existsInDb || existsInLocal;
        }
    }
}
