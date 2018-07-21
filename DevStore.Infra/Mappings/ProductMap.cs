using DevStore.Domain;
using System.Data.Entity.ModelConfiguration;

namespace DevStore.Infra.Mappings
{
    class ProductMap : EntityTypeConfiguration<Product>
    {
        public ProductMap()
        {
            this.ToTable("Product")
                .HasKey(x => x.Id);

            this.Property(x => x.Title).HasMaxLength(160).IsRequired();

            this.Property(x => x.Price).IsRequired();

            this.Property(x => x.AcquireDate).IsRequired();

            this.HasRequired(x => x.Category);

            
        }
    }
}
