using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    public static class WordProcessor
    {
        public static List<string> GetWords(string sentence, bool capitalizeWords=false, bool recerseOrder=false,bool reverseWords=false)
    {
        List<string> words = new List<string>(sentence.Split(' '));
            if (capitalizeWords)
            {
                words = CapitalizeWords(words);
            }
            if(recerseOrder)
            {
                words = ReverseOrder(words);
            }
            if(reverseWords)
            {
                words = ReverseWords(words);
            }
            return words;

    }
        private static List<string> CapitalizeWords(List<string> words)
        {
            List<string> capitalizedWord = new List<string>();
            foreach (string item in words)
            {
                if (item.Length==0)
                {
                    continue;
                }
                if (item.Length == 1)
                {
                    capitalizedWord.Add(words[0].ToString().ToUpper());
                }
                else
                    capitalizedWord.Add(words[0].ToString().ToUpper() + item.Substring(1));
            }
            return capitalizedWord;
        }

        private static List<string> ReverseOrder(List<string> words)

        {
            List<string> revWords = new List<string>();
            for(int wordIndex=words.Count-1;wordIndex>=0;wordIndex--)
            {
                revWords.Add(words[wordIndex]);
            }
            return revWords;
        }
        private static List<string> ReverseWords(List<string> words)
        {
            List<string> revWords = new List<string>();
            foreach (string item in words)
            {
                revWords.Add(revWord(item));
            }
            return revWords;
        }
        private static string revWord(string word)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = word.Length-1; i >= 0 ; i--)
            {
                sb.Append(word[i]);
            }
            return sb.ToString();
        }
    }
}
