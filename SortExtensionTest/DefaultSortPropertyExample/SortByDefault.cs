using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            return tasks.ToList(); //.OrderBy("", SortDirection.Ascending).ToList();
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
            appPath = appPath.Remove(appPath.IndexOf("bin"));

            return $@"{appPath}\Files\";
        }
    }
}
