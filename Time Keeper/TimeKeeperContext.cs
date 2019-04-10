namespace Time_Keeper
{
    using System.Data.Entity;

    public partial class TimeKeeperContext : DbContext
    {
        public TimeKeeperContext()
            : base("name=TimeKeeperDBConnectionString")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<TimeKeeperContext>());
        }

        public virtual DbSet<Date> Dates { get; set; }
        public virtual DbSet<Entry> Entries { get; set; }
        public virtual DbSet<Total> Totals { get; set; }
        public virtual DbSet<Program> Programs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
