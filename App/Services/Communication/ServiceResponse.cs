namespace rp.Accounting.App.Services.Communication
{
    public class ServiceResponse<T> where T : class
    {
        public T Entity { get; private set; }
        public bool Success { get; set; }
        public ServiceCode Code { get; set; }

        public ServiceResponse(bool success, ServiceCode code, T entity)
        {
            this.Entity = entity;
            Success = success;
            Code = code;
        }

        /// <summary>
        /// Creates a custom response without entity
        /// </summary>
        public ServiceResponse(bool success, ServiceCode code) : this(success, code, null)
        { }

        /// <summary>
        /// Creates a 200 response
        /// </summary>
        /// <param name="entity"></param>
        public ServiceResponse(T entity) : this(true, ServiceCode.Ok, entity)
        { }

        /// <summary>
        /// Creates a non-200 response
        /// </summary>
        /// <param name="message"></param>
        public ServiceResponse(ServiceCode code) : this(false, code, null)
        { }
    }
}
