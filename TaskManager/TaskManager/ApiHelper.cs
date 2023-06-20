using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TaskManager
{
    class ApiHelper
    {
        private readonly string _apiKey;
        private readonly string _apiToken;

        public ApiHelper()
        {
            // Read the configuration from the JSON file
            JObject config = JObject.Parse(File.ReadAllText("settings.json"));
            _apiKey = (string)config["apiKey"];
            _apiToken = (string)config["apiToken"];
        }
        public void AddTaskToTrello(string listId, string taskName, string desc)
        {
            // API endpoint for creating a new card
            string createCardUrl = $"https://api.trello.com/1/cards?key={_apiKey}&token={_apiToken}";

            // Prepare the data to be sent
            NameValueCollection data = new NameValueCollection();
            data["name"] = taskName;
            data["desc"] = desc;
            data["idList"] = listId;

            // Send a POST request to create the card
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

        public async Task<List<TrelloTask>> ReadAllTasksFromBoard(string boardId)
        {
            string apiUrl = $"https://api.trello.com/1/boards/{boardId}/cards?key={_apiKey}&token={_apiToken}";

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
    
        public async Task<List<TrelloList>> ReadAllListsFromBoard(string boardId)
        {
            string apiUrl = $"https://api.trello.com/1/boards/{boardId}/Lists?key={_apiKey}&token={_apiToken}";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

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

        public void RemoveTaskFromTrello(string cardId)
        {
            // API endpoint for removing the card
            string removeCardUrl = $"https://api.trello.com/1/cards/{cardId}?key={_apiKey}&token={_apiToken}";
            using (WebClient client = new WebClient())
            {
                try
                {
                    byte[] response = client.UploadValues(removeCardUrl, "DELETE",new NameValueCollection());
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
    }
}