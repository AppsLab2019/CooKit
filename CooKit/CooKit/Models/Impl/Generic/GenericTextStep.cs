using System;
using CooKit.Models.Steps;

namespace CooKit.Models.Impl.Generic
{
    internal sealed class GenericTextStep : ITextStep
    {
        public Guid Id { get; set; }
        public StepType Type { get; set; }
        public string Text { get; set; }
    }
}
