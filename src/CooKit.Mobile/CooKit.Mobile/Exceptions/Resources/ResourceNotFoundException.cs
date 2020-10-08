using System;

namespace CooKit.Mobile.Exceptions.Resources
{
    public class ResourceNotFoundException : Exception
    {
        public string ResourceName { get; }

        public ResourceNotFoundException(string resourceName) 
            : base($"Resource {resourceName} was not found!")
        {
            ResourceName = resourceName;
        }
    }
}
