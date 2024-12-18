using SortHelper.Attributes;

namespace SortExtensionTest.AliasSortPropertyExample
{
    public class TaskModel3:ITaskModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public DateTime CreateDate { get; set; }
        [AliasSortProperty("userCode")]
        public int AssignedToUserId { get; set; }
        public DateTime DueDate { get; set; }

        public override string ToString()
        {
            return $"User {AssignedToUserId,-20}" +
                   $"finished {Title,-20}" +
                   $"on {DueDate}";
        }
    }
}
