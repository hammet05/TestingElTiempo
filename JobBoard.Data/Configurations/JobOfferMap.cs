using JobBoard.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace JobBoard.Data.Configurations
{
    public class JobOfferMap : EntityTypeConfiguration<JobOffer>
    {
        public JobOfferMap()
        {
            ToTable("JobOffers");
            HasKey(x => x.Id);

            Property(x => x.Title).IsRequired().HasMaxLength(120);
            Property(x => x.Description).IsRequired();
            Property(x => x.Location).IsRequired().HasMaxLength(120);
            Property(x => x.Salary).HasPrecision(18, 2);
            Property(x => x.PublishedAt).IsRequired();
        }
    }
}
