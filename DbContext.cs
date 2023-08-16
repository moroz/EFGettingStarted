using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using EFCore.NamingConventions;

namespace ConsoleApp.PostgreSQL
{
  public class BloggingContext : DbContext
  {
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    => optionsBuilder.UseNpgsql("postgres://postgres:postgres@localhost/postgres_demo?sslmode=disable").UseSnakeCaseNamingConvention();
  }

  public class Blog
  {
    public int Id { get; set; }
    public string Url { get; set; }
    public List<Post> Posts { get; set; }
  }

  public class Post
  {
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public int BlogId { get; set; }
    public Blog Blog { get; set; }
  }
}