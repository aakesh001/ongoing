using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RMSDemo.Models
{
    public class Error
    {
        public string Message { get; set; }
        public string ExceptionMessage { get; set; }
        public string ExceptionType { get; set; }
        public string StackTrace { get; set; }

        public Error(string Message, string ExceptionMessage, string ExceptionType, string StackTrace)
        {
            this.Message = Message;
            this.ExceptionMessage = string.IsNullOrEmpty(ExceptionMessage) ? string.Empty : ExceptionMessage;
            this.ExceptionType = string.IsNullOrEmpty(ExceptionType) ? string.Empty : ExceptionType;
            this.StackTrace = string.IsNullOrEmpty(StackTrace) ? string.Empty : StackTrace;
        }
    }
}