namespace Lab3
{
    internal interface IText
    {
        void SortSentencesByWordCount();
        void SortSentencesByLength();
        void FindWordsInQuestionsByLength(int length);
        void RemoveWordsByLengthStartingWithConsonant(int length);
        void ReplaceWordsByLengthInSentence(int sentenceIndex, int length, string replacement);
        void RemoveStopWords();
    }
}
