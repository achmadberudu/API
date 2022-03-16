using API2.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API2.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {

        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Profiling> Profilings { get; set; }
        public DbSet<University>Universities{ get; set; }
        public DbSet<Role>Roles{ get; set; }
        public DbSet<AccountRole>AccountRoles{ get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasOne(a => a.Account)
                .WithOne(b => b.Employee)
                .HasForeignKey<Account>(b => b.NIK);

            modelBuilder.Entity<Profiling>()
                .HasOne(a => a.Account)
                .WithOne(b => b.Profiling)
                .HasForeignKey<Account>(b => b.NIK);

            modelBuilder.Entity<Education>()
               .HasMany(a => a.Profilings)
               .WithOne(b => b.Education)
               .HasForeignKey(b => b.Education_id);

            modelBuilder.Entity<University>()
               .HasMany(a => a.Educations)
               .WithOne(b => b.University)
               .HasForeignKey(b => b.University_id);

            modelBuilder.Entity<Role>()
                .HasMany(a => a.AccountRoles)
                .WithOne(b => b.Role)
                .HasForeignKey(b => b.RoleId);

            modelBuilder.Entity<Account>()
                .HasMany(a => a.AccountRoles)
                .WithOne(b => b.Account)
                .HasForeignKey(b => b.AccountNIK);


        }
    }
}
