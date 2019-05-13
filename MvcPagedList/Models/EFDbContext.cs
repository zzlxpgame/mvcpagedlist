using System.Data.Entity;

namespace MvcPagedList.Models
{
    public class EFDbContext : DbContext
    {
        public DbSet<Rkxx> Rkxxs { get; set; }

    }
}