using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RobberLanguageAPI.Models;

namespace RobberLanguageAPI.Data
{
    public class RobberTranslationDbContext : DbContext
    {
        public RobberTranslationDbContext(DbContextOptions<RobberTranslationDbContext> options)
            : base(options)
        {

        }

        public DbSet<Translation> Translations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Translation>().ToTable("Translation");
        }
    }
}
