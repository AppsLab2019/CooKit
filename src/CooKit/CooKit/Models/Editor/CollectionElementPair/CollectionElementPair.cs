using System;
using System.Collections.ObjectModel;

namespace CooKit.Models.Editor.CollectionElementPair
{
    public sealed class CollectionElementPair<T> : ICollectionElementPair<T>
    {
        public ObservableCollection<T> Collection { get; }
        public T Element { get; }

        public CollectionElementPair(ObservableCollection<T> collection, T element)
        {
            Collection = collection;
            Element = element;
        }

        public CollectionElementPair(ICollectionElementPair<T> pair)
        {
            if (pair is null)
                throw new ArgumentNullException(nameof(pair));

            Collection = pair.Collection;
            Element = pair.Element;
        }
    }
}
