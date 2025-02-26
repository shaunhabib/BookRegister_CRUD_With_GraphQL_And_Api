namespace BookRegisterApi.Wrapper
{
    public class Response<T>
    {
        public T? Data { get; set; }
        public string Message { get; set; }

        public static Response<T> Success(T data, string message)
        {
            var res = new Response<T>
            {
                Data = data,
                Message = message
            };
            return res;
        }

        public static Response<T> Fail(string message)
        {
            var res = new Response<T>
            {
                Message = message
            };
            return res;
        }
    }
}
