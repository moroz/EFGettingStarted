using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using EFCore.NamingConventions;
using System.Runtime.InteropServices;

namespace ConsoleApp.PostgreSQL
{
  public class BloggingContext : DbContext
  {
    public DbSet<Blog>? Blogs { get; set; }
    public DbSet<Post>? Posts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    => optionsBuilder.UseNpgsql(GetDatabaseConnectionString()).UseSnakeCaseNamingConvention();

    private string GetDatabaseConnectionString()
    {
      var connStr = Environment.GetEnvironmentVariable("DATABASE_URL");
      if (string.IsNullOrWhiteSpace(connStr))
      {
        throw new ArgumentNullException("Environment variable DATABASE_URL is not set!");
      }

      var parsed = new Uri(connStr);
      if (parsed == null)
      {
        throw new Exception("Could not parse DATABASE_URL");
      }

      var userInfo = parsed.UserInfo.Split(':', 2);
      var username = userInfo[0];
      var password = userInfo[1];
      var passwordSection = password == null ? "" : $";Password={password}";

      return $"Host={parsed.Host};Database={parsed.AbsolutePath.TrimStart('/')};Username={username}{passwordSection}";
    }
  }

  public class Blog
  {
    public int Id { get; set; }
    public required string Url { get; set; }
    public required List<Post> Posts { get; set; }
  }

  public class Post
  {
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Content { get; set; }
    public int BlogId { get; set; }
    public Blog? Blog { get; set; }
  }
}