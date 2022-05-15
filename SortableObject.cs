using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingLab
{
    class SortableObject : IComparable
    {
        public int key;
        public double[] Data;
        private static int sizeofObject = 1500;
        public SortableObject(int targetValue)
        {
            key = targetValue;
            Data = new double[sizeofObject];
            for (int i=0; i<sizeofObject; i++)
            {
                Data[i] = SriRandom.GetRandomDouble();
            }
        }
        public int CompareTo(object obj)
        {
            SortableObject b = obj as SortableObject;
            if (obj == null) return 1;
            //A more elegant way to implement the next five lines is:
            // return this.value.CompareTo(b.value);
            if (key < b.key)
                return -1;
            if (key > b.key)
                return 1;
            else return 0;
        }
    }
}
