using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Common.Helper
{
    public static class ResponseHelper<T>
    {
        public static APIResponse<T> CreateValidationErrorResponse(HttpStatusCode Status, List<string> Messages)
        {
            return new APIResponse<T>(default(T), false, Status, true, Messages);
        }
        public static APIResponse<T> CreateUpdateSuccessResponse(HttpStatusCode Status, List<string> Messages)
        {
            return new APIResponse<T>(default(T), false, Status, true, Messages);

        }
        public static APIResponse<T> CreateExceptionErrorResponse(HttpStatusCode Status, List<string> Messages)
        {
            return new APIResponse<T>(default(T), false, Status, true, Messages);

        }
        public static APIResponse<T> CreateNotFoundErrorResponse(HttpStatusCode Status, List<string> Messages)
        {
            return new APIResponse<T>
            {
                Data = default(T),
                Status = Status,
                Messages = Messages,
                IsValidationError = true
            };
        }

        public static APIResponse<T> CreateSuccessRes(T Data, List<string> Messages)
        {
            return new APIResponse<T>
            {
                Data = Data,
                Status = HttpStatusCode.OK,
                Messages = Messages
            };
        }

        public static APIResponse<T> CreateGetSuccessResponse(T Data, long TotalCount)
        {
            return new APIResponse<T>
            {
                Data = Data,
                Success = true,
                Status = HttpStatusCode.OK,
                TotalCount = TotalCount
            };

        }
        public static APIResponse<T> CreateSuccessRes(T Data, HttpStatusCode Status, List<string> Messages)
        {
            return new APIResponse<T>
            {
                Data = Data,
                Status = Status,
                Messages = Messages
            };
        }

        public static APIResponse<T> CreateValidationErrorRes(HttpStatusCode Status, List<string> Messages)
        {
            return new APIResponse<T>
            {
                Data = default(T),
                Success = false,
                Status = Status,
                IsValidationError = true,
                Messages = Messages

            };
        }

        public static APIResponse<T> CreateErrorRes(T Data)
        {
            return new APIResponse<T>
            {
                Data = Data,
                Success = false,
                Status = HttpStatusCode.BadRequest,
                IsValidationError = true,
                Messages = new List<string>()

            };
        }
        public static APIResponse<T> CreateErrorRes(T Data, List<string> Messages)
        {
            return new APIResponse<T>
            {
                Data = Data,
                Success = false,
                Status = HttpStatusCode.BadRequest,
                IsValidationError = true,
                Messages = Messages

            };
        }
        public static APIResponse<T> CreateErrorRes(T Data, HttpStatusCode Status, List<string> Messages)
        {
            return new APIResponse<T>
            {
                Data = Data,
                Success = false,
                Status = Status,
                IsValidationError = true,
                Messages = Messages,
            };
        }
        public static APIResponse<T> CreateErrorRes(HttpStatusCode Status, List<string> Messages)
        {
            return new APIResponse<T>
            {
                Data = default(T),
                Success = false,
                Status = Status,
                IsValidationError = true,
                Messages = Messages

            };
        }

        public static APIResponse<T> CreateUnauthorizedErrorResponse(HttpStatusCode Status, List<string> message)
        {
            return new APIResponse<T>
            {
                Data = default(T),
                Success = false,
                Status = Status,
                IsValidationError = true,
                Messages = message
            };
        }

        public static APIResponse<T> CreateSuccessResponses(T data, List<string> message)
        {
            return new APIResponse<T>
            {
                Data = data,
                Success = true,
                Status = HttpStatusCode.OK,
                IsValidationError = false,
                Messages = message

            };
            //return new ApiResponse { Message = message, Result = data, StatusCode = (int)HttpStatusCode.OK };
        }
    }
}
