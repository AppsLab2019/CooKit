using System;
using Xamarin.Forms;

namespace CooKit.Behaviors
{
    public class BehaviorBase<T> : Behavior<T> where T : BindableObject
    {
        protected T AttachedObject { get; private set; }

        protected override void OnAttachedTo(T bindable)
        {
            if (bindable is null)
                throw new ArgumentNullException(nameof(bindable));

            AttachedObject = bindable;
            RefreshBindingContext();
            bindable.BindingContextChanged += OnBindingContextChanged;

            base.OnAttachedTo(bindable);
        }

        private void OnBindingContextChanged(object sender, EventArgs e)
        {
            RefreshBindingContext();
        }

        private void RefreshBindingContext()
        {
            BindingContext = AttachedObject?.BindingContext;
        }

        protected override void OnDetachingFrom(T bindable)
        {
            if (bindable is null)
                throw new ArgumentNullException(nameof(bindable));

            base.OnDetachingFrom(bindable);

            AttachedObject = null;
            RefreshBindingContext();
            bindable.BindingContextChanged -= OnBindingContextChanged;
        }
    }
}
