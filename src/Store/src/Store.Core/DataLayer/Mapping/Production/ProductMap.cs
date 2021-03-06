using System.Composition;
using Microsoft.EntityFrameworkCore;
using Store.Core.EntityLayer.Production;

namespace Store.Core.DataLayer.Mapping.Production
{
    [Export(typeof(IEntityMap))]
    public class ProductMap : IEntityMap
    {
        public void Map(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<Product>();

            entity.ToTable("Product", "Production");

            entity.HasKey(p => p.ProductID);

            entity.Property(p => p.ProductID).UseSqlServerIdentityColumn();

            entity.HasOne(p => p.ProductCategoryFk).WithMany(b => b.Products).HasForeignKey(p => p.ProductCategoryID).HasConstraintName("fk_Product_ProductCategoryID_ProductCategory");

            entity.HasAlternateKey(p => new { p.ProductName }).HasName("U_ProductName");

            entity.Property(p => p.ProductName).HasColumnType("varchar(100)").IsRequired();

            entity.Property(p => p.ProductCategoryID).HasColumnType("int").IsRequired();

            entity.Property(p => p.UnitPrice).HasColumnType("decimal(8, 4)").IsRequired();

            entity.Property(p => p.Description).HasColumnType("varchar(255)");

            entity.Property(p => p.Discontinued).HasColumnType("bit").IsRequired();

            entity.Property(p => p.CreationUser).HasColumnType("varchar(25)").IsRequired();

            entity.Property(p => p.CreationDateTime).HasColumnType("datetime").IsRequired();

            entity.Property(p => p.LastUpdateUser).HasColumnType("varchar(25)");

            entity.Property(p => p.LastUpdateDateTime).HasColumnType("datetime");

            entity.Property(p => p.Timestamp).ValueGeneratedOnAddOrUpdate().IsConcurrencyToken();
        }
    }
}
