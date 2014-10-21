
namespace Sample.Web.Mvc.UI
{
    using System;
    using System.Web.Mvc;

    public class ToggleClickClientEvents : IClientEventList
    {
        public ToggleClickClientEvents()
        {
            Toggle = new ClientEvent();
            SwitchOn = new ClientEvent();
            SwitchOff = new ClientEvent();
            Load = new ClientEvent();
        }

        public ClientEvent Toggle { get; private set; }
        public ClientEvent SwitchOn { get; private set; }
        public ClientEvent SwitchOff { get; private set; }
        public ClientEvent Load { get; private set; }
    }

    public class ToggleClickClientEventsBuilder : ClientEventsBuilder
    {
        private readonly ToggleClickClientEvents clientEvents;

        public ToggleClickClientEventsBuilder(ToggleClickClientEvents clientEvents, ViewContext viewContext)
        {
            Guard.IsNotNull(clientEvents, "clientEvents");
            Guard.IsNotNull(viewContext, "viewContext");

            this.clientEvents = clientEvents;
            base.viewContext = viewContext;
        }

        public ToggleClickClientEventsBuilder Toggle(string onToggleHandlerName)
        {
            Guard.IsNotNullOrEmpty(onToggleHandlerName, "onToggleHandlerName");
            clientEvents.Toggle.HandlerName = onToggleHandlerName;

            return this;
        }

        public ToggleClickClientEventsBuilder OnSwitchOn(string onSwitchOnHandlerName)
        {
            Guard.IsNotNullOrEmpty(onSwitchOnHandlerName, "onSwitchOnHandlerName");
            clientEvents.SwitchOn.HandlerName = onSwitchOnHandlerName;

            return this;
        }

        public ToggleClickClientEventsBuilder OnSwitchOff(string onSwitchOffHandlerName)
        {
            Guard.IsNotNullOrEmpty(onSwitchOffHandlerName, "onSwitchOffHandlerName");
            clientEvents.SwitchOff.HandlerName = onSwitchOffHandlerName;

            return this;
        }

        public ToggleClickClientEventsBuilder OnLoad(string onLoadHandlerName)
        {
            Guard.IsNotNullOrEmpty(onLoadHandlerName, "onLoadHandlerName");
            clientEvents.Load.HandlerName = onLoadHandlerName;

            return this;
        }
    }
}