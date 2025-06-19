using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTKDotNetCore.ConsoleApp.Model
{
    public class DotNetTrainingBatch5Context : DbContext
    {
        public DotNetTrainingBatch5Context(DbContextOptions options) : base(options)
        {
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        string connectionString = "Data Source=DESKTOP-QREHFRH;Initial Catalog=DotNetTrainingBatch5;User ID=sa;Password=rider;TrustServerCertificate=True;";
        //        optionsBuilder.UseSqlServer(connectionString);
        //    }
        //}

        public DbSet<Blog> Blogs { get; set; }
    }
}