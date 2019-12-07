using RTL.TVMaze.Generic.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RTL.TVMaze.BLL.Repositories
{
    public interface IPersonRepository
    {
        Task<bool> AddPerson(Person person);
        Task<bool> AddPeople(IEnumerable<Person> people);
        Task<bool> PersonExists(int id);
    }
}
