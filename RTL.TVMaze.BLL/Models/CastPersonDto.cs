using RTL.TVMaze.BLL.Helpers;
using RTL.TVMaze.Generic.Entities;
using System;

namespace RTL.TVMaze.BLL.Models
{
    public class CastPersonDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? Birthday { get; set; }

        public CastPersonDto(Person person) {
            PropertyMapper.Map(person, this);
        }

        public CastPersonDto(){}
    }
}
