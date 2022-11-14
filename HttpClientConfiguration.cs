using System.Net.Http.Headers;
using System.Text;

namespace TestApi
{
	public class HttpClientConfiguration
	{
		public static void ConfigureService(WebApplicationBuilder builder)
		{
			builder.Services.AddTransient<HttpMessageHandler>();

			builder.Services.AddHttpClient("BoredApiClient", httpClient =>
			{
				httpClient.BaseAddress = new Uri("http://www.boredapi.com");
				httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			}).AddHttpMessageHandler<HttpMessageHandler>();
		}
	}
}
