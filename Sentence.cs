using System.Xml.Serialization;

namespace Lab3
{
    public enum SentenceTypes
    {
        Unknown,
        Statement,
        Question,
        Exclamatory,
    }

    public class Sentence
    {
        [XmlArray("Tokens")]
        [XmlArrayItem("Token")]
        public List<Token> Tokens { get; set; }

        [XmlIgnore]
        public int SentenceLengthByWord { get; set; }

        [XmlIgnore]
        public int SentenceLengthByChar { get; set; }

        [XmlIgnore]
        public SentenceTypes SentenceType { get; set; }

        public Sentence()
        {
            Tokens = new List<Token>();
            SentenceLengthByWord = 0;
            SentenceLengthByChar = 0;
            SentenceType = SentenceTypes.Unknown;
        }

        public void CalculateSentenceLengthByWord()
        {
            foreach (var token in Tokens)
            {
                if (token is Word)
                {
                    SentenceLengthByWord++;
                }
            }
        }

        public void CalculateSentenceLengthByChar()
        {
            foreach (Token token in Tokens)
            {
                SentenceLengthByChar += token.TokenLength;
            }
        }

        public void DetermineSentenceType()
        {
            var lastToken = Tokens.LastOrDefault();

            if (lastToken is Punctuation punctuation)
            {
                switch (punctuation.Symbol)
                {
                    case "?":
                        SentenceType = SentenceTypes.Question;
                        break;
                    case "!":
                        SentenceType = SentenceTypes.Exclamatory;
                        break;
                    case ".":
                        SentenceType = SentenceTypes.Statement;
                        break;
                    default:
                        SentenceType = SentenceTypes.Unknown;
                        break;
                }
            }
            else
            {
                SentenceType = SentenceTypes.Unknown;
            }
        }

        public override string ToString()
        {
            return string.Join(" ", Tokens.Select(t => t.ToString()));
        }
    }
}
 