using System.Xml.Serialization;

namespace Lab4
{
    internal class Lab4
    {
        private static string XMLFile = "exported.xml";
        private static string replacement = "***";

        private static void Menu()
        {
            Console.WriteLine("Menu:");
            Console.WriteLine("1. Вывести все предложения в порядке возрастания количества слов.");
            Console.WriteLine("2. Вывести все предложения в порядке возрастания длины предложения.");
            Console.WriteLine("3. Найти слова заданной длины в вопросительных предложениях.");
            Console.WriteLine("4. Удалить из текста все слова заданной длины, начинающиеся с согласной.");
            Console.WriteLine("5. Заменить слова заданной длины на указанную подстроку.");
            Console.WriteLine("6. Удалить стоп-слова из текста.");
            Console.WriteLine("7. Экспортировать текст в XML-документ.");
            Console.WriteLine("[ESC] -> Выход.\n");
            Console.Write("Выберите действие:\n");
        } 

        public static void ExportToXml(Text text, string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Text));
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, text);
            }
            Console.WriteLine("...Нажмите любую клавишу для продолжения...");
            Console.ReadKey(true);
        }

        public static void Main(string[] args)
        {
            ParseII parser = new ParseII("text.txt");
            parser.Run();
            Text tokenedText = parser.GetParsedText();
          
            bool isValidKey = false;  

            while (!isValidKey)
            {
                Console.Clear();
                Menu();
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);  

                switch (keyInfo.Key)
                {
                    case ConsoleKey.D1:
                        Console.WriteLine("\nВы нажали 1\n");
                        tokenedText.SortSentencesByWordCount();
                        break;
                    case ConsoleKey.D2:
                        Console.WriteLine("\nВы нажали 2\n");
                        tokenedText.SortSentencesByLength();
                        break;
                    case ConsoleKey.D3:
                        Console.Write("\nВы нажали 3\nВведите длину слова: ");
                        int.TryParse(Console.ReadLine(), out int length);
                        tokenedText.FindWordsInQuestionsByLength(length);
                        break;
                    case ConsoleKey.D4:
                        Console.Write("\nВы нажали 4\nВведите длину слова: ");
                        int.TryParse(Console.ReadLine(), out int length2);
                        tokenedText.RemoveWordsByLengthStartingWithConsonant(length2);
                        break;
                    case ConsoleKey.D5: 
                        Console.WriteLine("\nВы нажали 5\n");
                        tokenedText.PrintSentenceWithNumeration();
                        Console.Write("Введите номер предложения: ");
                        int.TryParse(Console.ReadLine(), out int sentenceIndex);
                        Console.Write("Введите длину слова: ");
                        int.TryParse(Console.ReadLine(), out int length3);
                        tokenedText.ReplaceWordsByLengthInSentence(sentenceIndex, length3, replacement);
                        break;
                    case ConsoleKey.D6:
                        Console.WriteLine("\nВы нажали 6\n Стоп-слова удалены!\n\n");
                        tokenedText.RemoveStopWords();
                        break;
                    case ConsoleKey.D7:
                        Console.WriteLine("\nВы нажали 7\nЭкспорт текста в XML....");
                        ExportToXml(tokenedText, XMLFile);
                        break;
                    case ConsoleKey.D8:
                        tokenedText.ConcordPrint();
                        break;
                    case ConsoleKey.Escape:
                        Console.Clear();
                        Console.WriteLine("Вы нажали Esc. Завершение программы.");
                        isValidKey = true;
                        break;
                    default:
                        Console.WriteLine("Неверная клавиша. Попробуйте снова.");
                        break;
                }
            }
        }
    }
}
