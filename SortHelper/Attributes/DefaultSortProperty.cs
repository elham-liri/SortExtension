using System;

namespace SortHelper.Attributes
{
    /// <summary>
    /// use this attribute on the property you want to mark as the default sort property
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class DefaultSortProperty : Attribute
    {
        public DefaultSortProperty()
        {
            DescendingSort = false;
        }

        public DefaultSortProperty(bool descendingSort)
        {
            DescendingSort = descendingSort;
        }

        public bool DescendingSort { get; }
    }
}
