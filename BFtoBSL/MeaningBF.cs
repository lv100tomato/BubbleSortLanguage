using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BFtoBSL
{
    class MeaningBF
    {
        public bool isSource { get; }
        public int id { get; }
        public List<MeaningBF> mBF { get; }

        public MeaningBF(int _id)
        {
            isSource = true;
            id = _id;   //commands
            mBF = null;
        }
        public MeaningBF(List<MeaningBF> _mBF, int _id)
        {
            isSource = false;
            id = _id;   //depth of nest
            mBF = _mBF;
        }
    }
}
