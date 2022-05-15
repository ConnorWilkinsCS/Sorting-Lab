using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
using System.Globalization;

namespace SortingLab
{
    public static class Util // This used the built in C# RNG.  
    {
        private static Random rnd = new Random();
        public static int GetRandom()
        {
            return rnd.Next();
        }
    }
    public class SriRandom  // Switched to a faster XorShift RNG based on some code I pillaged from the web
    {
        private static XorShiftRandom sriRnd = new XorShiftRandom();

        public static int GetRandom()
        {
            return sriRnd.NextInt32();
        }
        public static double GetRandomDouble()
        {
            return sriRnd.NextDouble();
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            // Use 10 elements for testing, but then run with a few 10's of thousands of elements to see how long it takes.  Ideally try 50k.  
            int numElements = 50000;  // 
           

            SortableObject[] ArrayofSortables = new SortableObject[numElements];
            for (int i = 0; i < numElements; i++)
            {
                ArrayofSortables[i] = new SortableObject(Util.GetRandom()); // trust me and don't change this to the faster random
            }

            // This just shows you that the keys and the first data element are unique (ish).  
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(ArrayofSortables[i].key + " " + ArrayofSortables[i].Data[1]);
            }


            //Begin timing
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

			//comment this out when testing other sorting algorthms.
			//InsertionSort(ArrayofSortables);


            //Student code goes here.
            // Possible sorts
            //Bubble Sort
            //BubbleSort(ArrayofSortables);
            //Selection Sort
            //Shell Sort
            //QuickSort
            QuickSort(ArrayofSortables, 0, 49999);
            //MergeSort


            stopWatch.Stop();
            // Get the elapsed time as a TimeSpan value.
            TimeSpan ts = stopWatch.Elapsed;
            Console.WriteLine();
            Console.WriteLine("The first 10 elements in sorted order");
            Console.WriteLine("The Key is the left column right column is the first element of the actual data itself");
            Console.WriteLine();
            // Show the first 10 elements as being sorted, to prove that, well, it sorted correctly (the data part is irrelevant).  
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(ArrayofSortables[i].key.ToString("D10") + " " + ArrayofSortables[i].Data[1]);
            }
            // That goofy .key.ToSTring("D10") is what formats it with the leading zero's so all ints are the same length


            // Format and display the TimeSpan value.
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
            Console.WriteLine("RunTime " + elapsedTime);
            Console.ReadLine();

        }//end main
		
		static public void InsertionSort(SortableObject[] items)
		{
			//Sample from Sri for a simple Insertion Sort
            // On a fast computer the following takes about 1.0 seconds to run with 10 000 elements
            // on my machine at home 25k elements: 7 seconds 
            // my machine at home 50 000 elements is 30 seconds almost exactly 
            for (int i = 0; i < items.Length - 1; i++)
            {
                for (int j = i + 1; j > 0; j--)
                {
                    if (items[j - 1].CompareTo( items[j]) > 0)
                    {
                        SortableObject temp = items[j - 1];
                        items[j - 1] = items[j];
                        items[j] = temp;
                    }//end if
                }//end for j
            }//end for i
		}//end InsertionSort
		
		/*
		QuickSort()
		Complete QuickSort and Partition based om the comments provided
		Arguments:
		SortableObject[] items - the list to be sorted
		startIndex - the lowest index value that we are sorting (inclusive)
		endIndex - the highest index value (inclusive)
		*/
		static public void QuickSort(SortableObject[] items, int startIndex, int endIndex)
		{
            //if we have more than one item to sort
            if (startIndex < endIndex)
            {
                //partition the list
                 int pi = Partition(items, startIndex, endIndex);
                //recursivly call QuickSort on the list that is to the left of the partion index
                //recursively call QuickSort on the list that is to the right of the partion index
                QuickSort(items, startIndex, pi - 1);
                QuickSort(items, pi + 1, endIndex);
            }
        }//end QuickSort()
		
		/*
		Partion()
		Used with QuickSort()
		This partitions the list into two parts with a pivot value in the middle.  The index of the pivot point is returned.
		Everything that is less than the pivot is placed to the left of the partiton index. 
		Everything that is greater than the pivot is placed to right of the pivot index. 
		Note: the items to the left and right of the pivot are not sorted.  They are just all less than or greater
		than the pivot value.
		Arguments
		SortableObject[] items - the list to be sorted
		startIndex - the lowest index value that we are sorting (inclusive)
		endIndex - the highest index value (inclusive)
		
		*/
		static public int Partition(SortableObject[] items,  int startIndex, int endIndex)
		{
            //make the item at the end of the list our pivot value
            SortableObject pivot = items[endIndex];
            //declare a variable to hold the index of the last item that was placed to the left of the pivot. Call it lastLeftItem.
            //it will start at startIndex - 1 because there are none yet.
            int i = (startIndex - 1);
            //use a for loop to loop through each element in the items list from startIndex to endIndex inclusive
            for (int j = startIndex; j <= endIndex - 1; j++)
            {
                //inside the loop, compare the item at the current index with our pivot value.
                //If it is less than the pivot value, increment lastLeftItem and swap whatever is in items[lastLeftItem] with the value at the current index
                if (items[j].CompareTo(pivot) < 0)
                {
                    i++;    // increment index of smaller element
                    SortableObject temp = items[i];
                    items[i] = items[j];
                    items[j] = temp;
                }
            }
            //after the loop, we need to move pivot value so that it is between the items that are less than it and the items that are bigger than it.
            //we do this by swapping the value at index lastLeftItem + 1 with the value at index endIndex
            SortableObject temp1 = items[i + 1];
            items[i + 1] = items[endIndex];
            items[endIndex] = temp1;
            //return the pivot index

            return i + 1;
        }//end Partion()
        static void BubbleSort(SortableObject[] items)
        {
            int n = items.Length;
            for (int i = 0; i < n - 1; i++)
                for (int j = 0; j < n - i - 1; j++)
                    if (items[j].CompareTo(items[j + 1]) > 0)
                    {
                        // swap temp and arr[i] 
                        SortableObject temp = items[j];
                        items[j] = items[j + 1];
                        items[j + 1] = temp;
                    }
        } //end bubble sort
    }//end program
}//end namespace SortingLab



