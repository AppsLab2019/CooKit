using System;
using System.Reflection;
using System.Windows.Input;
using Xamarin.Forms;

namespace CooKit.Behaviors
{
    public sealed class EventToCommandBehavior : BehaviorBase<View>
    {
        public static BindableProperty EventNameProperty =
            BindableProperty.Create(nameof(EventName), typeof(string), typeof(EventToCommandBehavior), propertyChanged: OnEventNameChanged);

        public static BindableProperty CommandProperty =
            BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(EventToCommandBehavior));

        public static BindableProperty CommandParameterProperty =
            BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(EventToCommandBehavior));

        public static BindableProperty ConverterProperty =
            BindableProperty.Create(nameof(Converter), typeof(IValueConverter), typeof(EventToCommandBehavior));

        private Delegate _attachedDelegate;
        private string _attachedEventName;

        private static void OnEventNameChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!(bindable is EventToCommandBehavior behavior))
                throw new NotSupportedException();

            if (behavior.IsAttached())
                throw new NotImplementedException();

            //if (behavior.AttachedObject is null)
            //    return;

            //behavior.DetachFromCurrentlyAttachedEvent();
            //behavior.AttachToEvent();
        }

        protected override void OnAttachedTo(View bindable)
        {
            ThrowIfEventNameNull();
            base.OnAttachedTo(bindable);

            AttachToEvent();
        }

        private void AttachToEvent()
        {
            ThrowIfAlreadyAttached();

            var eventInfo = GetEvent(AttachedObject, EventName);
            var eventMethod = GetEventMethod();

            var eventMethodDelegate = eventMethod.CreateDelegate(eventInfo.EventHandlerType, this);
            eventInfo.AddEventHandler(AttachedObject, eventMethodDelegate);

            _attachedDelegate = eventMethodDelegate;
            _attachedEventName = EventName;
        }

        private void DetachFromCurrentlyAttachedEvent()
        {
            if (IsAttached())
                return;

            var eventInfo = GetEvent(AttachedObject, _attachedEventName);
            eventInfo.RemoveEventHandler(AttachedObject, _attachedDelegate);

            _attachedDelegate = null;
            _attachedEventName = null;
        }

        private bool IsAttached()
        {
            var detached = _attachedDelegate is null && _attachedEventName is null;
            return !detached;
        }

        private static EventInfo GetEvent(View bindable, string eventName)
        {
            var type = bindable.GetType();
            var info = type.GetEvent(eventName);

            if (info is null)
                throw new Exception($"Event {eventName} was not found in the type {type.Name}!");

            return info;
        }

        private MethodInfo GetEventMethod()
        {
            const BindingFlags methodFlags = BindingFlags.Instance | BindingFlags.NonPublic;
            var method = typeof(EventToCommandBehavior).GetMethod(nameof(OnEventInvoked), methodFlags);

            if (method is null)
                throw new Exception($"{nameof(GetEventMethod)} failed!");

            return method;
        }

        private void OnEventInvoked(object sender, object args)
        {
            ThrowIfCommandNull();

            var parameter = CommandParameter 
                            ?? Converter?.Convert(args, typeof(object), null, default);

            if (Command.CanExecute(parameter))
                Command.Execute(parameter);
        }

        protected override void OnDetachingFrom(View bindable)
        {
            DetachFromCurrentlyAttachedEvent();
            base.OnDetachingFrom(bindable);
        }

        private void ThrowIfAlreadyAttached()
        {
            if (!IsAttached())
                return;

            throw new Exception("This behavior is already attached to an event!");
        }

        private void ThrowIfEventNameNull()
        {
            if (EventName is null)
                ThrowFormattedException(nameof(EventName));
        }

        private void ThrowIfCommandNull()
        {
            if (Command is null)
                ThrowFormattedException(nameof(Command));
        }

        private static void ThrowFormattedException(string propertyName)
        {
            throw new Exception($"Property {propertyName} cannot be null!");
        }

        public string EventName
        {
            get => (string) GetValue(EventNameProperty);
            set => SetValue(EventNameProperty, value);
        }

        public ICommand Command
        {
            get => (ICommand) GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        public IValueConverter Converter
        {
            get => (IValueConverter) GetValue(ConverterProperty);
            set => SetValue(ConverterProperty, value);
        }
    }
}
