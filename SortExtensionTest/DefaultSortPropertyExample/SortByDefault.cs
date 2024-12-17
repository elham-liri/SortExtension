using Newtonsoft.Json;
using SortHelper;
using SortHelper.Enums;

namespace SortExtensionTest.DefaultSortPropertyExample
{
    public class SortByDefault
    {
        public List<TaskModel> SortTasksByDefault()
        {
            var tasks = GetCollection();
            return tasks.OrderBy().ToList();
        }

        public List<TaskModel> SortTasksByDefault(SortDirection sortDirection)
        {
            var tasks = GetCollection();
            return tasks.OrderBy(sortDirection).ToList();
        }

        private IQueryable<TaskModel> GetCollection()
        {
            var path = @$"{FilesPath()}\MOCK_DATA_Task.json";

            var data = File.ReadAllText(path);
            var dataList = JsonConvert.DeserializeObject<List<TaskModel>>(data) ?? new List<TaskModel>();
            return dataList.AsQueryable();
        }

        private string FilesPath()
        {
            var appPath = Directory.GetCurrentDirectory();
            appPath = appPath.Remove(appPath.IndexOf("bin") - 1);

            return $@"{appPath}\Files\";
        }
    }
}
