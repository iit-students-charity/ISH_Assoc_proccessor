using System;

namespace aois7
{
    class Program
    {
        class GL
        {
            public bool G { get; set; }
            public bool L { get; set; }
            public GL()
            {
                G = L = false;
            }
        }

        class Word
        {
            public const int WORD_SIZE = 8;
            public static Random Random { get; } = new Random();
            public bool[] word;
            public Word()
            {
                word = new bool[WORD_SIZE];
                for (int i = 0; i < WORD_SIZE; i++)
                {
                    word[i] = (Random.Next(-1, 2) == 1);
                }
            }
            public void Show()
            {
                for (int i = 0; i < WORD_SIZE; i++)
                {
                    Console.Write(word[i] == false ? "0" : "1");
                }
            }
            public static GL GreaterThan(Word a, Word b, int k = WORD_SIZE - 1)
            {
                GL result = new GL();
                GL resultPlusOne = k == 0 ? new GL() : GreaterThan(a, b, k - 1);
                result.G = resultPlusOne.G || (!a.word[k] && b.word[k] && !resultPlusOne.L);
                result.L = resultPlusOne.L || (a.word[k] && !b.word[k] && !resultPlusOne.G);
                return result;
            }
        }

        class Dictionary
        {
            public static int DIC_SIZE { get; } = 20;
            public Word[] dictionary;
            public Dictionary()
            {
                dictionary = new Word[DIC_SIZE];
                for (int i = 0; i < DIC_SIZE; i++)
                {
                    dictionary[i] = new Word();
                }
            }
            public void Show()
            {
                for (int i = 0; i < DIC_SIZE; i++)
                {
                    Console.Write($"[{i + 1}]  =\t");
                    dictionary[i].Show();
                    Console.WriteLine();
                }
            }
            public void Sort(int IncOrDec)
            {
                Word[] d = dictionary;
                bool UpOrDown = IncOrDec == 1;
                for (int gap = DIC_SIZE / 2; gap > 0; gap /= 2)
                {
                     for (int i = gap; i < DIC_SIZE; i++)
                     {
                        Word temp = dictionary[i];
                        int j;
                        for (j = i; j >= gap && Word.GreaterThan(temp, dictionary[j - gap]).G == UpOrDown; j -= gap)
                        {
                            dictionary[j] = dictionary[j - gap];
                        }
                        dictionary[j] = temp;
                     }
                }
                Show();
                dictionary = d;
            }
        }

        static void Main(string[] args)
        {
            Dictionary D = new Dictionary();

            Console.WriteLine("Start dictionary:");
            D.Show();

            Console.WriteLine("Sorted dictionary (increasing):");
            D.Sort(1);
            Console.WriteLine("Sorted dictionary (decreasing):");
            D.Sort(0);

            Console.ReadKey();
        }
    }
}