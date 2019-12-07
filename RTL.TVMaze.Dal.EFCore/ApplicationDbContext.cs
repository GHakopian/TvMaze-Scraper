using Microsoft.EntityFrameworkCore;
using RTL.TVMaze.Generic.Entities;
using System;

namespace RTL.TVMaze.Dal.EFCore
{
    public class ApplicationDbContext : DbContext
    {
        public virtual DbSet<Show> Shows { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<CastCredit> CastCredits { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
    }
}
