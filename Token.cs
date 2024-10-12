using System.Xml.Serialization;

namespace Lab4
{
    [XmlInclude(typeof(Word))]
    [XmlInclude(typeof(Punctuation))]

    public abstract class Token
    {
        [XmlIgnore]
        public int TokenLength { get; set; }

        protected Token(string value)
        {
            TokenLength = value.Length;
        }

        protected Token() { }
    }
}
