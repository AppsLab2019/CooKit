using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CooKit.Models.Recipes
{
    public class MockRecipe : IRecipe
    {
        public string Name { get; }
        public int Difficulty { get; }
        public int TimeNeeded { get; }
        public ImageSource Image { get; }

        public MockRecipe()
        {
            var rand = new Random();

            Name = $"Recipe {rand.Next()}";
            Difficulty = rand.Next(10);
            TimeNeeded = rand.Next(10);
            Image = ImageSource.FromFile(rand.Next(2) == 0 ? "food.jpg" : "food2.jpg");
        }

    }
}
