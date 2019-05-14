namespace PeopleAreUs.Core
{
    public class OperationResult<T>
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public static OperationResult<T> Success()
        {
            return new OperationResult<T>
            {
                Status = true
            };
        }

        public static OperationResult<T> Success(T data)
        {
            return new OperationResult<T>
            {
                Status = true,
                Data = data
            };
        }

        public static OperationResult<T> Failure(string message)
        {
            return new OperationResult<T>
            {
                Status = false,
                Message = message
            };
        }
    }
}