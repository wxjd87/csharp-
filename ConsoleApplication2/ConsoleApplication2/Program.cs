using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
            string sentence = "twas brillin,and the slithy toves did gyre" + "and gimble in the wabe";
            List<string> words;
            words = WordProcessor.GetWords(sentence);
            Console.WriteLine("ORIGINAL:");
            foreach (string item in words)
            {
                Console.Write(item);
                Console.Write(' ');
            }
            Console.WriteLine('\n');
            words = WordProcessor.GetWords(sentence, recerseOrder true, capitalizeWords: true);
            Console.WriteLine("Capitalized :");
            foreach (string item in words)
            {
                Console.Write(item);
                Console.Write(' ');
            }
            Console.ReadKey();
        }
    }
}
