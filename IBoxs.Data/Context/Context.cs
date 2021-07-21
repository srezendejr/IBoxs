using IBoxs.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBoxs.Data.Context
{
    public class Context: DbContext
    {
        public Context()
            : base("name=Iboxs")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<Context, Migrations.Configuration>());
        }

        public Context(DbConnection existingConnection, bool contextOwnsConnection)
            : base(existingConnection, contextOwnsConnection)
        {
            Database.SetInitializer<Context>(null);
        }
        public virtual DbSet<Loja> Lojas { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Properties<string>().Configure(p => p.HasColumnType("varchar"));
            modelBuilder.Properties<string>().Configure(p => p.HasMaxLength(1000));

            this.Configuration.UseDatabaseNullSemantics = false;
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.AutoDetectChangesEnabled = true;
            this.Configuration.ValidateOnSaveEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;

            Mapping.Mapping.Map(modelBuilder);
        }
        public async Task Commit()
        {
            await base.SaveChangesAsync();
        }

        public void Salvar<T>(T entity) where T : class
        {
            Set<T>().Attach(entity);
            Entry(entity).State = EntityState.Added;
        }

        public void Alterar<T>(T entity) where T : class
        {
            Set<T>().Attach(entity);
            Entry(entity).State = EntityState.Modified;
        }

        public void Excluir<T>(T entity) where T : class
        {
            Set<T>().Attach(entity);
            Entry(entity).State = EntityState.Deleted;

        }

        public IDbSet<T> DbSet<T>() where T : class
        {
            return Set<T>();
        }

        public T Find<T>(params object[] keyValues) where T : class
        {
            return Set<T>().Find(keyValues);
        }

        public T Find<T>(string sql) where T : class
        {
            return Set<T>().SqlQuery(sql).FirstOrDefault<T>();
        }

        public List<T> RetornaLista<T>(string sql) where T : class
        {
            return Database.SqlQuery<T>(sql).ToList();
        }

        public void Remove<T>(T item) where T : class
        {
            try
            {
                Set<T>().Attach(item);
                Set<T>().Remove(item);
                Entry(item).State = EntityState.Deleted;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
