using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace Lab3
{
    public enum WordInitialTypes
    {
        Unknown,
        Consonant,
        Vowel
    }

    public class Word : Token
    {
        private string _word;

        [XmlIgnore]
        private WordInitialTypes _wordInitialType;

        public Word() : base("")
        {
            _word = string.Empty;
            WordInitialType = WordInitialTypes.Unknown;
        }

        public Word(string word) : base(word)
        {
            _word = word;
            WordInitialType = WordInitialTypes.Unknown;
        }

        [XmlElement("Word")]
        public string WordSetGet
        {
            get { return _word; }
            set { _word = value; }
        }

        [XmlIgnore]
        public WordInitialTypes WordInitialType
        {
            get { return _wordInitialType; }
            set { _wordInitialType = value; }
        }

        public override string ToString()
        {
            return _word;
        }

        public void DetermineWordInitialType()
        {
            if (Regex.IsMatch(_word, @"^[^aeiouAEIOUаеёиоуыэюяАЕЁИОУЫЭЮЯ]"))
            {
                WordInitialType = WordInitialTypes.Consonant;
            }
            else
            {
                WordInitialType = WordInitialTypes.Vowel;
            }
        }
    }
}
