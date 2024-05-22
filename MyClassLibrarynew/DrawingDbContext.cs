using MyClassLibrarynew.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Linq;
using System.Reflection.Metadata;


namespace MyClassLibrarynew
{
    public class DrawingDbContext:DbContext
    {
        public DrawingDbContext(DbContextOptions<DrawingDbContext> options) : base(options)
        {
            Database.SetCommandTimeout(TimeSpan.FromMinutes(5));
        }
        public virtual DbSet<Login> Logins { get; set; }
        public DbSet<Dimensions> Dimension { get; set; }
        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Conventions.Add(_ => new BlankTriggerAddingConvention());
        }
        
    }
    public class BlankTriggerAddingConvention : IModelFinalizingConvention
    {
        public virtual void ProcessModelFinalizing(
            IConventionModelBuilder modelBuilder,
            IConventionContext<IConventionModelBuilder> context)
        {
            foreach (var entityType in modelBuilder.Metadata.GetEntityTypes())
            {
                var table = StoreObjectIdentifier.Create(entityType, StoreObjectType.Table);
                if (table != null
                    && entityType.GetDeclaredTriggers().All(t => t.GetDatabaseName(table.Value) == null)
                    && (entityType.BaseType == null
                        || entityType.GetMappingStrategy() != RelationalAnnotationNames.TphMappingStrategy))
                {
                    entityType.Builder.HasTrigger(table.Value.Name + "_Trigger");
                }

                foreach (var fragment in entityType.GetMappingFragments(StoreObjectType.Table))
                {
                    if (entityType.GetDeclaredTriggers().All(t => t.GetDatabaseName(fragment.StoreObject) == null))
                    {
                        entityType.Builder.HasTrigger(fragment.StoreObject.Name + "_Trigger");
                    }
                }
            }
        }
    }
}