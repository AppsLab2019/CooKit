using System.Threading.Tasks;

namespace CooKit.Delegates
{
    public delegate Task MessageHandler(object sender, string title, object param);

    public delegate Task MessageHandlerSend<in TSender>(TSender sender, string title, object param);

    public delegate Task MessageHandlerParam<in TParam>(object sender, string title, TParam param);

    public delegate Task MessageHandlerSendParam<in TSender, in TParam>(TSender sender, string title, TParam param);
}
