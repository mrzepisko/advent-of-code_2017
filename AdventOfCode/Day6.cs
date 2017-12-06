////////////////////////////////////////////////////////////////////////////////////////////////////////
//FileName: DBOperations.cs
//FileType: Visual C# Source file
//Size : 26206
//Author : Your Name
//Created On : 7/8/2015 9:56:39 AM
//Last Modified On : 8/9/2015 11:53:23 AM
//Copy Rights : your company
//Description : Class for defining database related functions
////////////////////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode {
    public class Day6 {
      static int[] blocks;
      public static void Run() {
            string input0 = "0	2	7	0";
            string input1 = "0	5	10	0	11	14	13	4	11	8	8	7	1	4	12	11";

            Console.WriteLine("\n\tExample data:");
            Console.WriteLine("\t" + Step(input0));
            Console.WriteLine("\n\tTest data:");
            Console.WriteLine("\t" + Step(input1));
        }

        static Result Step(string input) {
            InitMemory(input);
            return CheckLoop();
        }

        static void InitMemory(string s) {
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

        static Result CheckLoop() {
            List<string> steps = new List<string> {
                IntArrayToString(blocks)
            };
            int count = 0;
            for (;;) {
                count++;
                Cycle();
                string result = IntArrayToString(blocks);
                if (steps.Contains(result)) {
                    return new Result() { steps = count, distance = count - steps.IndexOf(result) };
                }

                steps.Add(result);
            }
        }
        

        static void Cycle() {
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

        static string IntArrayToString(int[] arr) {
            StringBuilder sb = new StringBuilder();
            sb.Append(arr[0]);
            for (int i = 1; i < arr.Length; i++) {
                sb.Append("\t").Append(arr[i]);
            }
            return sb.ToString();
        }

        struct Result {
            public int steps, distance;

            public override string ToString() {
                return string.Format("Steps: {0}, Distance: {1}", steps, distance);
            }
        }
    }
}
