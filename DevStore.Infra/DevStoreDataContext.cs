using DevStore.Domain;
using DevStore.Infra.Mappings;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevStore.Infra
{ 
    public class DevStoreDataContext : DbContext
    {
        public DevStoreDataContext(): base("DevStoreConnectionString")
        {
            Database.SetInitializer(new DevStoreContextInitializer());

            this.Configuration.LazyLoadingEnabled = false;
        }

        public IDbSet<Product> Products { get; set; }

        public IDbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ProductMap());
            modelBuilder.Configurations.Add(new CategoryMap());
            base.OnModelCreating(modelBuilder);
        }
    }

    public class DevStoreContextInitializer : DropCreateDatabaseIfModelChanges<DevStoreDataContext> {

        protected override void Seed(DevStoreDataContext context)
        {
            context.Categories.Add(new Category { Id = 1, Title = "Informática" });
            context.Categories.Add(new Category { Id = 2, Title = "Games" });
            context.Categories.Add(new Category { Id = 3, Title = "Papelaria" });
            context.SaveChanges();

            context.Products.Add(new Product { Id = 1, Title = "Product 1", CategoryId = 1, IsActive = true, Price = 29.9M });
            context.Products.Add(new Product { Id = 2, Title = "Product 2", CategoryId = 1, IsActive = true, Price = 29.9M });
            context.Products.Add(new Product { Id = 3, Title = "Product 3", CategoryId = 1, IsActive = true, Price = 29.9M });

            context.Products.Add(new Product { Id = 4, Title = "Product 4", CategoryId = 2, IsActive = true, Price = 29.9M });
            context.Products.Add(new Product { Id = 5, Title = "Product 5", CategoryId = 2, IsActive = true, Price = 29.9M });
            context.Products.Add(new Product { Id = 6, Title = "Product 6", CategoryId = 2, IsActive = true, Price = 29.9M });

            context.Products.Add(new Product { Id = 7, Title = "Product 7", CategoryId = 3, IsActive = true, Price = 29.9M });
            context.Products.Add(new Product { Id = 8, Title = "Product 8", CategoryId = 3, IsActive = true, Price = 29.9M });
            context.Products.Add(new Product { Id = 9, Title = "Product 9", CategoryId = 3, IsActive = true, Price = 29.9M });
            context.SaveChanges();


            base.Seed(context);
        }
    }
}
