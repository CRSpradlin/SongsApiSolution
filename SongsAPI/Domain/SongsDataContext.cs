using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SongsAPI.Domain
{
    public class SongsDataContext : DbContext
    {
        public SongsDataContext(DbContextOptions<SongsDataContext> options): base(options)
        {

        }
        public DbSet<Song> Songs { get; set; }
        // We need to create this as a Scoped Service because all instances of editing the values of the database need to be editing the same database instance.
    }
}
