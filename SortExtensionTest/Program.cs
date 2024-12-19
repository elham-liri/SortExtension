using SortExtensionTest.AliasSortPropertyExample;
using SortExtensionTest.AlternativeSortPropertyExample;
using SortExtensionTest.DefaultSortPropertyExample;
using SortExtensionTest.Helpers;

for (; ; )
{
    ShowMenu();
    Console.WriteLine("Please Enter A Number:");
    var input = Console.ReadLine();
    switch (input)
    {
        case "1":
            {
                SortByDefault();
                break;
            }
        case "2":
            {
                var isDescendingSort = IsDescendingSort();
                SortByDefault(isDescendingSort);
                break;
            }
        case "3":
            {
                var isDescendingSort = IsDescendingSort();
                var sortProperty = GetAlternativeSortProperty();
                SortByAlternative(sortProperty,isDescendingSort);
                break;
            }
        case "4":
            {
                var isDescendingSort = IsDescendingSort();
                SortByAlias(isDescendingSort);
                break;
            }
        default:
            Environment.Exit(0);
            break;
    }
}

static void ShowMenu()
{
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine("Choose one of following options to see the result");
    Console.WriteLine("=========================================");
    Console.WriteLine("");

    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine("1. Sort by default sortProperty and default sort Direction");
    Console.WriteLine("2. Sort by default sortProperty and chosen sort Direction");
    Console.WriteLine("3. Sort by alternative sort property");
    Console.WriteLine("4. Sort by alias sort property");

    Console.WriteLine("");
    Console.WriteLine("");

    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("ENTER 0 TO EXIT");
    Console.WriteLine("----------------");

    Console.ForegroundColor = ConsoleColor.White;
}

static void SortByDefault(bool? descendingSort=null)
{
    var defaultSorter = new SortByDefault();

    var tasks = descendingSort.HasValue
        ? defaultSorter.SortTasksByDefault(descendingSort.Value)
        : defaultSorter.SortTasksByDefault();

    MockDataProvider.PrintCollection(tasks);
}

static void SortByAlternative(string sortProperty, bool descendingSort)
{
    var alternativeSorter = new SortByAlternative();
    var tasks = alternativeSorter.SortByAlternativeSortProperty(sortProperty, descendingSort);
    MockDataProvider.PrintCollection(tasks);
}

static void SortByAlias(bool descendingSort)
{
    var alternativeSorter = new SortByAlias();
    var tasks = alternativeSorter.SortTasksBAssignedUser(descendingSort);
    MockDataProvider.PrintCollection(tasks);
}

static bool IsDescendingSort()
{
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine("Choose Sort Direction - Enter 1 for Ascending or 2 for Descending :");
    Console.ForegroundColor = ConsoleColor.White;

    var sortDirectionInput = Console.ReadLine();

    switch (sortDirectionInput)
    {
        case "1": return false;
        case "2": return true;
        default: return false;
    }
}

static string GetAlternativeSortProperty()
{
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine("Choose Sort Property - Enter 1 for firstColumn or 2 for secondColumn :");
    Console.ForegroundColor = ConsoleColor.White;

    var sortPropertyInput = Console.ReadLine();
    switch (sortPropertyInput)
    {
        case "1": return "createDateString";
        case "2": return "dueDateString";
        default: return "createDateString";
    }
}

