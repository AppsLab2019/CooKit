﻿using System;
using CooKit.Models.Ingredients;

namespace CooKit.Models.Editor.Ingredients
{
    public sealed class EditorIngredient : BaseEditorModel, IEditorIngredient
    {
        public EditorIngredient()
        {
        }

        public EditorIngredient(IIngredient ingredient)
        {
            if (ingredient is null)
                throw new ArgumentNullException(nameof(ingredient));

            _template = ingredient.Template;
            _note = ingredient.Note;
            _quantity = ingredient.Quantity;
        }

        public IIngredientTemplate Template
        {
            get => _template;
            set => OnPropertyChanged(ref _template, value);
        }

        public string Note
        {
            get => _note;
            set => OnPropertyChanged(ref _note, value);
        }

        public float Quantity
        {
            get => _quantity;
            set => OnPropertyChanged(ref _quantity, value);
        }

        private IIngredientTemplate _template;
        private string _note;
        private float _quantity;
    }
}
