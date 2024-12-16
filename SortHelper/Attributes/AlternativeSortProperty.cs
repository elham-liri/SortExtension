using System;

namespace SortHelper.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class AlternativeSortProperty:Attribute
    {
        public AlternativeSortProperty(string alternativeName)
        {
            AlternativeName = alternativeName;
        }

        public string AlternativeName { get;  }
    }
}
