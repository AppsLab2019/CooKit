using System.Collections.ObjectModel;

namespace CooKit.Models.Editor.CollectionElementPair
{
    public interface ICollectionElementPair<T>
    {
        ObservableCollection<T> Collection { get; }
        T Element { get; }
    }
}
