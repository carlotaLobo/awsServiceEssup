
using Microsoft.EntityFrameworkCore;
using ModelsEssup.Models;
using System;
using System.Collections.Generic;
//using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;


namespace ServiceEssupProject_Azure.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions options) : base(options) { }
        public DbSet<Provider> Provider { get; set; }
        public DbSet<Business> Business { get; set; }
        public DbSet<Materials> Material { get; set; }
        public DbSet<Sector> Sector { get; set; }
        public DbSet<Point> Point { get; set; }
        public DbSet<Likes> Like { get; set; }
        public DbSet<ProviderMaterial> ProviderMaterial { get; set; }
        public DbSet<SectorMaterial> SectorMaterial { get; set; }
        public DbSet<SectorProvider> SectorProvider { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<ProvidersMaterialView> providersMaterialViews { get; set; }
        public DbSet<SectorProviderView> SectorProviderViews { get; set; }

   
    }
}
