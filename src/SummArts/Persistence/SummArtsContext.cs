using Microsoft.EntityFrameworkCore;
using SummArts.Models;

namespace SummArts.Persistence
{
    public class SummArtsContext : DbContext
    {
        public SummArtsContext (
            DbContextOptions<SummArtsContext> options)
            : base(options)
        {
        }

        public DbSet<Summary> Summary { get; set; }
    }
}