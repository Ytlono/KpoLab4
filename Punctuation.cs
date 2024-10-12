using System.Xml.Serialization;

namespace Lab3
{
    public class Punctuation : Token
    {
        [XmlElement("Punctuation")]
        public string Symbol { get; set; }

        public Punctuation() : base("")
        {
            Symbol = string.Empty;
        }

        public Punctuation(string symbol) : base(symbol)
        {
            Symbol = symbol;
        }

        public override string ToString()
        {
            return Symbol;
        }
    }
}
