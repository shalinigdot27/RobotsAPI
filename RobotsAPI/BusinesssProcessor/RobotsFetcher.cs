using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace RobotsAPI
{
    public class RobotsFetcher
    {
        private const string url = "https://60c8ed887dafc90017ffbd56.mockapi.io/";
        public IList<Robot> GetRobots(int x, int y)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                var robots = client.GetAsync("robots").Result;

                if (robots.StatusCode == System.Net.HttpStatusCode.NotFound ||
                    robots.StatusCode == System.Net.HttpStatusCode.Forbidden ||
                    robots.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                {
                    return null;
                }

                var robotsListJson = robots.Content.ReadAsStringAsync().Result;

                var robotsList = JsonConvert.DeserializeObject<List<Robot>>(robotsListJson);

                return robotsList;
            }           
        }
    }
}
