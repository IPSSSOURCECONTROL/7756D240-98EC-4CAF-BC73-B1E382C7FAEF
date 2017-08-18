namespace KhanyisaIntel.Kbit.Framework.Infrustructure.Application
{
    public abstract class ServiceResponseBase
    {
        private string _message;

        public ServiceResponseBase()
        {
            this.Message = string.Empty;
            this.ServiceResult = ServiceResult.Default;
        }

        protected ServiceResponseBase(string message, ServiceResult serviceResult)
        {
            this.Message = message;
            this.ServiceResult = serviceResult;
        }

        public string Message
        {
            get
            {
                if (this.ServiceResult == ServiceResult.Default)
                    return "Invalid response";

                return this._message;
            }
            set { this._message = value; }
        }
        public ServiceResult ServiceResult { get; set; }

        /// <summary>
        /// Registers a <see cref="ServiceResult.Error"/> with a supporting optional message.
        /// </summary>
        /// <param name="message">The optional message to be set for the response.</param>
        public void RegisterError(string message)
        {
            this.Message = message;
            this.ServiceResult = ServiceResult.Error;
        }

        /// <summary>
        /// Registers a <see cref="ServiceResult.Success"/> with a supporting optional message.
        /// </summary>
        /// <param name="message">The optional message to be set for the response.</param>
        public void RegisterSuccess(string message = null)
        {
            this.Message = message;
            this.ServiceResult = ServiceResult.Success;
        }

    }
}