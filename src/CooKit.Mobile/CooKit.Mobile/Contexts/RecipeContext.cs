using CooKit.Mobile.Contexts.Entities;
using CooKit.Mobile.Models;
using CooKit.Mobile.Models.Ingredients;
using CooKit.Mobile.Models.Steps;
using Microsoft.EntityFrameworkCore;

namespace CooKit.Mobile.Contexts
{
    public class RecipeContext : DbContext
    {
        public DbSet<Pictogram> Pictograms { get; set; }
        public DbSet<RecipeEntity> Recipes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Pictogram>(entityBuilder =>
            {
                entityBuilder.HasIndex(pictogram => pictogram.Id).IsUnique();
                entityBuilder.OwnsOne(pictogram => pictogram.Icon);
            });

            modelBuilder.Entity<Ingredient>(entityBuilder =>
            {
                entityBuilder.Property<int>("Id");
                entityBuilder.HasKey("Id");
            });

            modelBuilder.Entity<Step>(entityBuilder =>
            {
                entityBuilder.Property<int>("Id");
                entityBuilder.HasKey("Id");
            });

            modelBuilder.Entity<TextStep>().HasBaseType<Step>();

            modelBuilder.Entity<ImageStep>(entityBuilder =>
            {
                entityBuilder.HasBaseType<Step>();
                entityBuilder.OwnsOne(imageStep => imageStep.Image);
            });

            // configure many to many relationship between recipes and pictograms
            modelBuilder.Entity<RecipePictogramPair>(entityBuilder =>
            {
                entityBuilder.HasKey(pair => new { pair.RecipeId, pair.PictogramId });
                entityBuilder.HasIndex(pair => pair.RecipeId);

                entityBuilder.HasOne(pair => pair.Recipe)
                    .WithMany(pair => pair.Pictograms)
                    .HasForeignKey(pair => pair.RecipeId);

                entityBuilder.HasOne(pair => pair.Pictogram)
                    .WithMany()
                    .HasForeignKey(pair => pair.PictogramId);
            });

            modelBuilder.Entity<RecipeEntity>(entityBuilder =>
            {
                entityBuilder.HasIndex(recipe => recipe.Id).IsUnique();

                entityBuilder.OwnsOne(recipe => recipe.PreviewImage);
                entityBuilder.OwnsMany(recipe => recipe.Images, ownedBuilder =>
                {
                    ownedBuilder.Property<int>("Id");
                    ownedBuilder.HasKey("Id");
                });

                // because inheritance can't be used with owned types
                entityBuilder.HasMany(recipe => recipe.Ingredients)
                    .WithOne()
                    .OnDelete(DeleteBehavior.Cascade);

                entityBuilder.HasMany(recipe => recipe.Steps)
                    .WithOne()
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }

        public RecipeContext(DbContextOptions options) : base(options)
        {
        }
    }
}
