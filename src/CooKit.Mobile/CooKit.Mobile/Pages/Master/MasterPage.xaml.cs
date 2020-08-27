using System.Windows.Input;
using CooKit.Mobile.Providers.Page.MasterDetail;
using Xamarin.Forms;

namespace CooKit.Mobile.Pages.Master
{
    // HACK: ListView doesn't have a command that is executed when selected item is changed
    // TODO: remove the logic out of this class and replace it with custom behavior
    public partial class MasterPage
    {
        public static readonly BindableProperty SelectCommandProperty =
            BindableProperty.Create(nameof(SelectCommand), typeof(ICommand), typeof(MasterPage));

        private readonly IMasterDetailPageProvider _pageProvider;

        public MasterPage(IMasterDetailPageProvider pageProvider)
        {
            _pageProvider = pageProvider;

            InitializeComponent();
            SetBinding(SelectCommandProperty, new Binding("SelectCommand"));
        }

        private void OnEntrySelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (SelectCommand == null)
                return;

            var entry = e.SelectedItem;

            if (SelectCommand.CanExecute(entry))
                SelectCommand.Execute(entry);

            var masterDetailPage = _pageProvider.GetMasterDetailPage();
            masterDetailPage.IsPresented = false;
        }

        public ICommand SelectCommand
        {
            get => (ICommand) GetValue(SelectCommandProperty);
            set => SetValue(SelectCommandProperty, value);
        }
    }
}