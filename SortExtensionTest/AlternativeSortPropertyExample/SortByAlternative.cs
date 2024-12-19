using SortExtensionTest.Helpers;
using SortHelper;

namespace SortExtensionTest.AlternativeSortPropertyExample
{
    internal class SortByAlternative
    {
        private string DataFileName => "MOCK_DATA_Task1.json";

        public List<TaskModel2> SortByAlternativeSortProperty(string sortProperty, bool descendingSort)
        {
            switch (sortProperty)
            {
                case "createDateString":
                    return SortTasksByCreateDateString(descendingSort);
                case "dueDateString":
                    return SortTasksByDueDateString(descendingSort);
            }

            return new List<TaskModel2>();
        }

        private List<TaskModel2> SortTasksByCreateDateString( bool descendingSort)
        {
            var tasks = MockDataProvider.GetCollection<TaskModel2>(DataFileName);
            return tasks.OrderBy("createDateString",  descendingSort).ToList();
        }

        private List<TaskModel2> SortTasksByDueDateString(bool descendingSort)
        {
            var tasks = MockDataProvider.GetCollection<TaskModel2>(DataFileName);
            return tasks.OrderBy("dueDateString", descendingSort).ToList();
        }

    }
}
