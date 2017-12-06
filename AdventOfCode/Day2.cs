using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode {
    public class Day2 : IDay {
        public string Name => "--- Day 2: Corruption Checksum ---";

        void Run() {
            string example = "5\t1\t9\t5\n7\t5\t3\n2\t4\t6\t8";
            Console.WriteLine("-- Part one --");
            Console.WriteLine(string.Format("\tExample data checksum: {0}", CalculateChecksum(example).regular));
            Console.WriteLine(string.Format("\tTest data checksum: {0}", CalculateChecksum(Advent.DATA2).regular));

            string example2 = "5\t9\t2\t8\n9\t4\t7\t3\n3\t8\t6\t5";
            Console.WriteLine("-- Part two --");
            Console.WriteLine(string.Format("\tExample data checksum: {0}", CalculateChecksum(example2).divisible));
            Console.WriteLine(string.Format("\tTest data checksum: {0}", CalculateChecksum(Advent.DATA2).divisible));
        }

        Result CalculateChecksum(string input) {
            Result result = new Result();
            if (input.Length == 0) return default(Result);
            string[] rows = input.Split('\n');
            for (int r = 0; r < rows.Length; r++) {
                string[] cells = rows[r].Split('\t');
                int[] row = new int[cells.Length];
                int min = int.MaxValue, max = int.MinValue;
                for (int i = 0; i < cells.Length; i++) {
                    row[i] = int.Parse(cells[i]);
                    min = Math.Min(min, row[i]);
                    max = Math.Max(max, row[i]);
                }
                result.regular += max - min;
                result.divisible += FindDivisible(row);
            }
            return result;
        }

        int FindDivisible(int[] row) {
            for (int i = 0; i < row.Length - 1; i++) {
                for (int j = i + 1; j < row.Length; j++) {
                    int a = row[i], b = row[j];
                    if (a % b == 0) {
                        return a / b;
                    } else if (b % a == 0) {
                        return b / a;
                    }
                }
            }
            return 0;
        }

        public void Print() {
            Run();
        }

        struct Result {
            public int regular, divisible;
        }
    }
}
