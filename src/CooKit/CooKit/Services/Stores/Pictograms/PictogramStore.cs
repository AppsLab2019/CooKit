﻿using CooKit.Models.Pictograms;
using CooKit.Services.Repositories.Pictograms;

namespace CooKit.Services.Stores.Pictograms
{
    public sealed class PictogramStore : RepositoryStore<IPictogram>, IPictogramStore
    {
        public PictogramStore(IPictogramRepository repository) : base(repository)
        {
        }
    }
}
