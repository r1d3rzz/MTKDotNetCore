using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BlogWebAPI.Models;

public partial class DotNetTrainingBatch5Context : DbContext
{
    public DotNetTrainingBatch5Context()
    {
    }

    public DotNetTrainingBatch5Context(DbContextOptions<DotNetTrainingBatch5Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Blog> Blogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Blog>(entity =>
        {
            entity.ToTable("Blog");

            entity.Property(e => e.BlogAuthor).HasMaxLength(50);
            entity.Property(e => e.BlogContent).HasMaxLength(200);
            entity.Property(e => e.BlogTitle).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
