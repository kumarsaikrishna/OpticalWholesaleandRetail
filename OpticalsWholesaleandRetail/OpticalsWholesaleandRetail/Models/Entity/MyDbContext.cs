using Microsoft.EntityFrameworkCore;
using OpticalFibersRetailShop.Models.Entity;
using System.Collections.Generic;
using System.Linq;

namespace OpticalFibersRetailShop.Models.Entity
{
    public class MyDbContext : DbContext
    {

        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserEntity>().ToTable("Users");
            modelBuilder.Entity<UserTypeEntites>().ToTable("Roles");



        }


        public DbSet<UserEntity> userEntity { get; set; }
        public DbSet<UserTypeEntites> uTypeEntity { get; set; }


        //public void CreateDynamicTable(string tableName, List<string> subjects)
        //{
        //    string columns = string.Join(", ", subjects.Select(sub => $"[{sub}] INT NULL"));
        //    string query = $@"
        //    IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '{tableName}')
        //    BEGIN
        //        CREATE TABLE [{tableName}] (
        //            Id INT  PRIMARY KEY,
        //            StudentId INT NOT NULL,
        //            {columns}
        //        )
        //    END";

        //    this.Database.ExecuteSqlRaw(query);
        //}

    }


}
