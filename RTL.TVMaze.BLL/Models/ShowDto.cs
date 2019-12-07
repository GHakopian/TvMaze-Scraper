using RTL.TVMaze.BLL.Helpers;
using RTL.TVMaze.Generic.Entities;
using System.Collections.Generic;
using System.Linq;

namespace RTL.TVMaze.BLL.Models
{
    public class ShowDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<CastPersonDto> Cast { get; set; }

        public ShowDto(Show show) {
            PropertyMapper.Map(show, this);
            if (show.Cast != null) {
                Cast = new List<CastPersonDto>();
                foreach (var castCredit in show.Cast) {
                    Cast.Add(new CastPersonDto(castCredit.Person));
                }
            }
            Cast = Cast.OrderByDescending(c => c.Birthday).ToList();
        }

        public ShowDto(){}
    }
}
