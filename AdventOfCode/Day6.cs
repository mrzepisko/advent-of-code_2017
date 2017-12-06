using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode {
    /// <summary>
    /// Advent of Code 2017 - Day 6: Memory Reallocation ---
    /// </summary>
    public class Day6 : IDay {
        int[] blocks;

        public string Name => "--- Day 6: Memory Reallocation ---";

        void Run() {
            string input0 = "0	2	7	0";
            string input1 = Advent.DATA6;

            Console.WriteLine("\n\tExample data:");
            Console.WriteLine("\t" + Step(input0));
            Console.WriteLine("\n\tTest data:");
            Console.WriteLine("\t" + Step(input1));
        }

        Result Step(string input) {
            InitMemory(input);
            return CheckLoop();
        }

        void InitMemory(string s) {
            string[] split = s.Split('\t');
            if (blocks == null) {
                blocks = new int[split.Length];
            }

            if (blocks.Length != split.Length) {
                System.Array.Resize(ref blocks, split.Length);
            }

            for (int i = 0; i < blocks.Length; i++) {
                blocks[i] = int.Parse(split[i]);
            }
        }

        Result CheckLoop() {
            List<string> steps = new List<string> {
                IntArrayToString(blocks)
            };
            int count = 0;
            for (; ; ) {
                count++;
                Cycle();
                string result = IntArrayToString(blocks);
                if (steps.Contains(result)) {
                    return new Result() { steps = count, distance = count - steps.IndexOf(result) };
                }

                steps.Add(result);
            }
        }


        void Cycle() {
            int max = blocks.Max();
            int maxIndex = Array.IndexOf(blocks, max);

            blocks[maxIndex] = 0;

            int addAll = max / blocks.Length;

            for (int i = 0; i < blocks.Length; i++) {
                blocks[i] += addAll;
            }

            for (int i = 1; i <= max % blocks.Length; i++) {
                blocks[(maxIndex + i) % blocks.Length]++;
            }
        }

        string IntArrayToString(int[] arr) {
            StringBuilder sb = new StringBuilder();
            sb.Append(arr[0]);
            for (int i = 1; i < arr.Length; i++) {
                sb.Append("\t").Append(arr[i]);
            }
            return sb.ToString();
        }

        public void Print() {
            Run();
        }

        struct Result {
            public int steps, distance;

            public override string ToString() {
                return string.Format("Steps: {0}, Distance: {1}", steps, distance);
            }
        }
    }
}
