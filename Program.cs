using ConsoleApp.PostgreSQL;

using var db = new BloggingContext();

Console.WriteLine("Querying for posts");
var posts = db.Posts!.OrderBy(b => b.Id)
    .ToList();

foreach (var post in posts)
{
  Console.WriteLine($"Post: {post.Title}");
}