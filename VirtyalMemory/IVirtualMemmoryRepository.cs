using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtyalMemory
{
    public interface IVirtualMemmoryRepository
    {
        void WritePage(Page page);
        void WriteSignature();
        Page? ReadPage(uint index);
    }

}
