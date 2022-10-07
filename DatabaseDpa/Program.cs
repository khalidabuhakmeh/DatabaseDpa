// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using DatabaseDpa;
using Microsoft.EntityFrameworkCore;

// LoadDatabase();

for (var i = 0; i < 10; i++)
{
    var query = new Database();
    var blogs = query.Blogs.Include(b => b.Posts).ToList();
    
    Console.WriteLine(blogs.Count);
}

var sw = new Stopwatch();
sw.Start();
var db = new Database();
db.Database.ExecuteSqlRaw("WAITFOR DELAY '00:00:10'");
sw.Stop();

Console.WriteLine($"Done! Time elapsed: {sw.Elapsed.TotalSeconds} seconds");

void LoadDatabase()
{
    var database = new Database();
    database.Database.Migrate();

    for (var i = 1; i <= 100; i++)
    {
        var blog = new Blog { Name = "Blog #{i}" };
        for (var j = 1; j <= 100; j++)
        {
            blog.Posts.Add(new Post { Name = $"{blog.Name} @ Post #{j}"});
        }

        database.Blogs.Add(blog);
        database.SaveChanges();
        database.ChangeTracker.Clear();
    }
}