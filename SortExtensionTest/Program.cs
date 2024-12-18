using SortExtensionTest.AliasSortPropertyExample;
using SortExtensionTest.AlternativeSortPropertyExample;
using SortExtensionTest.DefaultSortPropertyExample;
using SortExtensionTest.Helpers;
using SortHelper.Enums;

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
                var sortDirection = GetSortDirection();
                while (!sortDirection.HasValue)
                {
                    sortDirection = GetSortDirection();
                }
                SortByDefault(sortDirection);
                break;
            }
        case "3":
            {
                var sortDirection = GetSortDirection();
                while (!sortDirection.HasValue)
                {
                    sortDirection = GetSortDirection();
                }
                var sortProperty = GetAlternativeSortProperty();
                SortByAlternative(sortProperty,sortDirection.Value);
                break;
            }
        case "4":
            {
                var sortDirection = GetSortDirection();
                while (!sortDirection.HasValue)
                {
                    sortDirection = GetSortDirection();
                }
                SortByAlias(sortDirection.Value);
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

static void SortByDefault(SortDirection? sortDirection = null)
{
    var defaultSorter = new SortByDefault();

    var tasks = sortDirection.HasValue
        ? defaultSorter.SortTasksByDefault(sortDirection.Value)
        : defaultSorter.SortTasksByDefault();

    MockDataProvider.PrintCollection(tasks);
}

static void SortByAlternative(string sortProperty, SortDirection sortDirection)
{
    var alternativeSorter = new SortByAlternative();
    var tasks = alternativeSorter.SortByAlternativeSortProperty(sortProperty, sortDirection);
    MockDataProvider.PrintCollection(tasks);
}

static void SortByAlias(SortDirection sortDirection)
{
    var alternativeSorter = new SortByAlias();
    var tasks = alternativeSorter.SortTasksBAssignedUser( sortDirection);
    MockDataProvider.PrintCollection(tasks);
}

static SortDirection? GetSortDirection()
{
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine("Choose Sort Direction - Enter 1 for Ascending or 2 for Descending :");
    Console.ForegroundColor = ConsoleColor.White;

    var sortDirectionInput = Console.ReadLine();

    switch (sortDirectionInput)
    {
        case "1": return SortDirection.Ascending;
        case "2": return SortDirection.Descending;
        default: return null;
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

