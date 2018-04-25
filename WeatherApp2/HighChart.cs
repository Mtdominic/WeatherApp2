namespace WeatherApp2
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using WeatherApp2.Models;

    public partial class HighChart : DbContext
    {
        public HighChart()
            : base("name=HighChart")
        {
        }

        public virtual DbSet<WeatherSnap> WeatherSnaps { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
