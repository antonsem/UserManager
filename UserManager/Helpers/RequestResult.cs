using Newtonsoft.Json;
using System.Linq;
using System.Net.Http;

namespace UserManager.Helpers
{
    internal class RequestResult<T>
    {
        public RequestResult() { }
        public RequestResult(HttpResponseMessage message)
        {
            ResponsePhrase = message.ReasonPhrase;
            Success = message.IsSuccessStatusCode;

            if (Success && message.Content.Headers.TryGetValues("content-type", out var values)
                && values.ToArray()[0].Contains("json"))
            {
                var json = message.Content.ReadAsStringAsync().Result;
                Request = JsonConvert.DeserializeObject<T>(json);
            }
        }

        public T? Request { get; set; }
        public string? ResponsePhrase { get; set; }
        public bool Success { get; set; }
    }

    internal class GetRequestResult<T> : RequestResult<T>
    {
        public GetRequestResult() { }
        public GetRequestResult(HttpResponseMessage message) : base(message)
        {
            int.TryParse(message.Headers.GetValues("x-pagination-pages")?.ToArray()[0], out var count);
            TotalPageCount = count;
        }

        public int TotalPageCount { get; set; }
    }
}
