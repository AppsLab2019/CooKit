using System;
using System.Threading.Tasks;

namespace CooKit.ViewModels
{
    public interface IViewModel
    {
        Task InitializeAsync(object parameter);

        public static bool IsValidImplementation(Type type)
        {
            return !type.IsInterface
                   && type.IsSealed
                   && typeof(IViewModel).IsAssignableFrom(type);
        }
    }
}
