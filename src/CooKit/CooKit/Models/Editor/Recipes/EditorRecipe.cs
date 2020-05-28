﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CooKit.Models.Editor.Ingredients;
using CooKit.Models.Editor.Steps;
using CooKit.Models.Ingredients;
using CooKit.Models.Pictograms;
using CooKit.Models.Steps;

namespace CooKit.Models.Editor.Recipes
{
    public sealed class EditorRecipe : BaseEditorModel, IEditorRecipe
    {
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
            get => ObservableIngredients.Cast<IIngredient>().ToList();
            set => throw new NotSupportedException();
        }

        public IList<IPictogram> Pictograms
        {
            get => ObservablePictograms.ToList();
            set => throw new NotSupportedException();
        }

        public IList<IStep> Steps
        {
            get => ObservableSteps.Cast<IStep>().ToList();
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

        public ObservableCollection<IPictogram> ObservablePictograms
        {
            get => _observablePictograms;
            set => OnPropertyChanged(ref _observablePictograms, value);
        }

        public ObservableCollection<IEditorIngredient> ObservableIngredients
        {
            get => _observableIngredients;
            set => OnPropertyChanged(ref _observableIngredients, value);
        }

        public ObservableCollection<IEditorStep> ObservableSteps
        {
            get => _observableSteps;
            set => OnPropertyChanged(ref _observableSteps, value);
        }

        private ObservableCollection<string> _observableImages;
        private ObservableCollection<IPictogram> _observablePictograms;

        private ObservableCollection<IEditorIngredient> _observableIngredients;
        private ObservableCollection<IEditorStep> _observableSteps;

        #endregion
    }
}
