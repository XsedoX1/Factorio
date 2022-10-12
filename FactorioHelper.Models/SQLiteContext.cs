using FactorioHelper.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioHelper.Logic
{
    public class SQLiteContext : DbContext
    {
        public DbSet<Item>  Items { get; set; }
        public DbSet<Ingredient> Ingredients {get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Ingredient>()
                .HasKey(k => new { k.AmountNeeded, k.TimeToCraftMainItem, k.ItemId});

            modelBuilder.Entity<Item>()
                .HasKey(k => k.ItemId);

            modelBuilder.Entity<Item>()
                .HasMany(it => it.Ingredients)
                .WithOne()
                .HasForeignKey(ig => ig.MainItemId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Ingredient>()
                .HasOne(ig => ig.Item)
                .WithMany()
                .HasForeignKey(ig=>ig.ItemId)
                .HasPrincipalKey(it=>it.ItemId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Ingredient>()
               .Property(ig => ig.TimeToCraftMainItem)
               .ValueGeneratedNever();

            modelBuilder.Entity<Ingredient>()
               .Property(ig => ig.AmountNeeded)
               .ValueGeneratedNever();

            modelBuilder.Entity<Ingredient>()
               .Property(ig => ig.ItemId)
               .ValueGeneratedNever();

        }

        public string DbPath { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            path = Path.Join(path, "FactorioHelperData\\");
            DbPath = Path.Join(path, "Items.db");
            options.UseSqlite($"Data Source={DbPath}");
        }

        
    }
}
