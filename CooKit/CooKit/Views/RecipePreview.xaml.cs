using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CooKit.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecipePreview
    {

        private string name_;
        public string Name
        {
            get => name_;
            set
            {
                if (name_ == value)
                    return;

                name_ = value;
                NameLabel.Text = name_;
            }
        }

        private ImageSource image_;
        public ImageSource Image
        {
            get => image_;
            set
            {
                if (image_ == value)
                    return;

                image_ = value;
                ImageImage.Source = image_;
            }
        }

        public RecipePreview()
        {
            InitializeComponent();
        }

        public RecipePreview([NotNull] string name, [NotNull] ImageSource image) : base()
        {
            Name = name;
            Image = image;
        }

    }
}