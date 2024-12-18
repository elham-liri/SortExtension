using SortExtensionTest.AlternativeSortPropertyExample;
using SortExtensionTest.Helpers;
using SortHelper.Enums;
using SortHelper;

namespace SortExtensionTest.AliasSortPropertyExample
{
    internal class SortByAlias
    {
        private string DataFileName => "MOCK_DATA_Task1.json";

        public List<TaskModel3> SortTasksBAssignedUser(SortDirection sortDirection)
        {
            var tasks = MockDataProvider.GetCollection<TaskModel3>(DataFileName);
            return tasks.OrderBy("userCode", sortDirection).ToList();
        }
    }
}
