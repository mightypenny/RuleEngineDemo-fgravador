using Microsoft.EntityFrameworkCore;

namespace EmployeeService.Repositories.Postgres;

public class EmployeeDBContext : DbContext
{
    private readonly IConfiguration configuration;
    
    public DbSet<Employee> Employees { get; set; }

    public EmployeeDBContext(IConfiguration configuration)
    {
        this.configuration = configuration;
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(configuration.GetConnectionString("EmployeePostgres"));
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>()
            .Property(u => u.Id)
            .HasColumnName("id")
            .HasColumnType("varchar");
        
        modelBuilder.Entity<Employee>()
            .Property(u => u.LastName)
            .HasColumnName("lastname")
            .HasColumnType("varchar");

        modelBuilder.Entity<Employee>()
            .Property(u => u.FirstName)
            .HasColumnName("firstname")
            .HasColumnType("varchar");
        
        modelBuilder.Entity<Employee>()
            .Property(u => u.BirthDate)
            .HasColumnName("birthdate")
            .HasColumnType("date");
        
        modelBuilder.Entity<Employee>()
            .Property(u => u.Salary)
            .HasColumnName("salary")
            .HasColumnType("money");
        
        base.OnModelCreating(modelBuilder);
    }
}