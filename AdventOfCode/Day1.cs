using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace AdventOfCode {
    /// <summary>
    /// --- Day 1: Inverse Captcha ---
    /// </summary>
    public class Day1 {

        public static void Run() {
            PartOne();
        }

        static void PartOne() {
            Console.WriteLine("\t-- Part one --");
            string example1 = "1122";
            string example2 = "1111";
            string example3 = "1234";
            string example4 = "91212129";

            foreach (string input in new string[] { example1, example2, example3, example4 }) {
                Console.WriteLine(string.Format("\tExample data: {0}; result: {1}", input, InverseCaptcha(input)));
            }

            Console.WriteLine(string.Format("\n\tTest data: {0}...; result: {1}", Advent.DATA1.Substring(0, 5), InverseCaptcha(Advent.DATA1)));
        }
        
        public static int InverseCaptcha(string input) {
            int result = 0;
            for (int i = 0; i < input.Length; i++) {
                if (input[i].Equals(input[(i + 1) % input.Length])) {
                    result += input[i] - '0'; //ascii to int
                }
            }
            return result;
        }
    }
}
