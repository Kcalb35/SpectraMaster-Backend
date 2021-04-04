namespace SpectraMaster.Utils
{
    public record ApiResponse
    {
        public string Status { init; get; }
        public object Data { init; get; }

        public static ApiResponse Success(object dataObj)
        {
            return new ApiResponse {Status = "success", Data = dataObj};
        }

        public static ApiResponse Error(object dataObj)
        {
            return new ApiResponse {Status = "error", Data = dataObj};
        }
    }
}