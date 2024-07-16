using System.Net;

namespace OnlineStore.Helpers.ErrorHandeling
{
    public interface IApplicationException
    {
        public string Type { get; }
        public string Title { get; }
        public string Message { get; }
        public HttpStatusCode StatusCode { get; }
        public object ExtendedInfo { get; }
    }
}
