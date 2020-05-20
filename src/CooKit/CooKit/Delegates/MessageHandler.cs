using System.Threading.Tasks;
using CooKit.Models.Messages;

namespace CooKit.Delegates
{
    public delegate Task MessageHandler(IMessage message);
}
