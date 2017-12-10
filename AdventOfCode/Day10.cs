using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode {
    class Day10 : IDay {
        public string Name => "--- Day 10: Knot Hash ---";

        void Run() {

            int[] i255 = new int[256];
            for (int i = 0; i < i255.Length; i++) { i255[i] = i; }
            int hash = KnotHash(i255, TestData.DATA10.Split(',').Select(s => int.Parse(s)).ToArray());
            Console.Write("Puzzle output: {0}", hash);
#if false
            int[] test = new int[] { 1, 2, 3, 4, 5 };
            Console.WriteLine("Flip from (1), to (2):");
            int from, to;
            while (!int.TryParse(Console.ReadLine(), out from)) { Console.WriteLine("Flip from:"); }
            while (!int.TryParse(Console.ReadLine(), out to)) { Console.WriteLine("Flip to:"); }
            Console.Write("\nReverse: {0}", test);
            ReverseRange(ref test, from, to);
            Console.Write("\nReversed: {0}", test);
#endif

        }

        int KnotHash(int[] input, int[] lengths) {
            int currentPositionIndex = 0;
            int currentLengthIndex = 0;
            for (int skipSize = 0; currentLengthIndex < lengths.Length; skipSize++) {
                ReverseRange(ref input, currentPositionIndex, lengths[currentLengthIndex]);
                currentPositionIndex += lengths[currentLengthIndex] + skipSize;
                currentLengthIndex++;
            }
            return input[0] * input[1];
        }

        void ReverseRange<T>(ref T[] array, int index, int length) {
            if (length <= 1) return;
            T[] tmp = new T[length];
            for (int i = 0; i < tmp.Length; i++) {
                tmp[i % tmp.Length] = array[(index + i) % array.Length];
            }

            for (int i = 0; i < tmp.Length; i++) {
                array[(index + i) % array.Length] = tmp[tmp.Length - 1 - i];
            }
        }

        public void Print() {
            Run();
        }
    }
}
