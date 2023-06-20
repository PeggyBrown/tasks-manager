using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json.Linq;

class ApiHelper
{
    public void AddTaskToTrello(string listId, string taskName)
    {
        // Read the configuration from the JSON file
        JObject config = JObject.Parse(File.ReadAllText("settings.json"));
        string apiKey = (string)config["apiKey"];
        string apiToken = (string)config["apiToken"];

        // API endpoint for creating a new card
        string createCardUrl = $"https://api.trello.com/1/cards?key={apiKey}&token={apiToken}";

        // Prepare the data to be sent
        NameValueCollection data = new NameValueCollection();
        data["name"] = taskName + new Random().Next(100);
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
                // Handle any errors that occur during the request
                Console.WriteLine("An error occurred:");
                Console.WriteLine(ex.Message);
            }
        }
    }
}