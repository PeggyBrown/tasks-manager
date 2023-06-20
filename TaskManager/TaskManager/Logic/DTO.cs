using System.Collections.Generic;

namespace TaskManager
{
    class TrelloBoard
    {
        public string Name { get; set; }
        public List<TrelloList> Lists { get; set; }
    }

    public class TrelloList
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<TrelloTask> Tasks { get; set; }
    }

    public class TrelloTask
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ListId { get; set; }
        public string Desc { get; set; }
    }
}