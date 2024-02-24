using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtyalMemory
{
    public static class ProgramConst
    {
        /// <summary>
        /// Количество страниц в оперативной памяти.
        /// </summary>
        public const int COUNT_ITEMS_PAGE_BUFFER = 3;

        /// <summary>
        /// Количество страниц в файле подкачки.
        /// </summary>
        public const int COUNT_ITEMS_PAGE_SWAP_BINNARY_FILE = 20;

        /// <summary>
        /// Количество элементов на 1 странице.
        /// </summary>
        public const int COUNT_ELEMENT_ARRAY = 128;

        /// <summary>
        /// Путь к файлу подкачки.
        /// </summary>
        public const string PATH_SWAP_BINNARY_FILE = "file.bin";

        /// <summary>
        /// Сигнатура Virtual Memory.
        /// </summary>
        public const string SIGNATURE = "VM";
      
    }

}
