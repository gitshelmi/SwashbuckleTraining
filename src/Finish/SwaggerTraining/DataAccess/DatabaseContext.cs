using System;
using Microsoft.EntityFrameworkCore;
using SwaggerTraining.Models;

namespace SwaggerTraining.DataAccess
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
           : base(options) { }

        public DbSet<Developer> Developers { get; set; }
        public DbSet<Project> Projects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // seed the database with dummy data
            modelBuilder.Entity<Developer>().HasData(
                new Developer
                {
                    Id = Guid.Parse("d28888e9-2ba9-473a-a40f-e38cb54f9b35"),
                    FirstName = "Eddard",
                    LastName = "Stark",
                    DateOfBirth = new DateTime(1610, 7, 23),
                },
                new Developer
                {
                    Id = Guid.Parse("da2fd609-d754-4feb-8acd-c4f9ff13ba96"),
                    FirstName = "Jaime",
                    LastName = "Lannister",
                    DateOfBirth = new DateTime(1652, 8, 21),
                },
                new Developer
                {
                    Id = Guid.Parse("d902b665-1190-4c70-9915-b9c2d7680450"),
                    FirstName = "Daenerys",
                    LastName = "Targaryen",
                    DateOfBirth = new DateTime(1653, 1, 1),
                }
            );

            modelBuilder.Entity<Project>().HasData(
               new Project
               {
                   Id = Guid.Parse("5b1c2b4d-48c7-402a-80c3-cc796ad49c6b"),
                   DeveloperId = Guid.Parse("d28888e9-2ba9-473a-a40f-e38cb54f9b35"),
                   Name = "OpenAPI Training",
                   Description = "A training for OpenAPI and Swashbuckle."
               },
               new Project
               {
                   Id = Guid.Parse("d28888e9-2ba9-473a-a40f-e38cb54f9b35"),
                   DeveloperId = Guid.Parse("da2fd609-d754-4feb-8acd-c4f9ff13ba96"),
                   Name = "RESTful APIs Training",
                   Description = "A training for RESTful APIs in .NETCore MVC."
               },
               new Project
               {
                   Id = Guid.Parse("d28888e9-2ba9-473a-a40f-e38cb54f9ba2"),
                   DeveloperId = Guid.Parse("da2fd609-d754-4feb-8acd-c4f9ff13ba96"),
                   Name = "Docker Training",
                   Description = "A training for Docker."
               },
               new Project
               {
                   Id = Guid.Parse("5b1c2b4d-48c7-402a-80c3-cc796ad49c7a"),
                   DeveloperId = Guid.Parse("d902b665-1190-4c70-9915-b9c2d7680450"),
                   Name = "Git Training",
                   Description = "A training for Git."
               }
               );

            base.OnModelCreating(modelBuilder);
        }
    }
}
