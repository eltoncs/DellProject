using System;
using InsuranceServices.Domain.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace InsuranceServices.Infra.Data.Context
{
    public class IdentityContext : IdentityDbContext<ApplicationUser>, IDisposable
    {
        //private const string SchemaPad = "pad_identity";


        public IdentityContext()
            : base("name=InsuranceDB")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Properties()
                .Where(p => p.Name == p.ReflectedType.Name + "_Id")
                .Configure(p => p.IsKey());
            modelBuilder.Properties()
                .Where(p => p.Name == p.ReflectedType.Name + "_ID")
                .Configure(p => p.IsKey());

            modelBuilder.Properties<string>()
                .Configure(p => p.HasColumnType("varchar"));

            modelBuilder.Properties<string>()
                .Configure(p => p.HasMaxLength(120));


            base.OnModelCreating(modelBuilder);
        }

        public static IdentityContext Create()
        {
            return new IdentityContext();
        }

    }
}
