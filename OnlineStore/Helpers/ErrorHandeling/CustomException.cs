using System.Diagnostics.CodeAnalysis;
using System;
using System.Net;

namespace OnlineStore.Helpers.ErrorHandeling
{
    public class CustomException : Exception, IApplicationException
    {
        public string Type { get; }
        public string Title { get; } 
        public object ExtendedInfo { get; } // this will be used to add more info to the returned object
        public string Message { get; }

        public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

        

        public CustomException(
            [NotNull] string message,
            object extendedInfo = null,
            string type = null,
            string title = null)
            : base(message)
        {
            Message = message;
            ExtendedInfo = extendedInfo ?? ExtendedInfo;
            Type = type ?? Type;
            Title = title ?? Title;
        }


    }
}
