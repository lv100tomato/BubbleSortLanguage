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

        public static int MemSizeHalf = 2048;
        public static int Mod = 50;

        static void Main(string[] args)
        {
            List<long> preSource = new List<long>();
            string read;
            bool debug = false;
            string debOut = "";

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

                if(args.Length > 1)
                {
                    if(args[1] == "-debug")
                    {
                        debug = true;
                    }
                }
            }

            //bubblesort
            Memory = Enumerable.Repeat(0, MemSizeHalf * 2).ToArray();
            PointerA = MemSizeHalf;
            PointerB = MemSizeHalf;
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

                        //output command
                        if (debug)
                        {
                            Console.WriteLine("----------------");
                            Console.WriteLine(" Index" + "    " + "          SourceCode");
                            Console.WriteLine("");
                            Console.WriteLine(LongToString(Index - 1, 6) + " :  " + ArrayToString(Source, Index - 1));
                            Console.WriteLine(LongToString(Index, 6) + " :* " + ArrayToString(Source, Index));
                            Console.WriteLine(LongToString(Index + 1, 6) + " :* " + ArrayToString(Source, Index + 1));
                            Console.WriteLine(LongToString(Index + 2, 6) + " :  " + ArrayToString(Source, Index + 2));
                        }

                        //do command
                        cmd = cmd % Mod;
                        cmd = cmd + (Source[Index + 1] % Mod);
                        cmd = cmd % Mod;

                        if (debug)
                        {
                            Console.WriteLine("--------");
                            Console.WriteLine("Command : " + cmd);
                        }

                        switch (cmd)
                        {
                            case 0:
                                PointerA++;
                                if (debug) Console.WriteLine("PointerA : " + (PointerA - MemSizeHalf));
                                break;
                            case 1:
                                PointerB++;
                                if (debug) Console.WriteLine("PointerB : " + (PointerB - MemSizeHalf));
                                break;
                            case 2:
                                PointerA--;
                                if (debug) Console.WriteLine("PointerA : " + (PointerA - MemSizeHalf));
                                break;
                            case 3:
                                PointerB--;
                                if (debug) Console.WriteLine("PointerB : " + (PointerB - MemSizeHalf));
                                break;
                            case 4:
                                Memory[PointerA]++;
                                if (debug) Console.WriteLine("(*PointerA) : " + Memory[PointerA]);
                                break;
                            case 5:
                                Memory[PointerB]++;
                                if (debug) Console.WriteLine("(*PointerB) : " + Memory[PointerB]);
                                break;
                            case 6:
                                Memory[PointerA]--;
                                if (debug) Console.WriteLine("(*PointerA) : " + Memory[PointerA]);
                                break;
                            case 7:
                                Memory[PointerB]--;
                                if (debug) Console.WriteLine("(*PointerB) : " + Memory[PointerB]);
                                break;
                            case 8:
                                if (debug)
                                {
                                    Console.Write("Output : " + debOut);
                                    debOut = debOut + ((char)Memory[PointerA]).ToString();
                                }
                                Console.Write((char)Memory[PointerA]);
                                if (debug) Console.WriteLine();
                                break;
                            case 9:
                                if (debug)
                                {
                                    Console.Write("Output : " + debOut);
                                    debOut = debOut + ((char)Memory[PointerB]).ToString();
                                }
                                Console.Write((char)Memory[PointerB]);
                                if (debug) Console.WriteLine();
                                break;
                            case 10:
                                if (debug) Console.Write("Load to (*PointerA) : ");
                                Memory[PointerA] = Console.Read();
                                break;
                            case 11:
                                if (debug) Console.Write("Load to (*PointerB) : ");
                                Memory[PointerB] = Console.Read();
                                break;
                            case 12:
                                Source[Index + Memory[PointerA]] += Memory[PointerB];
                                if (debug) Console.WriteLine("SourceCode Line " + (Index + Memory[PointerA]) + " : " + Source[Index + Memory[PointerA]] + " (" + (Memory[PointerB] < 0 ? "" : "+") + Memory[PointerB] + ")");
                                break;
                            case 13:
                                Source[Index + Memory[PointerA]] -= Memory[PointerB];
                                if (debug) Console.WriteLine("SourceCode Line " + (Index + Memory[PointerA]) + " : " + Source[Index + Memory[PointerA]] + " (" + (Memory[PointerB] > 0 ? "" : "+") + (-Memory[PointerB]) + ")");
                                break;
                            case 14:
                                PointerA += (Memory[PointerB] == 0 ? 1 : 0);
                                if (debug) Console.WriteLine("(*PointerB) = " + Memory[PointerB] + " -> PointerA : " + (PointerA - MemSizeHalf));
                                break;
                            case 15:
                                PointerA -= (Memory[PointerB] == 0 ? 1 : 0);
                                if (debug) Console.WriteLine("(*PointerB) = " + Memory[PointerB] + " -> PointerA : " + (PointerA - MemSizeHalf));
                                break;
                            default:
                                break;
                        }

                        if (debug)
                        {
                            Console.ReadLine();
                        }
                    }
                }

            } while (!isSorted);
            //Console.WriteLine();
            //Console.ReadLine();
        }

        private static string ArrayToString(long[] array, int ind)
        {
            if(ind < 0 || ind >= array.Length)
            {
                return "";
            }
            else
            {
                string output = LongToString(array[ind], 20);
                return output;
            }
        }

        private static string LongToString(long num, int keta)
        {
            string output = num.ToString("D");
            for (; output.Length < keta; output = " " + output) ;
            return output;
        }
    }
}
