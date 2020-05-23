using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using CooKit.Extensions;
using CooKit.Models.Ingredients;
using CooKit.Models.Pictograms;
using CooKit.Models.Recipes;
using CooKit.Models.Steps;

namespace CooKit.Models.Editor.Recipe
{
    public sealed class EditorRecipe : IEditorRecipe
    {
        public EditorRecipe()
        {
        }

        public EditorRecipe(IRecipe recipe)
        {
            if (recipe is null)
                throw new ArgumentNullException(nameof(recipe));

            _id = recipe.Id;
            _name = recipe.Name;
            _description = recipe.Description;
            _estimatedTime = recipe.EstimatedTime;
            _isFavorite = recipe.IsFavorite;

            _previewImage = recipe.PreviewImage;
            _observableImages = recipe.Images.ToObservableCollection();

            _observableIngredients = recipe.Ingredients.ToObservableCollection();
            _observablePictograms = recipe.Pictograms.ToObservableCollection();
            _observableSteps = recipe.Steps.ToObservableCollection();
        }

        #region IRecipe

        public Guid Id
        {
            get => _id;
            set => OnPropertyChanged(ref _id, value);
        }

        public string Name
        {
            get => _name;
            set => OnPropertyChanged(ref _name, value);
        }

        public string Description
        {
            get => _description;
            set => OnPropertyChanged(ref _description, value);
        }

        public int EstimatedTime
        {
            get => _estimatedTime;
            set => OnPropertyChanged(ref _estimatedTime, value);
        }

        public bool IsFavorite
        {
            get => _isFavorite;
            set => OnPropertyChanged(ref _isFavorite, value);
        }

        public string PreviewImage
        {
            get => _previewImage;
            set => OnPropertyChanged(ref _previewImage, value);
        }

        public IList<string> Images
        {
            get => ObservableImages.ToList();
            set => throw new NotSupportedException();
        }

        public IList<IIngredient> Ingredients
        {
            get => ObservableIngredients.ToList();
            set => throw new NotSupportedException();
        }

        public IList<IPictogram> Pictograms
        {
            get => ObservablePictograms.ToList();
            set => throw new NotSupportedException();
        }

        public IList<IStep> Steps
        {
            get => ObservableSteps.ToList();
            set => throw new NotSupportedException();
        }

        private Guid _id;
        private string _name;
        private string _description;
        private int _estimatedTime;
        private bool _isFavorite;
        private string _previewImage;

        #endregion

        #region IEditorRecipe

        public ObservableCollection<string> ObservableImages
        {
            get => _observableImages;
            set => OnPropertyChanged(ref _observableImages, value);
        }

        public ObservableCollection<IIngredient> ObservableIngredients
        {
            get => _observableIngredients;
            set => OnPropertyChanged(ref _observableIngredients, value);
        }

        public ObservableCollection<IPictogram> ObservablePictograms
        {
            get => _observablePictograms;
            set => OnPropertyChanged(ref _observablePictograms, value);
        }

        public ObservableCollection<IStep> ObservableSteps
        {
            get => _observableSteps;
            set => OnPropertyChanged(ref _observableSteps, value);
        }

        private ObservableCollection<string> _observableImages;
        private ObservableCollection<IIngredient> _observableIngredients;
        private ObservableCollection<IPictogram> _observablePictograms;
        private ObservableCollection<IStep> _observableSteps;

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged<T>(ref T backingField, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(backingField, value))
                return;

            backingField = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
