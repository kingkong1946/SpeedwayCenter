using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Reflection;

namespace SpeedwayCenter.ORM
{
    public class SpeedwayCenterContext : DbContext, IDatabaseContext
    {
        public SpeedwayCenterContext() : base("name=SpeedwayCenterContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SpeedwayCenterContext, MigrationConfiguration>("SpeedwayCenterContext"));
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

        public IQueryable<T> Get<T>() where T : class => Set<T>();
        public T Add<T>(T entity) where T : class => Set<T>().Add(entity);
        public T Delete<T>(T entity) where T : class => Set<T>().Remove(entity);
        public void Update<T>(T entity) where T : class => Set<T>().AddOrUpdate(entity);
        public void Save() => SaveChanges();
    }
}