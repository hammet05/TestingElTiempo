using JobBoard.Domain.Entities;
using System.Data.Entity;

namespace JobBoard.Data
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext() : base("name=AppDbConnection")
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }

        public virtual DbSet<JobOffer> JobOffers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.AddFromAssembly(typeof(AppDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
