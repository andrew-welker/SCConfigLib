using System;
using System.Linq;
using Crestron.SimplSharp;

namespace SCConfigSplus.JSON
{
    public static class SPlusHelpers
    {
        public static ushort GetCurrentSourceCount(Source[] sources)
        {
            // Have to subtract 2 because S+ arrays are 1-based...
            // And I can't resize the array to 0 elements, so what corresponds to element 1 is actually element 3. 
            //BUT they still have a index 0 element, EVEN THOUGH that's not accessible from S+.
            var count = sources.Count() - 2;

            return (ushort)count;
        }

        public static void RemoveSource(ushort index, ref Source[] sources)
        {
            //Skipping 2 due to S+

            var list = sources.ToList();

            try
            {
                list.RemoveAt(index + 1);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                ErrorLog.Exception(String.Format("Exception removing element at index {0}", index - 1), ex);
            }

            sources = list.ToArray();
        }

        public static void AddEmptySource(ref Source[] sources)
        {
            var list = sources.ToList();

            list.Add(new Source());

            sources = list.ToArray();
        }
    }
}