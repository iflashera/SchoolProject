using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Common.Helper
{
    public  class APIResponse<T>
    {
        public APIResponse()
        {

        }
        public APIResponse(T Data)
        {
            this.Data = Data;
        }
        public APIResponse(T Data, bool Success)
        {
            this.Data = Data;
            this.Success = Success;
        }
        public APIResponse(T Data, bool Success, HttpStatusCode Status)
        {
            this.Data = Data;
            this.Success = Success;
            this.Status = Status;
        }
        public APIResponse(T Data, bool Success, HttpStatusCode Status, List<string> Messages)
        {
            this.Data = Data;
            this.Success = Success;
            this.Status = Status;
            this.Messages = Messages;
        }
        public APIResponse(T Data, bool Success, HttpStatusCode Status, long TotalCount)
        {
            this.Data = Data;
            this.Success = Success;
            this.Status = Status;
            this.TotalCount = TotalCount;
        }
        public APIResponse(T Data, bool Success, HttpStatusCode Status, bool IsValidationError, List<string> Messages)
        {
            this.Data = Data;
            this.Success = Success;
            this.Status = Status;
            this.IsValidationError = IsValidationError;
            this.Messages = Messages;
        }
        public APIResponse(T Data, HttpStatusCode Status, List<string> Messages)
        {
            this.Data = Data;
            this.Success = false;
            this.Messages = Messages;
            this.Status = Status;
        }
        public HttpStatusCode Status { get; set; }
        public T Data { get; set; }
        public bool Success { get; set; } = true;
        public bool IsValidationError { get; set; }
        public List<string> Messages { get; set; }
        public long TotalCount { get; set; }


    }
}
