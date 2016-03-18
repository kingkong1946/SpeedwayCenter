using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection;

namespace SpeedwayCenter.Models.FluentApi
{
    public class SpeedwayCenterContext : DbContext
    {
        public SpeedwayCenterContext() : base("name=SpeedwayCenterContext")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var types = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(t => !string.IsNullOrEmpty(t.Namespace) &&
                            t.Namespace.Contains("Mapping") &&
                            t.BaseType != null &&
                            t.BaseType.IsGenericType &&
                            t.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));

            foreach (var type in types)
            {
                dynamic mappingInstance = Activator.CreateInstance(type);
                modelBuilder.Configurations.Add(mappingInstance);

                base.OnModelCreating(modelBuilder);
            }
        }
    }
}