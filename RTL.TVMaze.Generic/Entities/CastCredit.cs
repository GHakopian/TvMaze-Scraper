using System;
using System.Collections.Generic;
using System.Text;

namespace RTL.TVMaze.Generic.Entities
{
    public class CastCredit
    {
        public int CastCreditId { get; set; }

        public int ShowId { get; set; }
        public int PersonId { get; set; }
        public Person Person { get; set; }
    
    }
}
