using SortExtensionTest.Helpers;
using SortHelper;

namespace SortExtensionTest.DefaultSortPropertyExample
{
    public class SortByDefault
    {
        private string DataFileName => "MOCK_DATA_Task1.json";

        public List<TaskModel> SortTasksByDefault()
        {
            var tasks =MockDataProvider.GetCollection<TaskModel>(DataFileName);
            return tasks.OrderBy().ToList();
        }

        public List<TaskModel> SortTasksByDefault(bool descendingSort)
        {
            var tasks = MockDataProvider.GetCollection<TaskModel>(DataFileName);
            return tasks.OrderBy(descendingSort).ToList();
        }
    }
}
