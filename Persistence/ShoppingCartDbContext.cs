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
        public ShoppingCartDbContext(DbContextOptions<ShoppingCartDbContext> options):base(options)
        {
            //It will look for connection string from appsettings

        }

        public DbSet<Make> Makes { get; set; }
        public DbSet<Feature> Features { get; set; }

        //Keeping only make will resolve model as well as we are having collection of model in make,
        //however, if we need to explicitly query Model, then we need to have.
        //public DbSet<Model> Models { get; set; }
    }
}
