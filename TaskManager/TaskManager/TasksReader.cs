using System;
using System.IO;
using Newtonsoft.Json;
using TaskManager;

class TasksReader
{
    private readonly ApiHelper _apiHelper;

    public TasksReader(ApiHelper apiHelper)
    {
        _apiHelper = apiHelper;
    }

    public void PrepareTasksFromFile()
    {
        string jsonFilePath = "input.json"; // Update with your JSON file path

        // Read JSON from file
        string json = File.ReadAllText(jsonFilePath);

        try
        {
            // Deserialize JSON into objects
            Board board = JsonConvert.DeserializeObject<Board>(json);

            // Access and display the parsed objects
            Console.WriteLine("Board Name: " + board.Name);
            Console.WriteLine();

            foreach (var list in board.Lists)
            {
                Console.WriteLine("List Name: " + list.Name);

                foreach (var task in list.Tasks)
                {
                    Console.WriteLine("Task Name: " + task.Name);
                    _apiHelper.AddTaskToTrello("64905ce37ca84c01968da438", task.Name);
                }

                Console.WriteLine();
            }
        }
        catch (JsonException ex)
        {
            Console.WriteLine("Error parsing JSON: " + ex.Message);
        }
    }
}