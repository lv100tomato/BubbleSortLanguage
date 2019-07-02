using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BubbleSortLanguage
{
    class Program
    {
        public static long[] Source;
        public static int[] Memory;
        public static int PointerA;
        public static int PointerB;
        public static int Index;

        static void Main(string[] args)
        {
            List<long> preSource = new List<long>();
            string read;

            //read source
            if(args.Length < 1)
            {

                read = Console.ReadLine();
                while("" != read)
                {
                    try
                    {
                        preSource.Add(long.Parse(read));
                    }
                    catch
                    {
                        Console.WriteLine("Error : Only accept long integer");
                        return;
                    }
                    read = Console.ReadLine();
                }

                Source = preSource.ToArray();

            }
            else
            {
                StreamReader file;
                try
                {
                    file = new StreamReader(args[0]);
                }
                catch
                {
                    Console.WriteLine("Can't open file : " + args[0]);
                    return;
                }

                read = file.ReadLine();
                while (null != read)
                {
                    try
                    {
                        preSource.Add(long.Parse(read));
                    }
                    catch
                    {
                    }
                    read = file.ReadLine();
                }

                Source = preSource.ToArray();
            }

            //bubblesort
            Memory = Enumerable.Repeat(0, 2048).ToArray();
            PointerA = 0;
            PointerB = 0;
            Index = 0;

            bool isSorted;
            do
            {
                isSorted = true;

                for(Index = 0; Index < Source.Length - 1; ++Index)
                {
                    if(Source[Index] > Source[Index + 1])
                    {
                        //swap
                        isSorted = false;
                        long cmd = Source[Index + 1];
                        Source[Index + 1] = Source[Index];
                        Source[Index] = cmd;

                        //do command
                        cmd = cmd % 32;
                        cmd = cmd + (Source[Index + 1] % 32);
                        cmd = cmd % 32;
                        switch (cmd)
                        {
                            case 0:
                                PointerA++;
                                break;
                            case 1:
                                PointerB++;
                                break;
                            case 2:
                                PointerA--;
                                break;
                            case 3:
                                PointerB--;
                                break;
                            case 4:
                                Memory[PointerA]++;
                                break;
                            case 5:
                                Memory[PointerB]++;
                                break;
                            case 6:
                                Memory[PointerA]--;
                                break;
                            case 7:
                                Memory[PointerB]--;
                                break;
                            case 8:
                                Console.Write((char)Memory[PointerA]);
                                break;
                            case 9:
                                Console.Write((char)Memory[PointerB]);
                                break;
                            case 10:
                                Memory[PointerA] = Console.Read();
                                break;
                            case 11:
                                Memory[PointerB] = Console.Read();
                                break;
                            case 12:
                                Source[Index + Memory[PointerA]] += Memory[PointerB];
                                break;
                            case 13:
                                Source[Index + Memory[PointerA]] -= Memory[PointerB];
                                break;
                            default:
                                break;
                        }
                    }
                }

            } while (!isSorted);
            Console.WriteLine();
            //Console.ReadLine();
        }
    }
}
