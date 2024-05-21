using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Snackis2.Areas.Identity.Data;
using Snackis2.Models;

namespace Snackis2.Data;

public class Snackis2Context : IdentityDbContext<Snackis2User>
{
    public Snackis2Context(DbContextOptions<Snackis2Context> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }

    public DbSet<Snackis2.Models.Category> Category { get; set; } = default!;
    public DbSet<Snackis2.Models.Comment> Comment { get; set; } = default!;
    public DbSet<Snackis2.Models.Post> Post { get; set; } = default!;

public DbSet<Snackis2.Models.Message> Message { get; set; } = default!;

public DbSet<Snackis2.Models.Report> Report { get; set; } = default!;

  
}
