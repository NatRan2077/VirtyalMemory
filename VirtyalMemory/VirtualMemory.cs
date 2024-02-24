using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtyalMemory
{
    public class VirtualMemmory
    {
        private Page[] p_pageBuff;
        private IVirtualMemmoryRepository p_repository;

        public VirtualMemmory(string filePath = ProgramConst.PATH_SWAP_BINNARY_FILE,
                              int countPageSwapFile = ProgramConst.COUNT_ITEMS_PAGE_SWAP_BINNARY_FILE)
        {
            FileInfo file = new FileInfo(filePath);
            p_repository = new BinFileRepository(new FileInfo(filePath));
            p_pageBuff = new Page[ProgramConst.COUNT_ITEMS_PAGE_BUFFER];

            if (!File.Exists(file.FullName))
            {
                p_repository.WriteSignature();
                for (uint index = 0; index < countPageSwapFile; index++)
                    p_repository.WritePage(new Page(index, false, new DateTime()));
            }

            for (uint index = 0; index < ProgramConst.COUNT_ITEMS_PAGE_BUFFER; index++)
                p_pageBuff[index] = p_repository.ReadPage(index);
        }

        private uint? FindElement(uint index)
        {
            uint indexPage = index / ProgramConst.COUNT_ELEMENT_ARRAY;
            var newPage = p_repository.ReadPage(indexPage);

            if (newPage == null)
                return null;

            for (uint indexPageBuff = 0; indexPageBuff < p_pageBuff.Count(); indexPageBuff++)
                if (p_pageBuff[indexPageBuff].index == indexPage)
                    return indexPageBuff;

            var oldestPage = p_pageBuff.OrderBy(ob => ob.dateUpdate).ToArray().First();
            if (oldestPage.flagModification)
                p_repository.WritePage(oldestPage);

            p_pageBuff[Array.IndexOf(p_pageBuff, oldestPage)] = newPage;

            return (uint)Array.IndexOf(p_pageBuff, newPage);
        }

        public bool CopyValueToVariable(uint index, ref int variable)
        {
            uint? indexPageBuff = FindElement(index);
            if (indexPageBuff == null)
            {
                Console.Error.WriteLine($"Элемента с индексом - {index} нет в массиве!");
                return false;
            }

            var localIndex = GetLocalIndexPage(index, (uint)indexPageBuff);
            ref Page page = ref p_pageBuff[(uint)indexPageBuff];
            if ((page.bitMap[localIndex / 8] & (1 << (byte)(7 - (localIndex % 8)))) == 0)
            {
                Console.Error.WriteLine($"Элемент {index} не инициализирован!");
                return false;
            }
            variable = p_pageBuff[(uint)indexPageBuff].arrayPage[GetLocalIndexPage(index, (uint)indexPageBuff)];
            return true;
        }
        public int? this[uint index]
        {
            get
            {
                int result = 0;
                if (CopyValueToVariable(index, ref result))
                    return result;
                return null;
            }
            set
            {
                uint? indexPageBuff = FindElement(index);
                if (indexPageBuff != null)
                {
                    ref Page page = ref p_pageBuff[(uint)indexPageBuff];
                    var localIndex = GetLocalIndexPage(index, (uint)indexPageBuff);

                    page.arrayPage[localIndex] = (int)value;
                    page.bitMap[localIndex / 8] = Convert.ToByte(page.bitMap[localIndex / 8] | (1 << (byte)(7 - (localIndex % 8))));
                    page.flagModification = true;
                    page.dateUpdate = DateTime.Now;
                }
                else
                {
                    Console.Error.WriteLine($"Невозможно записать в элемент с индексом - {index} т.к его нет в массиве!");
                }
            }

        }


        /// <summary>
        ///  Вычисляет страничный адрес элемента массива с заданным индексом.
        /// </summary>
        /// <param name="index">Индекс страницы.</param>
        /// <param name="indexPageBuff"> Индекс страницы в оперативной памяти.</param>
        /// <returns>Cтраничный адрес элемента массива с заданным индексом.</returns>
        private uint GetLocalIndexPage(uint index, uint indexPageBuff)
            => index - (ProgramConst.COUNT_ELEMENT_ARRAY * p_pageBuff[indexPageBuff].index);

    }

}
