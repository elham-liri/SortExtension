using SortHelper.Attributes;

namespace SortExtensionTest
{
    public interface ITaskModel
    {
         int Id { get; set; }
         string? Title { get; set; }
         DateTime CreateDate { get; set; }
         int AssignedToUserId { get; set; }
         DateTime DueDate { get; set; }

         string ToString();
    }
}
