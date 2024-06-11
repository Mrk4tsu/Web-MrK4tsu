using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Model.Entity
{
    public partial class MrKatsuWebDbContext : DbContext
    {
        public MrKatsuWebDbContext()
            : base("name=MrKatsuWebDbContext")
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Blog> Blogs { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<CategoryBlog> CategoryBlogs { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Feedback> Feedbacks { get; set; }
        public virtual DbSet<GroupUser> GroupUsers { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .Property(e => e.Username)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.GroupId)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .HasMany(e => e.Feedbacks)
                .WithOptional(e => e.Account)
                .HasForeignKey(e => e.CreatedBy);

            modelBuilder.Entity<Blog>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<Blog>()
                .Property(e => e.ModifyBy)
                .IsUnicode(false);

            modelBuilder.Entity<Blog>()
                .HasMany(e => e.Tags)
                .WithMany(e => e.Blogs)
                .Map(m => m.ToTable("TagBlog").MapLeftKey("BlogId").MapRightKey("TagId"));

            modelBuilder.Entity<Category>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<Category>()
                .HasOptional(e => e.Category1)
                .WithRequired(e => e.Category2);

            modelBuilder.Entity<CategoryBlog>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<CategoryBlog>()
                .Property(e => e.ModifyBy)
                .IsUnicode(false);

            modelBuilder.Entity<CategoryBlog>()
                .HasMany(e => e.Blogs)
                .WithOptional(e => e.CategoryBlog)
                .HasForeignKey(e => e.CategoryId);

            modelBuilder.Entity<CategoryBlog>()
                .HasMany(e => e.CategoryBlog1)
                .WithOptional(e => e.CategoryBlog2)
                .HasForeignKey(e => e.ParentId);

            modelBuilder.Entity<Comment>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<Comment>()
                .HasMany(e => e.Comment1)
                .WithOptional(e => e.Comment2)
                .HasForeignKey(e => e.ParentId);

            modelBuilder.Entity<GroupUser>()
                .Property(e => e.Id)
                .IsUnicode(false);

            modelBuilder.Entity<GroupUser>()
                .HasMany(e => e.Accounts)
                .WithOptional(e => e.GroupUser)
                .HasForeignKey(e => e.GroupId);

            modelBuilder.Entity<GroupUser>()
                .HasMany(e => e.Roles)
                .WithMany(e => e.GroupUsers)
                .Map(m => m.ToTable("Credential").MapLeftKey("GroupId").MapRightKey("RoleId"));

            modelBuilder.Entity<Product>()
                .Property(e => e.ProductCode)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.ModifyBy)
                .IsUnicode(false);

            modelBuilder.Entity<Role>()
                .Property(e => e.Id)
                .IsUnicode(false);

            modelBuilder.Entity<Tag>()
                .Property(e => e.Id)
                .IsUnicode(false);
        }
    }
}
