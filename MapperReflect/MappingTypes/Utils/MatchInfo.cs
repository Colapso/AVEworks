using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapperReflect
{
    public class MatchInfo
    {
        public int SrcIdx { get; private set; }
        public int DstIdx { get; private set; }
        public IMapper MapperAux { get; set; } = null;

        public MatchInfo(int srcIdx, int dstIdx)
        {
            this.SrcIdx = srcIdx;
            this.DstIdx = dstIdx;
        }
    }
}
