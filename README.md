## OrderBy extension methods to sort queryable and enumerable collections 
This library provides some extension methods to help sorting collections and some attributes to specify some conditions for sorting 

### Parameters
These are possible input parameters for extesion methods

**1. string sortProperty** : name of property by which the collection should be sorted. Should be **camelCase** string

**2. bool descendingSort** : sort will be ascending by default but you can change it to descending by setting this parameter true

 
 ### Methods
#### 1. Order by given sort property and sort direction
 ```
 IQueryable<T> OrderBy<T>(this IQueryable<T> source, string sortProperty,bool descendingSort) where T : class
 ```
 This method adds orderBy clause to a query with given sortProperty and sortDirection considering all conditions (will be mentioned in following section).

 Usage: 
 ```
 var orderedCollection=collection.OrderBy("propertyName", false).ToList();
 ```
 This will sort the collection by given property in ascending order

  Or
 ```
 var orderedCollection=collection.OrderBy("propertyName", true).Take(24).ToList();
 ```
  This will sort the collection by given property in descending order 

#### 2. Order by default sort property and given sort direction
```
IQueryable<T> OrderBy<T>(this IQueryable<T> source, bool descendingSort)where T : class
```
 This method adds orderBy clause to a query with default sortProperty and given sortDirection .

 Usage: 
 ```
 var orderedCollection=collection.OrderBy(false).ToList();
 ```

#### 3. Order by default sort property and default sort direction
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

#### 1. [DefaultSortProperty([descendingSort])]

when you mark a property with this attribute, that property becomes the default sort property for that entity and when you use orderBy extension method you can skip specifying sort property 
    
sort will be in ascending order by default but you can set the descendingSort=true to set the descnding order as the default sort direction for default sort property

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
var orderedCollection = collection.OrderBy().Take(20).ToList;
```

This will sort the collection by *"CreateDate"* in ascending order

OR : 

```
    public class TaskModel 
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        [DefaultSortProperty(true)]
        public DateTime CreateDate { get; set; }
    }
```
now you can sort a collection of this entity like this : 

```
IQueryable<TaskModel> collection = _database.GetCollection<TaskModel>();
var orderedCollection = collection.OrderBy().Take(20).ToList;
```

This will sort the collection by *"CreateDate"* in descending order

#### 2. [AlternativeSortProperty("alternativePropertyName")]

You mark a property with this attribute to tell the orderBy method that  if this property is given as the "sortProperty", which property it should use instead

For example :

```
    public class TaskModel2
    {
        public int Id { get; set; }
        public string? Title { get; set; }

        [AlternativeSortProperty(nameof(CreateDate))]
        public string? CreateDateString => CreateDate.ToUserFriendlyDate();

        [AlternativeSortProperty(nameof(DueDate))]
        public string? DueDateString => DueDate.ToUserFriendlyDate();

        [JsonIgnore]
        public DateTime CreateDate { get; set; }
        [JsonIgnore]
        public DateTime DueDate { get; set; }
    }
```
In this case we have two properties *"CreateDate"* and *"DueDate"* which hold the raw data and are not sent to the frontEnd. Also we have *"CreateDateString"* and *"DueDateString"* which provide user-friendly format of the same data.

Now imagine the user chooses to sort the collection by *"DueDateString"* so this will be the given sort property

```
IQueryable<TaskModel2> collection = _database.GetCollection<TaskModel2>();
var orderedCollection = collection.OrderBy("dueDateString",false).ToList;
```

but it will be sorted by *"DueDate"* which has been determined as the alternative sort property.

#### 3. [AliasSortProperty("aliasName")]

If there is a property which forever reason is known by different names, you can mark it with this attribute so that orderBy method can find it by alias name

for example:
```
    public class TaskModel3
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        
        [JsonProperty("userCode")]
        [AliasSortProperty("userCode")]
        public int AssignedToUserId { get; set; }
    }
```

here we have the property *"AssignedToUserId"* which is sent to frontEnd as *"userCode"* and this name is also introduced as the alias sort property so both following orderBy calls will sort the collection by *"AssignedToUserId"*

```
IQueryable<TaskModel3> collection = _database.GetCollection<TaskModel3>();
//use real name as sort property and ascending order
var orderedCollection1 = collection.OrderBy("assignedToUserId",false).ToList;
//use alias name as sort property and descending order
var orderedCollection2 = collection.OrderBy("userCode",true).false;
```




