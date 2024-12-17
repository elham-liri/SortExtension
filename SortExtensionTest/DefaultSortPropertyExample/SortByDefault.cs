using SortExtensionTest.Helpers;
using SortHelper;
using SortHelper.Enums;

namespace SortExtensionTest.DefaultSortPropertyExample
{
    public class SortByDefault
    {
        private string DataFileName => "MOCK_DATA_Task.json";

        public List<TaskModel> SortTasksByDefault()
        {
            var tasks =MockDataProvider.GetCollection<TaskModel>(DataFileName);
            return tasks.OrderBy().ToList();
        }

        public List<TaskModel> SortTasksByDefault(SortDirection sortDirection)
        {
            var tasks = MockDataProvider.GetCollection<TaskModel>(DataFileName);
            return tasks.OrderBy(sortDirection).ToList();
        }
    }
}
