using System.Collections.Generic;
using CooKit.Mobile.Models.Root;

namespace CooKit.Mobile.Registries.RootEntry
{
    public interface IRootEntryRegistry
    {
        IList<IRootEntry> GetEntries();
        void Register(IRootEntry entry);
    }
}
