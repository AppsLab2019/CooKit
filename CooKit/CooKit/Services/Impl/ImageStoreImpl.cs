using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CooKit.Services.Impl
{
    // TODO: introduce caching (maybe Dictionary with weak values?)
    public sealed class ImageStoreImpl : IImageStore
    {
        private readonly Dictionary<string, IImageLoader> _loaders;

        public ImageStoreImpl() =>
            _loaders = new Dictionary<string, IImageLoader>();

        public void RegisterLoader(string loaderName, IImageLoader instance)
        {
            if (!_loaders.ContainsKey(loaderName))
                _loaders[loaderName] = instance;
        }

        public ImageSource LoadImage(string loaderName, string source)
        {
            if (!_loaders.TryGetValue(loaderName, out var loader))
                throw new ArgumentException(nameof(loaderName));

            return loader.LoadImage(source);
        }

        public async Task<ImageSource> LoadImageAsync(string loaderName, string source)
        {
            if (!_loaders.TryGetValue(loaderName, out var loader))
                throw new ArgumentException(nameof(loaderName));

            return await loader.LoadImageAsync(source);
        }
    }
}
