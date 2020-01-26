using CooKit.Models.Recipes;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace CooKit.ViewModels
{
    public class RecipeIntroductionViewModel : INotifyPropertyChanged
    {

        private IRecipe recipe_;
        public IRecipe Recipe
        {
            get => recipe_;
            set
            {
                if (recipe_ == value)
                    return;

                recipe_ = value;
                
            }
        }

        private ImageSource image_;
        public ImageSource Image
        {
            get => image_;
            set => HandlePropertyChange(ref image_, value);
        }

        public RecipeIntroductionViewModel([NotNull] IRecipe recipe) => Recipe = recipe;

        public event PropertyChangedEventHandler PropertyChanged;

        public void HandlePropertyChange<T>(ref T field, T value, [CallerMemberName] string caller = null)
        {
            if (field.Equals(value))
                return;

            field = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
        }

    }
}
