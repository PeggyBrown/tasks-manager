using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TaskManager
{
    public class RequestSender
    {
        public void SendPostRequest(string createCardUrl, NameValueCollection data)
        {
            using (WebClient client = new WebClient())
            {
                try
                {
                    byte[] response = client.UploadValues(createCardUrl, "POST", data);
                    string responseString = Encoding.UTF8.GetString(response);
                    Console.WriteLine("Card created successfully:");
                    Console.WriteLine(responseString);
                }
                catch (WebException ex)
                {
                    Console.WriteLine("An error occurred:");
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public async Task<List<TrelloTask>> SendGetAllTasksRequest(string apiUrl)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        List<TrelloTask> tasks = JsonConvert.DeserializeObject<List<TrelloTask>>(responseBody);

                        return tasks;
                    }

                    Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }

            return new List<TrelloTask>();

        }

        public async Task<List<TrelloList>> SendGetAllListsRequest(string getListsUrl)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(getListsUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        List<TrelloList> tasks = JsonConvert.DeserializeObject<List<TrelloList>>(responseBody);

                        return tasks;
                    }

                    Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }

            return new List<TrelloList>();
        }

        public void SendDeleteRequest(string removeCardUrl)
        {
            using (WebClient client = new WebClient())
            {
                try
                {
                    byte[] response = client.UploadValues(removeCardUrl, "DELETE",new NameValueCollection());
                    string responseString = Encoding.UTF8.GetString(response);
                    Console.WriteLine("Card removed successfully:");
                    Console.WriteLine(responseString);
                }
                catch (WebException ex)
                {
                    Console.WriteLine("An error occurred:");
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}