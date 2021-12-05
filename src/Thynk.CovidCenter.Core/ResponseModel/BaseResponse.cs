namespace Thynk.CovidCenter.Core.ResponseModel
{
    public class BaseResponse
    {
        public bool Status { get; set; }

        public string Message { get; set; }
    }

    public class GenericResponse<T> : BaseResponse
    {
        public T Data { get; set; }
    }
}
