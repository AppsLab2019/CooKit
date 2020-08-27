using System;
using System.Threading.Tasks;
using CooKit.Mobile.Models.Images;
using Plugin.Media;

namespace CooKit.Mobile.Services.Pickers
{
    public class MediaPluginImagePicker : IImagePicker
    {
        public async Task<Image> PickImageAsync()
        {
            if (!CrossMedia.IsSupported)
                throw new NotSupportedException("CrossMedia is not supported!");

            var media = CrossMedia.Current;

            if (!media.IsPickPhotoSupported)
                throw new NotSupportedException("Photo picking is not supported!");

            using var mediaFile = await media.PickPhotoAsync();

            return mediaFile != null 
                ? new Image(ImageType.File, mediaFile.Path) 
                : null;
        }
    }
}
