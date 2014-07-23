namespace ProductReviewApp.Domain
{
    using System.Data.Entity;

    public partial class RoundtuitDbContext : DbContext
    {
        public RoundtuitDbContext()
            : base("DefaultConnection")
        {
            Database.SetInitializer(new DatabaseInitializer(this));
        }

        public virtual DbSet<Feature> Features { get; set; }
        public virtual DbSet<Feedback> Feedback { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Feature>()
                .HasMany(e => e.Feedback)
                .WithRequired(e => e.Feature)
                .WillCascadeOnDelete(false);
        }
    }
}
