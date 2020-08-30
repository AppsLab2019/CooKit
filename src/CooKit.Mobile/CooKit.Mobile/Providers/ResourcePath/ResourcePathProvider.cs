using System;
using System.IO;

namespace CooKit.Mobile.Providers.ResourcePath
{
    public class ResourcePathProvider : IResourcePathProvider
    {
        public string GetResourceFolderPath()
        {
            var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            return Path.Combine(basePath, "resources");
        }
    }
}
