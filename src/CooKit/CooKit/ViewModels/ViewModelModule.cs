using Autofac;

namespace CooKit.ViewModels
{
    public class ViewModelModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(ThisAssembly)
                .Where(type => type.Name.EndsWith("ViewModel") && IViewModel.IsValidImplementation(type))
                .AsSelf();
        }
    }
}
