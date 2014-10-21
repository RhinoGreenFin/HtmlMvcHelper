
namespace Sample.Web.Mvc.UI
{
    using System.Web.Mvc;

    public class AjaxClientEvents : IClientEventList
    {
        public AjaxClientEvents()
        {
            BeforeSend = new ClientEvent();
            Error = new ClientEvent();
            Success = new ClientEvent();
            Complete = new ClientEvent();
        }

        /// <summary>
        /// This event, which is triggered before an Ajax request is started.
        /// </summary>
        public ClientEvent BeforeSend { get; private set; }
        /// <summary>
        /// This event is only called if an error occurred with the request (you can never have both an error and a success callback with a request).
        /// </summary>
        public ClientEvent Error { get; private set; }
        /// <summary>
        /// This event is also only called if the request was successful.
        /// </summary>
        public ClientEvent Success { get; private set; }
        /// <summary>
        /// This event is called regardless of if the request was successful, or not. You will always receive a complete callback, 
        /// even for synchronous requests.
        /// </summary>
        public ClientEvent Complete { get; private set; }
    }

    public class AjaxClientEventsBuilder : ClientEventsBuilder
    {
        private readonly AjaxClientEvents clientEvents;

        public AjaxClientEventsBuilder(AjaxClientEvents clientEvents, ViewContext viewContext)
        {
            Guard.IsNotNull(clientEvents, "clientEvents");
            Guard.IsNotNull(viewContext, "viewContext");

            this.clientEvents = clientEvents;
            base.viewContext = viewContext;
        }

        /// <summary>
        /// This event, which is triggered before an Ajax request is started.
        /// </summary>
        public AjaxClientEventsBuilder BeforeSend(string jsHandlerName)
        {
            Guard.IsNotNullOrEmpty(jsHandlerName, "jsHandlerName");
            clientEvents.BeforeSend.HandlerName = jsHandlerName;

            return this;
        }

        /// <summary>
        /// This event is only called if an error occurred with the request (you can never have both an error and a success callback with a request).
        /// </summary>
        public AjaxClientEventsBuilder Error(string jsHandlerName)
        {
            Guard.IsNotNullOrEmpty(jsHandlerName, "jsHandlerName");
            clientEvents.Error.HandlerName = jsHandlerName;

            return this;
        }

        /// <summary>
        /// This event is also only called if the request was successful.
        /// </summary>
        public AjaxClientEventsBuilder Success(string jsHandlerName)
        {
            Guard.IsNotNullOrEmpty(jsHandlerName, "jsHandlerName");
            clientEvents.Success.HandlerName = jsHandlerName;

            return this;
        }

        /// <summary>
        /// This event is called regardless of if the request was successful, or not. You will always receive a complete callback, 
        /// even for synchronous requests.
        /// </summary>
        public AjaxClientEventsBuilder Complete(string jsHandlerName)
        {
            Guard.IsNotNullOrEmpty(jsHandlerName, "jsHandlerName");
            clientEvents.Complete.HandlerName = jsHandlerName;

            return this;
        }
    }
}