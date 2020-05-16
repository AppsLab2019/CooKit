using System;

namespace CooKit.Models.Steps
{
    public sealed class TextStep : ITextStep
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
    }
}
