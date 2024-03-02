using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Metadata;
using AutoWrapper.Wrappers;
using System.Text;
using System.Threading.Tasks;

namespace Common.Helper
{
    public static class ResponseHelpers
    {
        public static ApiResponse CreateSuccessResponse(object data, string message)
        {
            return new ApiResponse { Message = message, Result = data, StatusCode = (int)HttpStatusCode.OK };
        }
        public static ApiResponse CreateGetSuccessResponse(object data)
        {
            return new ApiResponse { Result = data, StatusCode = (int)HttpStatusCode.OK };
        }

        public static ApiResponse CreateAddSuccessResponse()
        {
            return new ApiResponse { Message = Constant.AddSuccess, StatusCode = (int)HttpStatusCode.OK };
        }

        public static ApiResponse CreateUpdateSuccessResponse()
        {
            return new ApiResponse { Message = Constant.UpdateSuccess, StatusCode = (int)HttpStatusCode.OK };
        }
        public static ApiResponse CreateUpdateSuccessResponse(string message)
        {
            return new ApiResponse { Message = message, StatusCode = (int)HttpStatusCode.OK };
        }

        public static ApiResponse CreateRemoveSuccessResponse()
        {
            return new ApiResponse { Message = "", StatusCode = (int)HttpStatusCode.OK };
        }

        public static ApiResponse CreateErrorResponse(string message)
        {
            return new ApiResponse((int)HttpStatusCode.BadRequest, new List<string>() { message });
        }

        public static ApiResponse CreateNotFoundErrorResponse(string message)
        {
            return new ApiResponse((int)HttpStatusCode.NotFound, new List<string>() { message });
        }
        public static ApiResponse CreateExceptionErrorResponse()
        {
            return new ApiResponse((int)HttpStatusCode.InternalServerError, new List<string>() { Constant.ExceptionMessage });
        }

        public static ApiResponse CreateValidationErrorResponse(object validationErrors)
        {
            return new ApiResponse((int)HttpStatusCode.BadRequest, validationErrors);
        }

        public static ApiResponse CreateUnauthorizedErrorResponse(string message)
        {
            return new ApiResponse((int)HttpStatusCode.Unauthorized, message);
        }

    }
}
