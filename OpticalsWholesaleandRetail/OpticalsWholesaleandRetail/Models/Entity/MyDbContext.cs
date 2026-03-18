using Microsoft.EntityFrameworkCore;
using OpticalsWholesaleandRetail.Models.Entity;
using System.Collections.Generic;
using System.Linq;

namespace OpticalsWholesaleandRetail.Models.Entity
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
            modelBuilder.Entity<Customer>().ToTable("Customers");
            modelBuilder.Entity<Suppliers>().ToTable("Suppliers");
            modelBuilder.Entity<OrdersEntity>().ToTable("Orders");
            modelBuilder.Entity<OrderItemEntity>().ToTable("OrderItems");
            modelBuilder.Entity<FrameBrandEntity>().ToTable("FrameBrands");
            modelBuilder.Entity<FrameModelEntity>().ToTable("FrameModels");
            modelBuilder.Entity<FrameSizeEntity>().ToTable("FrameSizes");
            modelBuilder.Entity<FrameCategoryEntity>().ToTable("FrameCategories");
            modelBuilder.Entity<FrameEntity>().ToTable("Frames");
            modelBuilder.Entity<LensTypeEntity>().ToTable("LensTypes");
            modelBuilder.Entity<LensCategoryEntity>().ToTable("LensCategories");
            modelBuilder.Entity<LensMaterialEntity>().ToTable("LensMaterials");
            modelBuilder.Entity<LensIndexEntity>().ToTable("LensIndexes");
            modelBuilder.Entity<LensCoatingEntity>().ToTable("LensCoatings");
            modelBuilder.Entity<LensTintEntity>().ToTable("LensTints");



        }


        public DbSet<UserEntity> userEntity { get; set; }
        public DbSet<UserTypeEntites> uTypeEntity { get; set; }
        public DbSet<Customer> customerEntity { get; set; }
        public DbSet<Suppliers> suppliersEntity { get; set; }
        public DbSet<OrderItemEntity> OrderItemEntities { get; set; }
        public DbSet<OrdersEntity> ordersEntities { get; set; }
        public DbSet<FrameBrandEntity> fBrandEntity { get; set; }
        public DbSet<FrameModelEntity> fModelEntity { get; set; }
        public DbSet<FrameSizeEntity> fSizeEntity { get; set; }
        public DbSet<FrameCategoryEntity> fCategoryEntities { get; set; }
        public DbSet<FrameEntity> frameEntities { get; set; }
        public DbSet<LensTypeEntity> LTypeEntities { get; set; }
        public DbSet<LensCategoryEntity> LCategoryEntity { get; set; }
        public DbSet<LensMaterialEntity> LMaterialEntity { get; set; }
        public DbSet<LensCoatingEntity> LCoatingEntity { get; set; }
        public DbSet<LensIndexEntity> LIndexEntities { get; set; }
        public DbSet<LensTintEntity> LTintEntities { get; set; }


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
