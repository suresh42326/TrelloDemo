using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Configuration;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace TrelloDemo
{
    class Program
    {
        public static void Main(string[] args)
        {
            string trelloKey =  ConfigurationManager.AppSettings["TrelloKey"];
            string trelloToken = ConfigurationManager.AppSettings["TrelloToken"];
            string baseAddress = ConfigurationManager.AppSettings["api"];
            string api = $"{baseAddress}?key={trelloKey}&token={trelloToken}";

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //GET Method
                HttpResponseMessage response =  client.GetAsync(api).Result;
                if (response.IsSuccessStatusCode)
                {
                    string responseData =  response.Content.ReadAsStringAsync().Result;
                    dynamic result = JsonConvert.DeserializeObject<dynamic>(responseData);
                    Console.WriteLine("List of Boards in Organization");
                    foreach (var board in result)
                    {
                        Console.WriteLine($"Board Id: {board.id}, Name: {board.name}");
                    }
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("Internal server Error");
                }
            }

        }
    }
}
