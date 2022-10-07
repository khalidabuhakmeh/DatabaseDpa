using Microsoft.EntityFrameworkCore;

namespace DatabaseDpa;

public class Database : DbContext
{
    public DbSet<Blog> Blogs => Set<Blog>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        => optionsBuilder.UseSqlServer("Server=localhost,11433;Database=dpa;User Id=sa;Password=Pass123!;Encrypt=false");
}

public class Blog
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Post> Posts { get; set; } = new();
}

public class Post
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; } = new();
    public Blog Blog { get; set; } = default!;
}