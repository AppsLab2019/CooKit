using System;

namespace CooKit.Mobile.Registries.PageViewmodel
{
    public interface IPageViewmodelTypeRegistry
    {
        void Register(Type pageType, Type viewmodelType);
        Type GetViewmodelType(Type pageType);
        Type GetPageType(Type viewmodelType);
    }
}
