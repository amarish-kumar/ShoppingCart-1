using Microsoft.EntityFrameworkCore;
using ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Persistence
{
    public class ShoppingCartDbContext : DbContext
    {
        public DbSet<Make> Makes { get; set; }
        public DbSet<Feature> Features { get; set; }

        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Model> Models { get; set; }
        //Keeping only make will resolve model as well as we are having collection of model in make,
        //however, if we need to explicitly query Model, then we need to have.
        //public DbSet<Model> Models { get; set; }
        public ShoppingCartDbContext(DbContextOptions<ShoppingCartDbContext> options) : base(options)
        {
            //It will look for connection string from appsettings

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Created Composite Key for VehicleFeature
            modelBuilder.Entity<VehicleFeature>().HasKey(vf =>
           new
           {
               vf.VehicleId,
               vf.FeatureId
           });
            base.OnModelCreating(modelBuilder);
        }
    }
}
