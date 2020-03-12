using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CooKit.Services.Impl
{
    // TODO: introduce caching (maybe Dictionary with weak values?)
    public sealed class ImageStoreImpl : IImageStore
    {
        public IReadOnlyList<IImageLoader> RegisteredLoaders => _loaders;

        private readonly List<IImageLoader> _loaders;
        private readonly Dictionary<string, IImageLoader> _loaderDictionary;

        public ImageStoreImpl()
        {
            _loaders = new List<IImageLoader>();
            _loaderDictionary = new Dictionary<string, IImageLoader>();
        }

        public void RegisterLoader(IImageLoader instance)
        {
            if (_loaderDictionary.ContainsKey(instance.Name))
                return;

            _loaders.Add(instance);
            _loaderDictionary[instance.Name] = instance;
        }

        public ImageSource LoadImage(string loaderName, string source)
        {
            if (!_loaderDictionary.TryGetValue(loaderName, out var loader))
                throw new ArgumentException(nameof(loaderName));

            return loader.LoadImage(source);
        }

        public async Task<ImageSource> LoadImageAsync(string loaderName, string source)
        {
            if (!_loaderDictionary.TryGetValue(loaderName, out var loader))
                throw new ArgumentException(nameof(loaderName));

            return await loader.LoadImageAsync(source);
        }
    }
}
