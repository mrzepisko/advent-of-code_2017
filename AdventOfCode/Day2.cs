using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode {
    public class Day2 : IDay {
        public string Name => "--- Day 2: Corruption Checksum ---";

        void Run() {
            string example = "5\t1\t9\t5\n7\t5\t3\n2\t4\t6\t8";
            Console.WriteLine(string.Format("\tExample data checksum: {0}", CalculateChecksum(example).regular));
            Console.WriteLine(string.Format("\tTest data checksum: {0}", CalculateChecksum(Advent.DATA2).regular));
        }

        Result CalculateChecksum(string input) {
            Result result = new Result();
            if (input.Length == 0) return default(Result);
            string[] rows = input.Split('\n');
            for (int r = 0; r < rows.Length; r++) {
                string[] cells = rows[r].Split('\t');
                int min = int.MaxValue, max = int.MinValue;
                for (int i = 0; i < cells.Length; i++) {
                    min = Math.Min(min, int.Parse(cells[i]));
                    max = Math.Max(max, int.Parse(cells[i]));
                }
                result.regular += max - min;
            }
            return result;
        }

        public void Print() {
            Run();
        }

        struct Result {
            public int regular, divides; 
        }
    }
}
