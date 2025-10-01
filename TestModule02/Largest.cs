using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestModule02
{
    public class LargestFinder
    {
        public int Largest(int[] list)
        {
            if (list == null)
                throw new NullReferenceException("Array is null");

            if (list.Length == 0)
                throw new ArgumentException("Array is empty");

            int num = list[0]; // Khởi tạo bằng phần tử đầu tiên
            for (int i = 1; i < list.Length; i++)
            {
                if (list[i] > num)
                {
                    num = list[i];
                }
            }

            return num;
        }
    }
}