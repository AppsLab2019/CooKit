﻿using CooKit.Views;
using Xamarin.Forms;

namespace CooKit
{
    public partial class AppShell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("introduction", typeof(RecipeIntroduction));
        }
    }
}