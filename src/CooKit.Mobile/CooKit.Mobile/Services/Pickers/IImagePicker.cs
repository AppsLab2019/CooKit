using System.Threading.Tasks;
using CooKit.Mobile.Models.Images;

namespace CooKit.Mobile.Services.Pickers
{
    public interface IImagePicker
    {
        Task<Image> PickImageAsync();
    }
}
