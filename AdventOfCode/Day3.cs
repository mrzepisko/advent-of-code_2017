using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode {
    class Day3 : IDay {
        public string Name => "--- Day 3: Spiral Memory ---";

        public void Print() {
            Run();
        }

        void Run() {
            int[] examples = new int[] {
                1, 12, 23, 1024,
            };

            foreach (int i in examples) {
                Console.WriteLine(string.Format("Example data: {1}; Distance: {0}", FindDistance(i), i));
            }

            Console.WriteLine(string.Format("Test data: {0}; Distance: {1}", Advent.DATA3, FindDistance(int.Parse(Advent.DATA3))));
        }

        /// <summary>
        /// Close enough but varies vy +/- 1
        /// </summary>
        int FindDistance(int value) {

            int evenOddCoefficient = 0; //TODO

            int n = (int)Math.Ceiling(Math.Sqrt(value));
            int m = n - 1;

            int[] candidates = new int[] {
                n * n,
                m * m,
                n * (n + 1),
                m * (m + 1),
            };

            Array.Sort(candidates);

            int next = candidates.Max(), prev = candidates.Min();
            for (int i = 0; i < candidates.Length; i++) {
                int c = candidates[i];
            }
            foreach (int c in candidates) {
                prev = c > prev && c < value ? c : prev;
                next = c < next && c >= value ? c : next;
            }

            int diff = next - prev;
            int h = diff / 2;
            int w = Math.Abs(prev + h - value);
            int s = h + w;

            return s + evenOddCoefficient;
        }
    }
}
