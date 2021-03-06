using System.Composition;
using Microsoft.EntityFrameworkCore;
using Store.Core.EntityLayer.HumanResources;

namespace Store.Core.DataLayer.Mapping.HumanResources
{
    [Export(typeof(IEntityMap))]
    public class EmployeeMap : IEntityMap
    {
        public void Map(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<Employee>();

            entity.ToTable("Employee", "HumanResources");

            entity.HasKey(p => p.EmployeeID);

            entity.Property(p => p.EmployeeID).UseSqlServerIdentityColumn();

            entity.Property(p => p.FirstName).HasColumnType("varchar(25)").IsRequired();

            entity.Property(p => p.MiddleName).HasColumnType("varchar(25)");

            entity.Property(p => p.LastName).HasColumnType("varchar(25)").IsRequired();

            entity.Property(p => p.BirthDate).HasColumnType("datetime").IsRequired();

            entity.Property(p => p.CreationUser).HasColumnType("varchar(25)").IsRequired();

            entity.Property(p => p.CreationDateTime).HasColumnType("datetime").IsRequired();

            entity.Property(p => p.LastUpdateUser).HasColumnType("varchar(25)");

            entity.Property(p => p.LastUpdateDateTime).HasColumnType("datetime");

            entity.Property(p => p.Timestamp).ValueGeneratedOnAddOrUpdate().IsConcurrencyToken();
        }
    }
}
