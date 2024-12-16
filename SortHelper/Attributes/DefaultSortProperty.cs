using System;
using SortHelper.Enums;

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
            DefaultSortDirection = SortDirection.Ascending;
        }

        public DefaultSortProperty(SortDirection sortDirection)
        {
            DefaultSortDirection = sortDirection;
        }

        public SortDirection DefaultSortDirection { get; }
    }
}
