using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Blog.Database.Models;

public partial class BlogContext : DbContext
{
    public BlogContext()
    {
    }

    public BlogContext(DbContextOptions<BlogContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblBlog> TblBlogs { get; set; }

    public virtual DbSet<TblUser> TblUsers { get; set; }

    public virtual DbSet<TblLogin> TblLogins { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblBlog>(entity =>
        {
            entity.ToTable("Tbl_Blog");

            entity.Property(e => e.TblBlogId).HasColumnName("Tbl_BlogId");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Detail).HasMaxLength(255);
            entity.Property(e => e.Slug).HasMaxLength(50);
            entity.Property(e => e.TblUserId).HasColumnName("Tbl_UserId");
            entity.Property(e => e.TblCategoryId).HasColumnName("Tbl_CategoryId");
            entity.Property(e => e.Title).HasMaxLength(50);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<TblUser>(entity =>
        {
            entity.ToTable("Tbl_User");

            entity.Property(e => e.TblUserId).HasColumnName("Tbl_UserId");
            entity.Property(e => e.Avatar).HasMaxLength(50);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        modelBuilder.Entity<TblCategory>(entity =>
        {
            entity.ToTable("Tbl_Category");

            entity.Property(e => e.TblCategoryId).HasColumnName("Tbl_CategoryId");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<TblLogin>(entity =>
        {
            entity.ToTable("Tbl_Login");

            entity.Property(e => e.TblLoginId).HasColumnName("Tbl_LoginId");
            entity.Property(e => e.SessionExpired).HasColumnType("datetime");
            entity.Property(e => e.SessionId).HasMaxLength(50);
            entity.Property(e => e.TblUserId).HasColumnName("Tbl_UserId");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
