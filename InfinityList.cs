using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BubbleSortLanguage
{
    class InfinityList
    {
        private List<int> posList;
        private List<int> negList;
        private int initilal = 0;

        public InfinityList()
        {
            posList = new List<int>();
            negList = new List<int>();
        }

        public InfinityList(int def) : this()
        {
            initilal = def;
        }

        public int this[int x]
        {
            get
            {
                if(x < 0)
                {
                    int ind = - 1 - x;

                    for(int i= negList.Count; i < ind + 1; ++i)
                    {
                        negList.Add(initilal);
                    }

                    return negList[ind];
                }
                else
                {
                    int ind = x;

                    for (int i = posList.Count; i < ind + 1; ++i)
                    {
                        posList.Add(initilal);
                    }

                    return posList[ind];
                }
            }

            set
            {
                if (x < 0)
                {
                    int ind = - 1 - x;

                    for (int i = negList.Count; i < ind + 1; ++i)
                    {
                        negList.Add(initilal);
                    }

                    negList[ind] = value;
                }
                else
                {
                    int ind = x;

                    for (int i = posList.Count; i < ind + 1; ++i)
                    {
                        posList.Add(initilal);
                    }

                    posList[ind] = value;
                }
            }
        }
    }

}
