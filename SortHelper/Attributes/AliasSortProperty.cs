using System;
using System.Collections.Generic;
using System.Text;

namespace SortHelper.Attributes
{

    [AttributeUsage(AttributeTargets.Property)]
    public class AliasSortProperty :Attribute
    {
        public AliasSortProperty( string aliasName)
        {
            AliasName = aliasName;
        }

        public string AliasName { get; }
    }
}
