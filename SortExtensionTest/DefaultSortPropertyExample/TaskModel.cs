using SortHelper.Attributes;

namespace SortExtensionTest.DefaultSortPropertyExample
{
    public class TaskModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }

        [DefaultSortProperty]
        public DateTime CreateDate { get; set; }

        public int AssignedToUserId { get; set; }

        public DateTime DueDate { get; set; }

    }
}
