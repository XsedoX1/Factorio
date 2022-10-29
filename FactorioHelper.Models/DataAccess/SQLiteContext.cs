using FactorioHelper.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.IO;

namespace FactorioHelper.EFCore.DataAccess
{
    public class SQLiteContext : DbContext
    {

        public DbSet<Item> Items { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public string DbPath { get; }

        public SQLiteContext(DbContextOptions options) : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Ingredient>()
                .HasKey(k => new { k.AmountNeeded, k.TimeToCraftMainItem, k.ItemId });

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
                .HasForeignKey(ig => ig.ItemId)
                .HasPrincipalKey(it => it.ItemId)
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



    }
}
