using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Autofac;
using CooKit.Models;
using CooKit.Services;
using Xamarin.Forms;

namespace CooKit.ViewModels.Editor.Designers
{
    public abstract class BaseSimpleDesignerViewModel<T, TBuilder> : BaseViewModel where T : IStorable
    {
        public TBuilder Builder { get; protected set; }
        public ICommand BuildCommand { get; }

        public ObservableCollection<string> AvailableImageLoaders { get; }

        protected IStoreBase<T, TBuilder> Store;

        protected BaseSimpleDesignerViewModel(IStoreBase<T, TBuilder> store)
        {
            Store = store;
            Builder = store.CreateBuilder();
            BuildCommand = new Command(HandleBuild);

            var loaderNames = App
                .Container.Resolve<IImageStore>()
                .RegisteredLoaders
                .Select(loader => loader.Name);

            AvailableImageLoaders = new ObservableCollection<string>(loaderNames);
        }

        protected virtual Task Build() =>
            Store.AddAsync(Builder);

        protected virtual async void HandleBuild()
        {
            using (await DisplayLoadingDialog("Building...."))
                await Build();

            await PopAsync();
        }
    }
}
