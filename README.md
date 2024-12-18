## OrderBy extension methods to sort queryable and enumerable collections 
This library provides some extension methods to help sorting collections and some attributes to specify some conditions for sorting 

### Parameters
These are possible input parameters for extesion methods

1. sortProperty : name of property by which the collection should be sorted. Should be camelCase string
2. sortDirection : direction of sort provided by following enum:

    ```
    enum SortDirection : byte
    {
        Ascending=0,
        Descending=1
    }
    ```
 
 ### Methods
 1. Order by given sort property and sort direction
 ```
 IQueryable<T> OrderBy<T>(this IQueryable<T> source, string sortProperty,SortDirection sortDirection) where T : class
 ```
 This method adds orderBy clause to a query with given sortProperty and sortDirection considering all conditions (will be mentioned in following section).

 Usage: 
 ```
 var orderedCollection=collection.OrderBy("propertyName", sortDirection).ToList();
 ```
 Or
 ```
 var orderedCollection=collection.OrderBy("propertyName", sortDirection).Take(24).ToList();
 ```

2. Order by default sort property and given sort direction
```
IQueryable<T> OrderBy<T>(this IQueryable<T> source, SortDirection sortDirection)where T : class
```
 This method adds orderBy clause to a query with default sortProperty and given sortDirection .

  Usage: 
 ```
 var orderedCollection=collection.OrderBy(sortDirection).ToList();
 ```

 3. Order by default sort property and default sort direction
```
IQueryable<T> OrderBy<T>(this IQueryable<T> source)where T : class
```
 This method adds orderBy clause to a query with default sortProperty and default sortDirection ;

  Usage: 
 ```
 var orderedCollection=collection.OrderBy().ToList();
 ```

### Attributes
These are attributes to mark properties which has special conditions to be sorted by

1. [DefaultSortProperty(string? defaultSortDirection)]

    when you mark a property with this attribute, that property becomes the default sort property for that entity and when you use orderBy extension method you can skip specifying sort property 
    
    You can also provide a default sort direction which makes it possible to order without mentioning sortProperty and sortDirection

example : 
```
    public class TaskModel 
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        [DefaultSortProperty]
        public DateTime CreateDate { get; set; }
    }
```
now you can sort a collection of this entity like this : 

```
IQueryable<TaskModel> collection = _database.GetCollection<TaskModel>();
var orderedCollection = collection.OrderBy(SortDirectin.Ascending).Take(20).ToList;
```
OR : 

```
    public class TaskModel 
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        [DefaultSortProperty(SortDirection.Descending)]
        public DateTime CreateDate { get; set; }
    }
```
now you can sort a collection of this entity like this : 

```
IQueryable<TaskModel> collection = _database.GetCollection<TaskModel>();
var orderedCollection = collection.OrderBy().Take(20).ToList;
```
