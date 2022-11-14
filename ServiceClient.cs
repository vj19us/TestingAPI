using TestApi.Models;

namespace TestApi
{
    public class ServiceClient: IServiceClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public ServiceClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<ActivityDetails> CallBoredApi()
        {
            var client = _httpClientFactory.CreateClient("BoredApiClient");
            var activity = new Activity();
            var activityDetails = new ActivityDetails();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "/api/activity/");
            
            request.Headers.Add("RequestType", "Request1");
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                activity = await response.Content.ReadFromJsonAsync<Activity>();

                HttpRequestMessage request2 = new HttpRequestMessage(HttpMethod.Get, $"/api/activity?key={activity.key}");
                request2.Headers.Add("RequestType", "Request2");
                using (var response2 = await client.SendAsync(request2))
                {
                    response2.EnsureSuccessStatusCode();
                    activityDetails = await response2.Content.ReadFromJsonAsync<ActivityDetails>();
                }
            }

            return activityDetails;
        }
    }

    public interface IServiceClient
    {
        public Task<ActivityDetails> CallBoredApi();
    }
}
