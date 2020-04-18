﻿using Autofac;
using CooKit.ViewModels.Editor;
using CooKit.ViewModels.Recipes;

namespace CooKit.ViewModels
{
    public class ViewModelModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<RecipeListViewModel>();
            builder.RegisterType<RecipeViewModel>();

            builder.RegisterType<EditorStartViewModel>();
            builder.RegisterType<EditorMainViewModel>();

            builder.RegisterType<EditorPictogramMenuViewModel>();
        }
    }
}
