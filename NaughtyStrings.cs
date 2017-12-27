using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit.Sdk;

namespace big_list_of_naughty_strings
{
    public sealed class NaughtyStrings : DataAttribute
    {
        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            return NaughtyStringFactory
                .NaughtyStrings
                .Select((naughtyString, index) => new object[] {new NaughtyString(index, naughtyString)});
        }
    }
}
