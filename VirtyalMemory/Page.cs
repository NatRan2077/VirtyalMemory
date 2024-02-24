using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtyalMemory
{
    public class Page
    {
        public uint index { get; set; }
        public int[] arrayPage { get; set; }
        public byte[] bitMap { get; set; }
        public bool flagModification { get; set; }
        public DateTime dateUpdate { get; set; }

        public Page(uint index, bool flagModification, DateTime dateUpdate)
        {
            this.index = index;
            bitMap = new byte[ProgramConst.COUNT_ELEMENT_ARRAY / 8];
            arrayPage = new int[ProgramConst.COUNT_ELEMENT_ARRAY];
            this.flagModification = flagModification;
            this.dateUpdate = dateUpdate;
        }

    }
}
