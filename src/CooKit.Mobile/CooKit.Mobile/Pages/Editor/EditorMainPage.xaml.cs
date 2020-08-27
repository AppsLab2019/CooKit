using System;
using System.Collections.Generic;
using System.Diagnostics;
using CooKit.Mobile.Factories.Views;
using CooKit.Mobile.Views.Editor;
using Xamarin.Forms;

namespace CooKit.Mobile.Pages.Editor
{
    public partial class EditorMainPage
    {
        private readonly IViewFactory _viewFactory;
        private readonly IList<DataTemplate> _stepTemplates;

        public EditorMainPage(IViewFactory viewFactory, DataTemplate template)
        {
            _viewFactory = viewFactory;
            InitializeComponent();

            // TODO: maybe move this logic later
            _stepTemplates = new[]
            {
                ViewToTemplate<EditorNameView>(),
                ViewToTemplate<EditorDescriptionView>(),
                ViewToTemplate<EditorImageView>(),
                ViewToTemplate<EditorPictogramView>(),
                ViewToTemplate<EditorIngredientView>(),
                ViewToTemplate<EditorStepView>()
            };

            CreationCarousel.ItemTemplate = template;
            CreationCarousel.ItemsSource = _stepTemplates;
        }

        private DataTemplate ViewToTemplate<T>() where T : View
        {
            return new DataTemplate(() =>
            {
                var view = _viewFactory.CreateView(typeof(T));
                Debug.Assert(view != null);
                view.BindingContext = BindingContext;
                return new ContentView { Content = view };
            });
        }

        private void OnPrevClicked(object sender, EventArgs e)
        {
            CreationCarousel.Position--;
        }

        private void OnNextClicked(object sender, EventArgs e)
        {
            CreationCarousel.Position++;
        }

        private void OnPositionChanged(object sender, PositionChangedEventArgs e)
        {
            var position = e.CurrentPosition;
            PrevButton.IsVisible = position > 0;

            var isLast = position + 1 >= _stepTemplates.Count;
            NextButton.IsVisible = !isLast;
            PublishButton.IsVisible = isLast;
        }
    }
}