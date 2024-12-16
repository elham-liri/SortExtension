// See https://aka.ms/new-console-template for more information

using SortExtensionTest.DefaultSortPropertyExample;

Console.WriteLine("Test Sort By Default");

var defaultSorter = new SortByDefault();
var sortedList = defaultSorter.SortTasksByDefault();

foreach (var taskModel in sortedList)
{
    Console.WriteLine($"{taskModel.Title} Created on {taskModel.CreateDate.ToShortDateString()}");
}

