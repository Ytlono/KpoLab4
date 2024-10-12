using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace Lab3
{
    public class Text : IText
    {
        [XmlArray("Text")]
        [XmlArrayItem("Sentence")]
        public List<Sentence> sentenceTokenList { set; get; }

        [XmlIgnore]
        private List<string> stopWords;

        [XmlIgnore]
        private const string fileStopWords = "stopwords.txt";


        public Text()
        {
            sentenceTokenList = new List<Sentence>();
            LoadStopWordsFromFile(fileStopWords);
        }

        [XmlIgnore]
        public List<string> StopWords
        {
            get { return stopWords; }
            set { stopWords = value; }
        }

        private void LoadStopWordsFromFile(string filePath)
        {
            string fileContent = File.ReadAllText(filePath);
            StopWords = Regex.Split(fileContent, @"\s+").ToList();
        }

        public void SortSentencesByWordCount()
        {
            var sortedSentences = sentenceTokenList.OrderBy(sentence => sentence.SentenceLengthByWord);

            foreach (var sentence in sortedSentences)
            {
                Console.WriteLine(sentence.ToString());
                Console.WriteLine(sentence.SentenceLengthByWord + "\n");
            }
            Console.ReadKey(true);
        }

        public void SortSentencesByLength()
        {
            var sortedSentences = sentenceTokenList.OrderBy(sentence => sentence.SentenceLengthByChar);
            foreach (var sentence in sortedSentences)
            {
                Console.WriteLine(sentence.ToString());
                Console.WriteLine(sentence.SentenceLengthByChar + "\n");
            }
            Console.ReadKey(true);
        }

        public void FindWordsInQuestionsByLength(int length)
        {
            bool found = false;

            foreach (var sentence in sentenceTokenList)
            {
                if (sentence.SentenceType == SentenceTypes.Question)
                {
                    foreach (var token in sentence.Tokens)
                    {
                        if (token is Word && token.TokenLength == length)
                        {
                            found = true;
                            Console.WriteLine(token.ToString());
                        }
                    }
                }
            }
            string result = found ? "" : "Not found";
            Console.WriteLine(result);
            Console.ReadKey(true);
        }

        public void RemoveWordsByLengthStartingWithConsonant(int length)
        {
            var filteredSentenceTokenList = new List<Sentence>(sentenceTokenList);

            foreach (var sentence in filteredSentenceTokenList)
            {
                sentence.Tokens.RemoveAll(token => token is Word word && word.WordInitialType == WordInitialTypes.Consonant && token.TokenLength == length);
            }

            foreach (var sentence in filteredSentenceTokenList)
            {
                Console.WriteLine(sentence.ToString());
            }
            Console.ReadKey(true);
        }

        public void PrintSentenceWithNumeration()
        {
            for (int i = 0; i < sentenceTokenList.Count(); i++)
            {
                Console.WriteLine($"{i + 1}. " + sentenceTokenList[i].ToString() + "\n");
            }
        }

        public void ReplaceWordsByLengthInSentence(int sentenceIndex, int length, string replacement)
        {
            var filteredSentenceTokenList = new List<Sentence>(sentenceTokenList);

            bool found = false;

            foreach (var token in filteredSentenceTokenList[sentenceIndex - 1].Tokens)
            {
                if (token is Word word && word.TokenLength == length)
                {
                    word.WordSetGet = replacement;
                    found = true;
                }
            }

            Console.WriteLine(filteredSentenceTokenList[sentenceIndex - 1].ToString());
            string result = found ? "" : "Not found";
            Console.WriteLine(result);
            Console.ReadKey(true);
        }

        public void RemoveStopWords()
        {
            var filteredSentenceTokenList = new List<Sentence>(sentenceTokenList);

            foreach (var sentence in filteredSentenceTokenList)
            {
                sentence.Tokens.RemoveAll(token => token is Word word && stopWords.Contains(word.ToString()));
            }
            Console.WriteLine("Оригинал:\n");
            foreach (var sentence in sentenceTokenList)
            {
                Console.WriteLine(sentence.ToString());
            }
            Console.WriteLine("Отредактированный:\n");
            foreach (var sentence in filteredSentenceTokenList)
            {
                Console.WriteLine(sentence.ToString());
            }
            Console.ReadKey(true);
        }
    }
}
