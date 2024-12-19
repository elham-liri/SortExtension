using SortExtensionTest.Helpers;
using SortHelper;

namespace SortExtensionTest.AliasSortPropertyExample
{
    internal class SortByAlias
    {
        private string DataFileName => "MOCK_DATA_Task1.json";

        public List<TaskModel3> SortTasksBAssignedUser(bool descendingSort)
        {
            var tasks = MockDataProvider.GetCollection<TaskModel3>(DataFileName);
            return tasks.OrderBy("userCode", descendingSort).ToList();
        }
    }
}
