using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogWebAPI.Database.Models
{
    public class BlogDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connectionString = "Data Source = DESKTOP-QREHFRH; Initial Catalog = DotNetTrainingBatch5; User ID = sa; Password = rider; TrustServerCertificate = True";
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        public DbSet<Blog> Blogs { get; set; }
    }
}
