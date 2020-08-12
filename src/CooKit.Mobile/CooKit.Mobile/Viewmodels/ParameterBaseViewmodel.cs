using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace CooKit.Mobile.Viewmodels
{
    public abstract class ParameterBaseViewmodel<T> : BaseViewmodel
    {
        protected abstract Task InitializeAsync(T parameter);

        [EditorBrowsable(EditorBrowsableState.Never)]
        public sealed override Task InitializeAsync(object parameter)
        {
            if (parameter == null)
                return InitializeAsync(default);

            if (parameter is T castParameter)
                return InitializeAsync(castParameter);

            throw new ArgumentException($"Unexpected parameter of type {parameter.GetType().Name}");
        }
    }
}
