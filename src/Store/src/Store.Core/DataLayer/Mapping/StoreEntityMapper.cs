using System.Composition.Hosting;
using System.Reflection;

namespace Store.Core.DataLayer.Mapping
{
    public class StoreEntityMapper : EntityMapper
    {
        public StoreEntityMapper()
        {
            // Get current assembly
            var currentAssembly = typeof(StoreDbContext).GetTypeInfo().Assembly;

            // Create assemblies array
            var assemblies = new[] { currentAssembly };

            // Get configuration for container from current assembly
            var configuration = new ContainerConfiguration().WithAssembly(currentAssembly);

            // Create container for exports
            using (var container = configuration.CreateContainer())
            {
                // Get all definitions that implement IEntityMap interface
                Mappings = container.GetExports<IEntityMap>();
            }
        }
    }
}
