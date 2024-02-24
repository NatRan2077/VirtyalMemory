using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtyalMemory
{
    public class BinFileRepository : IVirtualMemmoryRepository
    {
        private FileInfo p_file;

        public BinFileRepository(FileInfo file)
        {
            this.p_file = file;
        }

        public void WriteSignature()
        {
            BinaryWriter writer = new BinaryWriter(new FileStream(p_file.FullName, FileMode.OpenOrCreate));
            try
            {
                writer.Write(ProgramConst.SIGNATURE.ToCharArray());
            }
            catch (OutOfMemoryException)
            {
                Console.Error.WriteLine("Недостаточно места на диске для размещения файла!");
            }
            writer.Close();

        }
        public void WritePage(Page page)
        {
            BinaryWriter writer = new BinaryWriter(new FileStream(p_file.FullName, FileMode.OpenOrCreate));
            try
            {
                writer.BaseStream.Seek(GetSeek(page.index), SeekOrigin.Begin);

                writer.Write(page.bitMap);
                foreach (int value in page.arrayPage)
                    writer.Write(value);
            }
            catch (OutOfMemoryException)
            {
                Console.Error.WriteLine("Недостаточно места на диске для размещения файла!");
            }
            writer.Close();

        }
        public Page? ReadPage(uint index)
        {
            BinaryReader reader = new BinaryReader(new FileStream(p_file.FullName, FileMode.Open));
            Page? page = new Page(index, false, DateTime.Now);

            reader.BaseStream.Seek(GetSeek(page.index), SeekOrigin.Begin);

            try
            {
                page.bitMap = reader.ReadBytes(ProgramConst.COUNT_ELEMENT_ARRAY / 8);

                for (int i = 0; i < ProgramConst.COUNT_ELEMENT_ARRAY - 1; i++)
                    page.arrayPage[i] = reader.ReadInt32();
            }
            catch (System.IO.EndOfStreamException)
            {
                page = null;
            }
            reader.Close();
            return page;
        }

        private int GetSeek(uint index)
        {
            var weightPage = ProgramConst.COUNT_ELEMENT_ARRAY * sizeof(int);
            var weightBitMap = ProgramConst.COUNT_ELEMENT_ARRAY / 8; // 1 byte = 8 elements.
            return (weightPage + weightBitMap) * (int)index + ProgramConst.SIGNATURE.Count();
        }

    }

}
