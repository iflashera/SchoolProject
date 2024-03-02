using DataContext.EntityConfiguration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models.Models.Access;
using Models.Models.Classes;
using Models.Models.Identity;
using Models.Models.Roles;
using Models.Models.Users;


namespace DataContext;

public class SmsDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
{
    public SmsDbContext(DbContextOptions<SmsDbContext> options)
    : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new AdminConfiguration());
        modelBuilder.ApplyConfiguration(new ParentConfiguration());
        modelBuilder.ApplyConfiguration(new TeacherConfiguration());
        modelBuilder.ApplyConfiguration(new StudentConfiguration());

    }
    public DbSet<AccessInRole> AccessInRoles { get; set; }
    public DbSet<ApplicationAction> ApplicationActions { get; set; }
    public DbSet<ApplicationController> ApplicationControllers { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Class> Classes { get; set; }
    public DbSet<Subject> Subjects { get; set; }
    public DbSet<Admin> Admins { get; set; }
    public DbSet<Parent> Parents { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<UserDetail> UserDetails { get; set; }

}
