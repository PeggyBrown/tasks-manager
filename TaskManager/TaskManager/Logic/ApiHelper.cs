using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;

namespace TaskManager
{
    class ApiHelper
    {
        private readonly string _apiKey;
        private readonly string _apiToken;
        private readonly RequestSender _requestSender;
        private readonly FilesHelper _filesHelper;
        private string _trelloAddress = "https://api.trello.com/1";

        public ApiHelper(RequestSender requestSender, FilesHelper filesHelper)
        {
            _requestSender = requestSender;
            _filesHelper = filesHelper;
            _apiKey = _filesHelper.GetApiKey();
            _apiToken = _filesHelper.GetApiToken();
        }
        
        public void AddTaskToTrello(string listId, string taskName, string desc)
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

        public async Task<List<TrelloTask>> ReadAllTasksFromBoard(string boardId)
        {
            string getCardsUrl = $"{_trelloAddress}/boards/{boardId}/cards?key={_apiKey}&token={_apiToken}";
            return await _requestSender.SendGetAllTasksRequest(getCardsUrl);

        }
    
        public async Task<List<TrelloList>> ReadAllListsFromBoard(string boardId)
        {
            string getListsUrl = $"{_trelloAddress}/boards/{boardId}/Lists?key={_apiKey}&token={_apiToken}";
            return await _requestSender.SendGetAllListsRequest(getListsUrl);
        }

        public void RemoveTaskFromTrello(string cardId)
        {
            // API endpoint for removing the card
            string removeCardUrl = $"{_trelloAddress}/cards/{cardId}?key={_apiKey}&token={_apiToken}";
            _requestSender.SendDeleteRequest(removeCardUrl);
        }
    }
}