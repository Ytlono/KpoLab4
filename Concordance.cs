using System.Text.Encodings.Web;
using System.Text.Json;

namespace Lab4
{
    public class Concordance
    {
        private Dictionary<string, (int frequency, SortedSet<int> sentenceNumbers)> concordance;
        private SortedSet<Word> uniqueWords;

        public Concordance(List<Sentence> sentences,string JSONFile)
        {
            concordance = new Dictionary<string, (int frequency, SortedSet<int> sentenceNumbers)>();
            uniqueWords = new SortedSet<Word>();
            CreateUniqueList(sentences, JSONFile);
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

        public void CreateUniqueList(List<Sentence> sentences,string JSONFile)
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
            SaveToJson(JSONFile);
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


        public void SaveToJson(string filePath)
        {
            // Преобразование словаря в более подходящий формат для сериализации
            var serializedConcordance = new Dictionary<string, ConcordanceEntry>();

            foreach (var entry in concordance)
            {
                // Преобразуем каждый элемент словаря в отдельный объект
                serializedConcordance[entry.Key] = new ConcordanceEntry
                {
                    Frequency = entry.Value.frequency,
                    SentenceNumbers = entry.Value.sentenceNumbers
                };
            }

            // Сериализация словаря в JSON с настройками для русских символов
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping // Позволяет сохранять русские символы
            };

            string jsonString = JsonSerializer.Serialize(serializedConcordance, options);

            // Запись JSON в файл
            File.WriteAllText(filePath, jsonString);

            Console.WriteLine("Конкорданс успешно сохранен в файл JSON.");
        }


        public class ConcordanceEntry
        {
            public int Frequency { get; set; }
            public SortedSet<int> SentenceNumbers { get; set; }
        }

    }

}
