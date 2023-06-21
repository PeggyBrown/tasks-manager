using System.Collections.Generic;
using System.Collections.Specialized;

namespace TaskManager
{
    public interface IApiHelper
    {
        void AddTask(string listId, string taskName, string desc);
        List<TrelloTask> ReadAllTasksFromBoard(string boardId);
        List<TrelloList> ReadAllListsFromBoard(string boardId);
        void RemoveTask(string taskId);
    }

    public class ApiHelper : IApiHelper
    {
        private readonly string _apiKey;
        private readonly string _apiToken;
        private readonly IRequestSender _requestSender;
        private readonly IFilesHelper _filesHelper;
        private string _trelloAddress = "https://api.trello.com/1";

        public ApiHelper(IRequestSender requestSender, IFilesHelper filesHelper)
        {
            _requestSender = requestSender;
            _filesHelper = filesHelper;
            _apiKey = _filesHelper.GetApiKey();
            _apiToken = _filesHelper.GetApiToken();
        }
        
        public void AddTask(string listId, string taskName, string desc)
        {
            // API endpoint for creating a new card
            string createCardUrl = $"{_trelloAddress}/cards?key={_apiKey}&token={_apiToken}";

            // Prepare the data to be sent
            NameValueCollection data = new NameValueCollection();
            data["name"] = taskName;
            data["desc"] = desc;
            data["idList"] = listId;

            // Send a POST request to create the card
            _requestSender.SendPostRequest(createCardUrl, data);
            
        }

        public List<TrelloTask> ReadAllTasksFromBoard(string boardId)
        {
            string getCardsUrl = $"{_trelloAddress}/boards/{boardId}/cards?key={_apiKey}&token={_apiToken}";
            return _requestSender.SendGetAllTasksRequest(getCardsUrl).Result;

        }
    
        public List<TrelloList> ReadAllListsFromBoard(string boardId)
        {
            string getListsUrl = $"{_trelloAddress}/boards/{boardId}/Lists?key={_apiKey}&token={_apiToken}";
            return _requestSender.SendGetAllListsRequest(getListsUrl).Result;
        }

        public void RemoveTask(string cardId)
        {
            // API endpoint for removing the card
            string removeCardUrl = $"{_trelloAddress}/cards/{cardId}?key={_apiKey}&token={_apiToken}";
            _requestSender.SendDeleteRequest(removeCardUrl);
        }
    }
}