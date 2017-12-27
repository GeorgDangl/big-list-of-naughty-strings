using System.Text.RegularExpressions;

namespace big_list_of_naughty_strings
{
    public class NaughtyString
    {
        public NaughtyString(int index, string value)
        {
            _index = index;
            Value = value;
        }

        private readonly int _index;

        public string Value { get; }

        /// <summary>
        /// This overrides is to avoid errors when naughty strings would otherwise get displayed in the text explorer
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"#{_index:000}";
        }

        public bool ContainsNonPrintableCharacter()
        {
            return Regex.IsMatch(Value, NON_PRINTABLE_REGEX);
        }

        public string ValueWithoutNonPrintableCharacters()
        {
            var replacedValue = Regex.Replace(Value, NON_PRINTABLE_REGEX, string.Empty);
            return replacedValue;
        }

        private const string NON_PRINTABLE_REGEX = "[\u0000-\u0008\u000b\u000e-\u001f\u2029]";
    }
}
