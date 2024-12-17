using SortExtensionTest.Helpers;
using SortHelper;
using SortHelper.Enums;

namespace SortExtensionTest.AlternativeSortPropertyExample
{
    internal class SortByAlternative
    {
        private string DataFileName => "MOCK_DATA_Task.json";

        public List<TaskModel2> SortTasksByCreateDateString(SortDirection sortDirection)
        {
            var tasks = MockDataProvider.GetCollection<TaskModel2>(DataFileName);
            return tasks.OrderBy("createDateString", sortDirection).ToList();
        }

        public List<TaskModel2> SortTasksByDueDateString(SortDirection sortDirection)
        {
            var tasks = MockDataProvider.GetCollection<TaskModel2>(DataFileName);
            return tasks.OrderBy("dueDateString", sortDirection).ToList();
        }

    }
}
