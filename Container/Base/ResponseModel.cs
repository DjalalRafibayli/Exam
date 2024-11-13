
namespace Container.Base
{
    public class ResponseModel<T>
    {
        public bool IsSuccess { get; set; }
        //[JsonProperty(NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public T Data { get; set; }
        //[JsonProperty(NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public List<string> Errors { get; set; }
        public int StatusCode { get; set; }

        public ResponseModel()
        {
            this.Errors = new List<string>();
        }
        public static ResponseModel<T> SuccessResponse(T data, int statusCode = 200)
        {
            return new ResponseModel<T> { IsSuccess = true, Data = data, StatusCode = statusCode };
        }
        public static ResponseModel<T> SuccessResponse(int statusCode = 200)
        {
            return new ResponseModel<T> { IsSuccess = true, Data = default(T), StatusCode = statusCode };
        }
        public static ResponseModel<T> ErrorResponse(int statusCode = 400)
        {
            return new ResponseModel<T> { IsSuccess = false, StatusCode = statusCode };
        }
        public static ResponseModel<T> ErrorResponse(List<string> errors, int statusCode)
        {
            return new ResponseModel<T> { IsSuccess = false, Errors = errors, StatusCode = statusCode };
        }
        public static ResponseModel<T> ErrorResponse(string errors, int statusCode)
        {
            return new ResponseModel<T> { IsSuccess = false, Errors = new List<string> { errors }, StatusCode = statusCode };
        }
    }
}
