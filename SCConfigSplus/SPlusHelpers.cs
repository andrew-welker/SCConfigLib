using System;
using System.Linq;
using Crestron.SimplSharp;
using SCConfigSplus.JSON;

namespace SCConfigSplus
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
    }
}