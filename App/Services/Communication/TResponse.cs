namespace rp.Accounting.App.Services.Communication
{
    public class TResponse<T> where T : class
    {
        public T Entity { get; private set; }
        public bool Success { get; set; }
        public ServiceCode Code { get; set; }

        public TResponse(bool success, ServiceCode code, T entity)
        {
            this.Entity = entity;
            Success = success;
            Code = code;
        }

        /// <summary>
        /// Creates a custom response without entity
        /// </summary>
        public TResponse(bool success, ServiceCode code) : this(success, code, null)
        { }

        /// <summary>
        /// Creates a 200 response
        /// </summary>
        /// <param name="entity"></param>
        public TResponse(T entity) : this(true, ServiceCode.Ok, entity)
        { }

        /// <summary>
        /// Creates a non-200 response
        /// </summary>
        /// <param name="message"></param>
        public TResponse(ServiceCode code) : this(false, code, null)
        { }
    }
}
