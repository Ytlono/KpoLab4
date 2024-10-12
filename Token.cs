using System.Xml.Serialization;

namespace Lab3
{
    [XmlInclude(typeof(Token))]
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
