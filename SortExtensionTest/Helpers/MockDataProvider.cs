﻿using Newtonsoft.Json;

namespace SortExtensionTest.Helpers
{
    internal static class MockDataProvider
    {
        internal static IQueryable<T> GetCollection<T>(string fileName) where T : class
        {
            var path = @$"{FilesPath()}\{fileName}";

            var data = File.ReadAllText(path);
            var dataList = JsonConvert.DeserializeObject<List<T>>(data) ?? new List<T>();
            return dataList.AsQueryable();
        }

        internal static void PrintCollection<T>(List<T> collection) where T : ITaskModel
        {
            foreach (var taskModel in collection)
            {
                Console.WriteLine(taskModel.ToString());
            }
        }

        private static string FilesPath()
        {
            var appPath = Directory.GetCurrentDirectory();
            appPath = appPath.Remove(appPath.IndexOf("bin") - 1);

            return $@"{appPath}\Files\";
        }
    }
}
