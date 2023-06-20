using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace TaskManager
{
    public class TasksManager
    {
        private readonly ApiHelper _apiHelper;
        private readonly FilesHelper _filesHelper;
        private const string BoardId = "64905ce37ca84c01968da431";
        private const string DefaultListId = "64905ce37ca84c01968da438";
        private static readonly List<string> TasksToLeave = new List<string>(){
            "64905ce37ca84c01968da4c4", 
            "64905ce37ca84c01968da4c6", 
            "64905ce37ca84c01968da4c8",
            "64905ce37ca84c01968da4ca",
            "64905ce37ca84c01968da4ce",
            "64905ce37ca84c01968da4d0",
            "64905ce37ca84c01968da4cc",
            "649169edf1024f64920a2f36"
        };

        public TasksManager(ApiHelper apiHelper, FilesHelper filesHelper)
        {
            _apiHelper = apiHelper;
            _filesHelper = filesHelper;
        }

        public bool PrepareTasksFromFile()
        {
            string json = _filesHelper.GetTasksInput();

            try
            {
                TrelloBoard trelloBoard = JsonConvert.DeserializeObject<TrelloBoard>(json);

                var allTasksFromBoard = _apiHelper.ReadAllTasksFromBoard(BoardId).Result;
                var allListsFromBoard = _apiHelper.ReadAllListsFromBoard(BoardId).Result;
                var inputLists = trelloBoard?.Lists;
                
                AssignListIdsToListsAndTasks(inputLists, allListsFromBoard);
                
                var tasksFromFile = inputLists.SelectMany(l => l.Tasks).ToList();
                CreateMissingTasks(tasksFromFile, allTasksFromBoard);
                HandleObsoleteTasks(allTasksFromBoard, tasksFromFile, true);
            }
            catch (JsonException ex)
            {
                Console.WriteLine("Error parsing JSON: " + ex.Message);
                return false;
            }

            return true;
        }

        private static void AssignListIdsToListsAndTasks(List<TrelloList> inputLists, List<TrelloList> allListsFromBoard)
        {
            inputLists.ForEach(t => t.Id = allListsFromBoard.FirstOrDefault(l => l.Name.Equals(t.Name))?.Id);
            inputLists.ForEach(l =>
                l.Tasks.ForEach(t => t.ListId = l.Id));
        }

        private void HandleObsoleteTasks(List<TrelloTask> allTasksFromBoard, 
            IList<TrelloTask> tasksFromFile,
            bool shouldDeleteObsoleteTasks)
        {
            foreach (var task in allTasksFromBoard.Where(t => !TasksToLeave.Contains(t.Id)))
            {
                if (!tasksFromFile.Select(t => t.Name).Contains(task.Name))
                {
                    Console.WriteLine("Obsolete task: " + task.Name);
                    if (shouldDeleteObsoleteTasks)
                    {
                        _apiHelper.RemoveTaskFromTrello(task.Id);
                    }
                }
            }
        }

        private void CreateMissingTasks(List<TrelloTask> tasksFromFile, List<TrelloTask> allTasksFromBoard)
        {
            foreach (var task in tasksFromFile)
            {
                if (!allTasksFromBoard.Select(t => t.Name).Contains(task.Name))
                {
                    _apiHelper.AddTaskToTrello(task.ListId ?? DefaultListId, task.Name, task.Desc);
                }
            }
        }
    }
}