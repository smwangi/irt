/**
This pattern explicitly wraps success and error responses in a Result object. 
A method returns either a success or error result, and the caller must handle both cases.
*/
using Irt.Application.Configuration.ApiResponse;

namespace Irt.Application.Configuration.Results
{
    public class Result<TSuccess, TError>
    {
        public TSuccess? Success { get; }
        public bool IsSuccess { get; }
        public TError? Error { get; }

        private Result(bool isSuccess, TSuccess? success = default, TError? failure = default)
        {
            IsSuccess = isSuccess;
            Success = success;
            Error = failure;
        }
        /*private Result(TSuccess success)
        {
            Success = success;
            IsSuccess = true;
        }

        private Result(TError error)
        {
            Error = error;
            IsSuccess = false;
        }*/

        public static Result<TSuccess, TError> Ok(TSuccess success) => new(true, success, default);

        public static Result<TSuccess, TError> Fail(TError error) => new(false, default, error);

        public ApiResponse<TSuccess> ToApiResponse()
        {
            return IsSuccess
                ? ApiResponse<TSuccess>.Success(Success, "success")
                : ApiResponse<TSuccess>.Fail(Error!.ToString());
        }
    }
}