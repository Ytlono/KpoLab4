using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace Lab4  
{
    public enum WordInitialTypes
    {
        Unknown,
        Consonant,
        Vowel
    }
    
    public class Word : Token, IComparable<Word>
    {
        private string _word;

        private WordInitialTypes _wordInitialType;
        [XmlIgnore]
        public int Frequency { get; set; }
        [XmlIgnore]
        public SortedSet<int> SentenceNumbers { get; set; }


        public Word() : base("")
        {
            _word = string.Empty;
            WordInitialType = WordInitialTypes.Unknown;
            Frequency = 0;
            SentenceNumbers = new SortedSet<int>();
        }

        public Word(string word) : base(word)
        {
            _word = word;
            WordInitialType = WordInitialTypes.Unknown;
            Frequency = 0; // Инициализируем частоту
            SentenceNumbers = new SortedSet<int>(); // Инициализируем SentenceNumbers
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

        public void IncrementFrequency(int sentenceIndex)
        {
            Frequency++;
            SentenceNumbers.Add(sentenceIndex);
            
        }

        public int CompareTo(Word other)
        {
            if (other == null) return 1;
            return string.Compare(_word, other._word, StringComparison.Ordinal);
        }
    }
}
