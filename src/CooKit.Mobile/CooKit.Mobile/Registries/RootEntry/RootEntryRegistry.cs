using System;
using System.Collections.Generic;
using CooKit.Mobile.Models.Root;

namespace CooKit.Mobile.Registries.RootEntry
{
    public class RootEntryRegistry : IRootEntryRegistry
    {
        private readonly IList<IRootEntry> _entries;

        public RootEntryRegistry()
        {
            _entries = new List<IRootEntry>();
        }

        public IList<IRootEntry> GetEntries()
        {
            return _entries;
        }

        public void Register(IRootEntry entry)
        {
            if (entry == null)
                throw new ArgumentNullException(nameof(entry));

            _entries.Add(entry);
        }
    }
}
