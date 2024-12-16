using System;

namespace SortHelper.Attributes
{
    /// <summary>
    /// Use this attribute on a property when it has a real name and also an alias name
    /// Here is an example : sometimes we use JsonProperty on a property to send it to the output with a different name
    /// So the client knows the alias name and not the real name of property
    /// therefore when it wants to ask for sort it uses the alias name and you should be able to get the real name and use it as sortProperty
    /// </summary>
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
