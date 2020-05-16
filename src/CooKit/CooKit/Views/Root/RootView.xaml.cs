using System;
using Xamarin.Forms;

namespace CooKit.Views.Root
{
    public partial class RootView
    {
        public RootView(Page page)
        {
            if (page is null)
                throw new ArgumentNullException(nameof(page));

            InitializeComponent();
            Detail = page;
        }
    }
}