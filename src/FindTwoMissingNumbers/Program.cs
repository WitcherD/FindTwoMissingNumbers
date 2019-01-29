using System;

namespace FindTwoMissingNumbers
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var n = 5;
            int[] array = { 1, 2, 3, 4 }; // 0 and 5
            var missing = FindTwoMissingNumbers(array, n);
            Console.WriteLine($"Two Missing Numbers are {missing[0]} and {missing[1]}");

            n = 5;
            array = new[] { 0, 2, 3, 5 }; // 1 and 4
            missing = FindTwoMissingNumbers(array, n);
            Console.WriteLine($"Two Missing Numbers are {missing[0]} and {missing[1]}");
        }

        /// <summary>
        /// Function to find two missing numbers in range [0, n].
        /// This function assumes that size of array is n-1 and all array elements are distinct.
        ///
        /// Solution is from here https://www.geeksforgeeks.org/find-two-missing-numbers-set-2-xor-based-solution/
        /// </summary>
        private static int[] FindTwoMissingNumbers(int[] array, int n)
        {
            n++;

            var xor = 0;

            // Get the XOR of all elements in {0, 1, 2 .. n} 
            for (var i = 1; i < n; i++)
                xor ^= i;

            // XOR of all elements in array 
            for (var i = 0; i < n - 2; i++)
                xor ^= array[i];

            // Now XOR has XOR of two missing element.
            // Any (ANY is important) set bit in it must be set in one missing and unset in other missing number.
            // Get a set bit of XOR (We get the rightmost set bit) 
            var setBit = xor & ~(xor - 1);

            // Now divide elements in two sets by comparing rightmost set bit of XOR
            // with bit at same position in each element. 
            int x = 0, y = 0;
            for (var i = 0; i < n - 2; i++)
            {
                if ((array[i] & setBit) > 0)
                    // XOR of first set in arr[] 
                    x = x ^ array[i];
                else
                    // XOR of second set in arr[] 
                    y = y ^ array[i];
            }

            for (var i = 0; i < n; i++)
            {
                if ((i & setBit) > 0)
                    // XOR of first set in arr[] and {0, 1, 2, ... n } 
                    x = x ^ i;
                else
                    // XOR of second set in arr[] and {0, 1, 2, ... n } 
                    y = y ^ i;
            }

            return new[] { x, y };
        }
    }
}
