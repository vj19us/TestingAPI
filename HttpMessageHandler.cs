using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text;

namespace TestApi
{
	public class HttpMessageHandler : DelegatingHandler
	{

		protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			Console.WriteLine($"Request Received: {request.ToJson()}");

			string requestType = string.Empty;

			if (request.Headers.TryGetValues("RequestType", out var requestTypeHeader))
			{
				requestType = requestTypeHeader.FirstOrDefault();
				request.Headers.Remove("RequestType");
			}
			else
				// enforce LogRequestType
				throw new Exception($"RequestType is required in header");

			var logreq = new HttpModel()
			{
				//Content = await request.Content.ReadAsStringAsync(),
				Request = request
			};

			var objreq = logreq.ToJson();

			Console.WriteLine($"Request Type: {requestType}");

			var response = await base.SendAsync(request, cancellationToken);

			var logresp = new HttpModel()
			{
				Content = await response.Content.ReadAsStringAsync(),
				Response = response
			};

			var objresp = logresp.ToJson();

			Console.WriteLine($"Response Received {response.ToJson()}");

			return response;
		}

		public class HttpModel
        {
			public HttpRequestMessage Request { get; set; }
			public HttpResponseMessage Response { get; set; }

			public string Content { get; set; }
		}
	}
}
