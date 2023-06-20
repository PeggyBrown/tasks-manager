using System.Collections.Generic;

namespace TaskManager
{
    class Board
    {
        public string Name { get; set; }
        public List<ListObject> Lists { get; set; }
    }

    class ListObject
    {
        public string Name { get; set; }
        public List<TaskObject> Tasks { get; set; }
    }

    class TaskObject
    {
        public string Name { get; set; }
    }
}