using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BFtoBSL
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Error : Please enter the file name.");
                return;
            }

            //pick up pure BrainF**k
            StreamReader sr = new StreamReader(args[0]);
            string rawBF = sr.ReadToEnd();
            string bf = "";
            int nest = 0;
            int max_nest = 0;

            for(int i = 0; i < rawBF.Length; ++i)
            {
                switch (rawBF[i])
                {
                    case '>':
                    case '<':
                    case '+':
                    case '-':
                    case '.':
                    case ',':
                        bf = bf + rawBF[i].ToString();
                        break;
                    case '[':
                        bf = bf + rawBF[i].ToString();
                        ++nest;
                        if (max_nest < nest) max_nest = nest;
                        break;
                    case ']':
                        bf = bf + rawBF[i].ToString();
                        --nest;
                        break;
                    default:
                        break;
                }

                if(nest < 0)
                {
                    Console.WriteLine("Error : Invalid source code");
                    return;
                }

            }

            if(nest != 0)
            {
                Console.WriteLine("Error : Invalid source code");
                return;
            }

            //Console.WriteLine(bf);

            //convert by nest
            List<MeaningBF> listMBF = new List<MeaningBF>();
            List<List<MeaningBF>> listlistMBF = new List<List<MeaningBF>>();
            nest = 0;
            for(int i = 0; i < bf.Length; ++i)
            {
                switch (bf[i])
                {
                    case '>':
                        listMBF.Add(new MeaningBF(0));
                        break;
                    case '<':
                        listMBF.Add(new MeaningBF(1));
                        break;
                    case '+':
                        listMBF.Add(new MeaningBF(2));
                        break;
                    case '-':
                        listMBF.Add(new MeaningBF(3));
                        break;
                    case '.':
                        listMBF.Add(new MeaningBF(4));
                        break;
                    case ',':
                        listMBF.Add(new MeaningBF(5));
                        break;
                    case '[':
                        listlistMBF.Add(listMBF);
                        listMBF = new List<MeaningBF>();
                        ++nest;
                        break;
                    case ']':
                        --nest;
                        listlistMBF[listlistMBF.Count - 1].Add(new MeaningBF(listMBF, max_nest - nest));
                        listMBF = listlistMBF[listlistMBF.Count - 1];
                        listlistMBF.RemoveAt(listlistMBF.Count - 1);
                        break;
                    default:
                        break;
                }
            }

            if(nest != 0)
            {
                Console.WriteLine("Internal Error");
                return;
            }

            //showMBF(listMBF);

            //convert to command

            //...[...]...
            //hogehoge{0}::if(*pt==0)then{0->32}::reflect{-16->0}::hogehoge{0}::if(*pt!=0)then{0->-16}::back{32->0}::hogehoge{0}

            /*                    *
             * ptB = ... 0 : 32 : 0 : -16+50*n : 0 ... 
             * ptA1 = MEM
             * ptA2 = ... 0 : 0 : 0 : ...
             * --------
             * 
             * ...
             * 
             * {  0} hogehoge
             * 
             * {  0} 15*3   *ptA1==0 -> ptB--
             * {  0} 0      ptA1->ptA2
             * {  0} 4      (*ptA2)++
             * {  0} 12     index+(*ptA2) += *ptB
             * { 32} 35*3   ptB--
             * {  0} 6      (*ptA2)--
             * {  0} 2      ptA2->ptA1
             * 
             * {  0} 0      ptA1->ptA2
             * {  0} 1*3    ptB++
             * {-16} 20     (*ptA2)++
             * {-16} 28     index+(*ptA2) += *ptB
             * {-16} 19*3   ptB--
             * {  0} 3*3    ptB--
             * {  0} 2      ptA2->ptA1
             * 
             * {  0} hogehoge
             * 
             * {-16} 22     (*ptA2)--
             * {-16} 17*3   ptB++
             * {  0} 15*3   *ptA1==0 -> ptB--
             * {  0} 1*3    ptB++
             * {  0} 0      ptA1->ptA2
             * {  0} 4      (*ptA2)++
             * {  0} 13     index+(*ptA2) -= *ptB
             * {  0} 6      (*ptA2)--
             * {  0} 2      ptA2->ptA1
             * 
             * {  0} 0      ptA1->ptA2
             * {  0} 4      (*ptA2)++
             * {  0} 3*3    ptB--
             * { 32} 33*3   ptB++
             * { 32} 45     index+(*ptA2) -= *ptB
             * {  0} 1*3    ptB++
             * {  0} 6      (*ptA2)--
             * {  0} 2      ptA2->ptA1
             * 
             * {  0} hogehoge
             * 
             * ...
             * 
             */

            //List<int> comList = toCommand(listMBF);

            List<int> comList = new List<int>();
            comList.Add(0);
            for (int i = 0; i < bf.Length; ++i)
            {
                switch (bf[i])
                {
                    case '>':
                        comList.Add(0);
                        comList.Add(0);
                        comList.Add(0);
                        break;
                    case '<':
                        comList.Add(2);
                        comList.Add(2);
                        comList.Add(2);
                        break;
                    case '+':
                        comList.Add(4);
                        break;
                    case '-':
                        comList.Add(6);
                        break;
                    case '.':
                        comList.Add(8);
                        break;
                    case ',':
                        comList.Add(10);
                        break;
                    case '[':
                        comList.Add(15);
                        comList.Add(15);
                        comList.Add(15);
                        comList.Add(0);
                        comList.Add(4);
                        comList.Add(12);
                        comList.Add(35);
                        comList.Add(35);
                        comList.Add(35);
                        comList.Add(6);
                        comList.Add(2);
                        comList.Add(0);
                        comList.Add(1);
                        comList.Add(1);
                        comList.Add(1);
                        comList.Add(20);
                        comList.Add(28);
                        comList.Add(19);
                        comList.Add(19);
                        comList.Add(19);
                        comList.Add(3);
                        comList.Add(3);
                        comList.Add(3);
                        comList.Add(2);
                        break;
                    case ']':
                        comList.Add(22);
                        comList.Add(17);
                        comList.Add(17);
                        comList.Add(17);
                        comList.Add(15);
                        comList.Add(15);
                        comList.Add(15);
                        comList.Add(1);
                        comList.Add(1);
                        comList.Add(1);
                        comList.Add(0);
                        comList.Add(4);
                        comList.Add(13);
                        comList.Add(6);
                        comList.Add(2);
                        comList.Add(0);
                        comList.Add(4);
                        comList.Add(3);
                        comList.Add(3);
                        comList.Add(3);
                        comList.Add(33);
                        comList.Add(33);
                        comList.Add(33);
                        comList.Add(45);
                        comList.Add(1);
                        comList.Add(1);
                        comList.Add(1);
                        comList.Add(6);
                        comList.Add(2);
                        break;
                    default:
                        break;
                }
            }

            int[] comArray = comList.ToArray();

            //long startMin = long.MinValue + 1000 - (long.MinValue % 1000);
            long startMin = 1000;

            long[] source = new long[comArray.Length];
            source[0] = startMin + comArray[0];

            for(int i = 1; i < comArray.Length; ++i)
            {
                if (comArray[i] < comArray[i - 1])
                {
                    startMin += 50;
                }
                source[i] = startMin + comArray[i];
            }

            long sMax = source[source.Length - 1] + 100 - (source[source.Length - 1] % 50);
            //long sMin = long.MinValue + 50 - (long.MinValue % 50) + 16;
            long sMin = 100;

            //file output
            string output = "";
            output += (sMax + "\n");
            output += ((3 + sMin) + "\n");
            output += ((3 + sMin) + "\n");
            output += ((3 + sMin) + "\n");
            for (int i = 0; i < 50 - 32; ++i)
            {
                output += ((5 + sMin) + "\n");
            }
            output += ((51 + sMin) + "\n");
            output += ((51 + sMin) + "\n");
            output += ((51 + sMin) + "\n");
            output += ((51 + sMin) + "\n");
            output += ((51 + sMin) + "\n");
            output += ((51 + sMin) + "\n");
            for (int i = 0; i < sMax - sMin - 50 + 16; ++i)
            {
                output += ((55 + sMin) + "\n");
            }
            output += ((103 + sMin) + "\n");
            output += ((103 + sMin) + "\n");
            output += ((103 + sMin) + "\n");
            for (int i = 0; i < source.Length; ++i)
            {
                output += (source[i] + "\n");
            }

            File.WriteAllText(args[0] + ".bsl", output);

            Console.WriteLine();
        }

        private static void showMBF(List<MeaningBF> LMBF)
        {
            for(int i = 0; i < LMBF.Count; ++i)
            {
                if (LMBF[i].isSource)
                {
                    Console.Write(LMBF[i].id);
                }
                else
                {
                    Console.Write("{(" + LMBF[i].id + ")");
                    showMBF(LMBF[i].mBF);
                    Console.Write("(" + LMBF[i].id + ")}");
                }
            }
        }

    }
}
