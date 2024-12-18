using Newtonsoft.Json;
using SortExtensionTest.Helpers;
using SortHelper.Attributes;

namespace SortExtensionTest.AlternativeSortPropertyExample
{
    public class TaskModel2 : ITaskModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public int AssignedToUserId { get; set; }

        [AlternativeSortProperty(nameof(CreateDate))]
        public string? CreateDateString => CreateDate.ToUserFriendlyDate();

        [AlternativeSortProperty(nameof(DueDate))]
        public string? DueDateString => DueDate.ToUserFriendlyDate();


        //[JsonIgnore] 
        public DateTime CreateDate { get; set; }
       //[JsonIgnore] 
        public DateTime DueDate { get; set; }

        public override string ToString()
        {
            return $"Create on {CreateDateString,-20}" +
                   $"Due on {DueDateString,-20}" +
                   $"{Title}";
        }
    }
}
