using System;
using System.Linq;
using Crestron.SimplSharp;
using SCConfigSPlus.JSON;

namespace SCConfigSPlus
{
    public static class SPlusHelpers
    {
        /// <summary>
        /// Method to get the count of an S+ array, allowing for the 0 index and skipping index 1
        /// </summary>
        /// <param name="sources">Array to get count from</param>
        /// <returns>Number of elements in the array</returns>
        public static ushort GetCurrentSourceCount(Source[] sources)
        {
            // Have to subtract 2 because S+ arrays are 1-based...
            // And I can't resize the array to 0 elements, so what corresponds to element 1 is actually element 3. 
            //BUT they still have a index 0 element, EVEN THOUGH that's not accessible from S+.
            var arrayToCount = sources.Skip(2);

            var count = arrayToCount.Count();

            return (ushort)count;
        }

        /// <summary>
        /// Method to remove an entry from an S+ array
        /// </summary>
        /// <param name="index">index of entry to remove</param>
        /// <param name="sources">Array to remove from</param>
        public static void RemoveSource(ushort index, ref Source[] sources)
        {
            //Adding 1 to index because of S+ being 1-based.

            var list = sources.ToList();

            try
            {
                list.RemoveAt(index + 1);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                ErrorLog.Exception(String.Format("Exception removing element at index {0}", index + 1), ex);
            }

            sources = list.ToArray();
        }

        /// <summary>
        /// Method to add an empty entry to an S+ array
        /// </summary>
        /// <param name="sources">Array to add entry to</param>
        public static void AddEmptySource(ref Source[] sources)
        {
            var list = sources.ToList();

            list.Add(new Source());

            sources = list.ToArray();
        }

        /// <summary>
        /// Method to add an existing source to the sources array
        /// </summary>
        /// <param name="sources">Array to add item to</param>
        /// <param name="sourceToAdd">Item to add</param>
        public static void AddNewSource(ref Source[] sources, Source sourceToAdd)
        {
            var list = sources.ToList();

            if (list.Contains(sourceToAdd))
            {
                return;
            }

            list.Add(sourceToAdd);

            sources = list.ToArray();
        }

        /// <summary>
        /// Method to edit S+ array for ease of display. Removes index 1 from the array
        /// </summary>
        /// <param name="sources"></param>
        public static void GetArrayForDisplay(ref Source[] sources)
        {
            var list = sources.ToList();

            list.RemoveAt(1);

            sources = list.ToArray();
        }

        /// <summary>
        /// Method to copy an S+ array
        /// </summary>
        /// <param name="source">Array to copy from</param>
        /// <param name="destination">Array to copy to</param>
        public static void CopyArray(Source[] source, ref Source[] destination)
        {
            var list = source.ToList();

            destination = list.ToArray();
        }
    }
}