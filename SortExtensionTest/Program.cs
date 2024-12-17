// See https://aka.ms/new-console-template for more information

using SortExtensionTest.DefaultSortPropertyExample;
using SortHelper.Enums;

Console.WriteLine("Test Sort By Default");

var defaultSorter = new SortByDefault();
var sortedList = defaultSorter.SortTasksByDefault(SortDirection.Descending);

foreach (var taskModel in sortedList)
{
    Console.WriteLine(taskModel.ToString());
}

