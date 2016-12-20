using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace ConsoleApplication
{
    public class MainContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Job> Jobs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=tcp:pasta.database.windows.net,1433;Initial Catalog=test;Persist Security Info=False;User ID=pasta;Password=DbTest999;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }
    }

    public class User
    {
        public int Id { get; set; }
        [Required] public string Name { get; set; }
        [Required] public string Mail { get; set; }
    }

    public class Job
    {
        public int Id { get; set; }
        [Required] public string Title { get; set; }
        [Required] public string Category { get; set; }
        [Required] public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public User CreateUser { get; set; }
    }
}