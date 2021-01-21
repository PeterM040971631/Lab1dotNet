using System.IO;
using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;

namespace Lab1 {
    class Program {

        public static IList<string> words = new List<string>();

        static void Main(string[] args) {

            string input = null;

            while (input != "x") {
                
                Console.Write(
                    //"Hello World!!! My First C# App\n" +
                    //"Options\n" +
                    //"----------\n" +
                    "1 - Import Words From File\n" +
                    "2 - Bubble Sort words\n" +
                    "3 - LINQ / Lambda sort words\n" +
                    "4 - Count the Distinct Words\n" +
                    "5 - Take the first 10 words\n" +
                    "6 - Get the number of words that start with 'j' and display the count\n" +
                    "7 - Get and display of words that end with 'd' and display the count\n" +
                    "8 - Get and display of words that are greater than 4 characters long, and display the count\n" +
                    "9 - Get and display of words that are less than 3 characters long and start with the letter 'a', and display the count\n" +
                    "x - Exit\n\n" +
                    "Make a selection: "
                );

                input = Console.ReadLine();
                if (input.Length != 1) {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid input");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("\n");
                    continue;
                }
                char inputChar = input.ToCharArray()[0];

                Console.Clear();

                switch (inputChar) {
                    case '1':
                        getWords();
                        break;
                    case '2':
                        BubbleSort(words);
                        break;
                    case '3':
                        sortList(words);
                        break;
                    case '4':
                        distinct();
                        break;
                    case '5':
                        display10();
                        break;
                    case '6':
                        displayJ();
                        break;
                    case '7':
                        displayD();
                        break;
                    case '8':
                        display4MoreLong();
                        break;
                    case '9':
                        displayStartALess3();
                        break;
                    case 'x':
                        return;
                    default:
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid input");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("\n");
                        break;
                }

                Console.WriteLine("\n");
            }
        }

        /* get the words from the Words.txt file */
        public static int getWords() {
            if (!File.Exists("Words.txt")) return -1;
            Console.WriteLine("Reading words");
            StreamReader reader = new StreamReader("Words.txt");
            string[] wordsRead = Regex.Split(reader.ReadToEnd(), "[\n\r ]+");
            int emptyWords = 0;

            for (int i = 0; i < wordsRead.Length; i++) {
                if (wordsRead[i].Length <= 0) {
                    emptyWords++;
                    continue;
                }
                words.Add(wordsRead[i]);
            }

            Console.WriteLine("Reading words complete\nNumber of words found: " + (wordsRead.Length-emptyWords));

            return wordsRead.Length;
        }

        /* manual bubble sort */
        public static IList<string> BubbleSort(IList<string> words) {
            IList<string> sortedWords = new List<string>();
            for (int i = 0; i < words.Count; i++) {
                sortedWords.Add(words[i]);
            }

            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            
            bool done = true;
            for (int j = sortedWords.Count-1; j > 0; j--) {
                done = true;
                for (int i = 0; i < j; i++) {
                    if (String.Compare(sortedWords[i], sortedWords[i+1]) > 0) {
                        string tempString = sortedWords[i];
                        sortedWords[i] = sortedWords[i + 1];
                        sortedWords[i + 1] = tempString;
                        done = false;
                    }
                }
                if (done) break;
            }

            watch.Stop();
            Console.WriteLine("Time elapsed: "+watch.ElapsedMilliseconds+"ms");
            return words;
        }

        /* sort using linq  */
        public static IList<string> sortList(IList<string> words) {
            IList<string> sortedWords = new List<string>();
            for (int i = 0; i < words.Count; i++) {
                sortedWords.Add(words[i]);
            }

            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();

            sortedWords = sortedWords.OrderBy(x => x).ToList();
            //words.Cast<string>().OrderBy(x => String.Compare(x, x+1));

            watch.Stop();
            Console.WriteLine("Time elapsed: " + watch.ElapsedMilliseconds + "ms");
            return sortedWords;
        }

        /* print distinct words */
        public static void distinct() {

            int distinctCount = words.Distinct().ToArray().Length; //.SingleOrDefault()

            Console.WriteLine("The number of distinct words is: "+distinctCount);
        }

        /* print the first 10 words */
        public static void display10() {
            
            string[] first10 = words.Take(10).ToArray(); //.SingleOrDefault()

            for (int i = 0; i < Math.Min(10,words.Count); i++) {
                Console.WriteLine(first10[i]);
            }
        }

        /* print all words that start with j */
        public static void displayJ() {

            string[] linqSearch = words.Where(x => x.StartsWith('j')).ToArray(); //.SingleOrDefault()

            for (int i = 0; i < linqSearch.Length; i++) {
                Console.WriteLine(linqSearch[i]);
            }
            Console.WriteLine("Number of words that start with 'j': "+linqSearch.Length);
        }

        /* print words that end with d */
        public static void displayD() {

            string[] linqSearch = words.Where(x => x.EndsWith('d')).ToArray(); 

            for (int i = 0; i < linqSearch.Length; i++) {
                Console.WriteLine(linqSearch[i]);
            }
            Console.WriteLine("Number of words that end with 'd': " + linqSearch.Length);
        }

        /* display words that are more than 4 characters long */
        public static void display4MoreLong() {

            string[] linqSearch = words.Where(x => x.Length > 4).ToArray();

            for (int i = 0; i < linqSearch.Length; i++) {
                Console.WriteLine(linqSearch[i]);
            }
            Console.WriteLine("Number of words longer than 4 characters: " + linqSearch.Length);
        }

        /* display all words shorter than 3 characters that start with a */
        public static void displayStartALess3() {

            string[] linqSearch = words.Where(x => x.Length < 3 && x.StartsWith('a')).ToArray();

            for (int i = 0; i < linqSearch.Length; i++) {
                Console.WriteLine(linqSearch[i]);
            }
            Console.WriteLine("Number of words less than 3 characters and start with 'a': " + linqSearch.Length);
        }

    }
}
