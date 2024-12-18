using SortExtensionTest.Helpers;
using SortHelper;
using SortHelper.Enums;

namespace SortExtensionTest.AlternativeSortPropertyExample
{
    internal class SortByAlternative
    {
        private string DataFileName => "MOCK_DATA_Task1.json";

        public List<TaskModel2> SortByAlternativeSortProperty(string sortProperty, SortDirection sortDirection)
        {
            switch (sortProperty)
            {
                case "createDateString":
                    return SortTasksByCreateDateString(sortDirection);
                case "dueDateString":
                    return SortTasksByDueDateString(sortDirection);
            }

            return new List<TaskModel2>();
        }

        private List<TaskModel2> SortTasksByCreateDateString(SortDirection sortDirection)
        {
            var tasks = MockDataProvider.GetCollection<TaskModel2>(DataFileName);
            return tasks.OrderBy("createDateString", sortDirection).ToList();
        }

        private List<TaskModel2> SortTasksByDueDateString(SortDirection sortDirection)
        {
            var tasks = MockDataProvider.GetCollection<TaskModel2>(DataFileName);
            return tasks.OrderBy("dueDateString", sortDirection).ToList();
        }

    }
}
