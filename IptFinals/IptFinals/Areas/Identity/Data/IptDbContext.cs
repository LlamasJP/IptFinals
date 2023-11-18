//using AspNetCore;
using IptFinals.Areas.Identity.Data;
using IptFinals.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IptFinals.Data;

public class IptDbContext : IdentityDbContext<IptUser>
{

    
    public IptDbContext(DbContextOptions<IptDbContext> options)
        : base(options)
    {
    }
    public DbSet<PersonalInfo> PersonalInfo { get; set; }

protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}
