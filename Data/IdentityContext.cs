using ExamForms.Models.Accounts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace ExamForms.Data;

public class IdentityContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
{
    public IdentityContext(DbContextOptions<IdentityContext> options)
        : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}
