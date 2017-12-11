using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode {
    class Day10 : IDay {
        public string Name => "--- Day 10: Knot Hash ---";

        void Run() {
            int[] i255 = new int[256];
            int steps = 64;
            Console.WriteLine("Number of steps: 64:");
            //if (!int.TryParse(Console.ReadLine(), out steps)) {
            //    steps = 64;
            //}

            for (int i = 0; i < i255.Length; i++) { i255[i] = i; }
            string[] examples = new string[] {
                "",
                "AoC 2017",
                "1,2,3",
                "1,2,4",
            };
            foreach (string ex in examples) {
                Console.Write("\nHash of \"{0}\": {1}", ex, KnotHash(i255, ex, steps));
            }


            Console.WriteLine("\n\nHashing test data..");
            string hash = KnotHash(i255, TestData.DATA10, steps);
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

        string KnotHash(int[] input, string asciiLengths, int steps) {
            int[] lengths = ParseLength(asciiLengths.Trim(' '));
            int currentPositionIndex = 0;
            int skipSize = 0;
            for (int step = 0; step < steps; step++) {
                for (int currentLengthIndex = 0; currentLengthIndex < lengths.Length; skipSize++) {
                    ReverseRange(ref input, currentPositionIndex % input.Length, lengths[currentLengthIndex]);
                    currentPositionIndex += lengths[currentLengthIndex] + skipSize;
                    currentLengthIndex++;
                }
            }
            int[] denseHash = new int[16];
            for (int i = 0; i < denseHash.Length; i++) {
                denseHash[i] = input[i * denseHash.Length];
                for (int j = 1; j < denseHash.Length; j++) {
                    denseHash[i] ^= input[i * denseHash.Length + j];
                }
            }
            StringBuilder hash = new StringBuilder();
            foreach (int h in denseHash) {
                hash.AppendFormat("{0:x2}", h);
            }
            return hash.ToString();
        }

        readonly int[] salt = new int[] { 17, 31, 73, 47, 23 };

        int[] ParseLength(string ascii) {
            int[] result = new int[ascii.Length];
            for (int i = 0; i < result.Length; i++) {
                result[i] = (int)ascii[i];
            }
            int idx0 = result.Length;
            Array.Resize(ref result, result.Length + salt.Length);
            salt.CopyTo(result, idx0);
            return result;
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
