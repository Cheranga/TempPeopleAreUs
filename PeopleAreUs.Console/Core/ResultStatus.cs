namespace PeopleAreUs.Console.Core
{
    public class ResultStatus<T>
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public static ResultStatus<T> Success()
        {
            return new ResultStatus<T>
            {
                Status = true
            };
        }

        public static ResultStatus<T> Success(T data)
        {
            return new ResultStatus<T>
            {
                Status = true,
                Data = data
            };
        }

        public static ResultStatus<T> Failure(string message)
        {
            return new ResultStatus<T>
            {
                Status = false,
                Message = message
            };
        }
    }
}