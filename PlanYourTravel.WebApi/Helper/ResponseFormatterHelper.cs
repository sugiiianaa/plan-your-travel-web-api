using PlanYourTravel.WebApi.Models.Abstract;

namespace PlanYourTravel.WebApi.Helper
{
    public static class ResponseFormatterHelper<T>
    {
        public static ApiResponse<T> FormatSuccessResponse(T? data)
        {
            return new ApiResponse<T>
            {
                IsSuccess = true,
                Data = data
            };
        }

        public static ApiResponse<T> FormatFailedResponse(string errorCode)
        {
            return new ApiResponse<T>
            {
                IsSuccess = false,
                ErrorCode = errorCode,
                Data = default,
            };
        }
    }
}
