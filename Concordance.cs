namespace Lab4
{
    public class Concordance
    {
        private Dictionary<string, (int frequency, SortedSet<int> sentenceNumbers)> concordance;
        private SortedSet<Word> uniqueWords;

        public Concordance()
        {
            concordance = new Dictionary<string, (int frequency, SortedSet<int> sentenceNumbers)>();
            uniqueWords = new SortedSet<Word>();
        }

        public Dictionary<string, (int frequency, SortedSet<int> sentenceNumbers)> ConcordanceData
        {
            get { return concordance; }
            set { concordance = value; }
        }

        public SortedSet<Word> UniqueWords
        {
            get { return uniqueWords; }
            set { uniqueWords = value; }
        }

        public void CreateUniqueList(List<Sentence> sentences)
        {
            foreach (var sentence in sentences)
            {
                foreach(var token in sentence.Tokens)
                {
                    if(token is Word word)
                    {
                        Word newWord = new Word();
                        newWord.WordSetGet = word.WordSetGet.ToLower();
                        UniqueWords.Add(newWord);
                    }
                }
            }
            Search(sentences);
            PopulateConcordance();
            PrintConcordance();
        }

        public void Search(List<Sentence> sentences)
        {
            foreach (var word in UniqueWords)
            {
                for (int i = 0; i < sentences.Count(); i++)
                {
                    var sentence = sentences[i];
                    foreach (var token in sentence.Tokens)
                    {
                        if (token is Word tokenWord && word.WordSetGet.Equals(tokenWord.WordSetGet, StringComparison.OrdinalIgnoreCase))
                        {
                            word.IncrementFrequency(i + 1);
                        }
                    }
                }
            }
        }

        public void PopulateConcordance()
        {
            foreach (var word in UniqueWords)
            {
                ConcordanceData[word.WordSetGet] = (word.Frequency, word.SentenceNumbers);
            }
        }

        public void PrintConcordance()
        {
            foreach (var entry in ConcordanceData)
            {
                string word = entry.Key;
                int frequency = entry.Value.frequency;
                SortedSet<int> sentenceNumbers = entry.Value.sentenceNumbers;

                Console.Write("{0,-20} {1} : ", word, frequency);

                int index = 0;
                int totalSentences = sentenceNumbers.Count;

                foreach (var sentNum in sentenceNumbers)
                {
                    if (index == totalSentences - 1)
                    {
                        Console.Write(sentNum + ".");
                    }
                    else
                    {
                        Console.Write(sentNum + ", ");
                    }
                    index++;
                }
                Console.WriteLine();
            }
        }




    }

}
