namespace LmsApi.Helpers
{
    public class ServiceResult<T>
    {
        public T? Data { get; set; }
        public string? ErrorMessage { get; set; }
        public bool Success => ErrorMessage == null;

        public static ServiceResult<T> Ok(T data)
        {
            //  Data = data; not valid because of avoiding shared state confusion
            return new ServiceResult<T> { Data = data };
        }
        public static ServiceResult<T> Fail(string? errorMessage)
        {
            return new ServiceResult<T> { ErrorMessage = errorMessage };
        }
    }
}
