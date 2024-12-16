using System;

namespace SortHelper.Attributes
{
    /// <summary>
    /// Use this attribute when you have a given sort property but you are supposed to sort by another property
    /// Here is one example to use this attribute : Suppose you have two property "A" and "B" in your class
    /// "A" contains the actual value and is not sent to the output
    /// "B" holds a user-friendly form of "A"'s value and has been sent to the output
    /// now you are asked to sort based on "B" so the given sortProperty is "B"
    /// but because the main value is stored in "A" the actual sort property should be "A"
    /// so you use this Attribute on "B" and give the "A" as alternative sort property
     /// </summary>
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
