In our webApi project we had to sort the collections and send it to client and we had a generic extension method for it.

In this case it is the common scenario:

FrontEnd offers some sorting options to user like list of columns and directions which the collection can be sort by.

User chooses desired options (which we call SortProperty and SortDirection)
and the collection will be reordered based on given options.

Here we had scenarios where we had to change the given sort property :
1. We had to hide the name of the property
```    
public class TaskModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public DateTime CreateDate { get; set; }

        [JsonProperty("userCode")]
        public int AssignedToUserId { get; set; }
        public DateTime DueDate { get; set; }
    }

```
As you can see we send property "AssignedToUserId" out with a differnet name which is "userCode". So when user chooses to sort based on this property the sort property sent to backEnd will be "userCode" but there is no property with this name on the class so we had to hardcode it like : 

```
if (filter.SortProperty == "userCode") 
    filter.SortProperty = "AssignedToUserId";

```

2. 