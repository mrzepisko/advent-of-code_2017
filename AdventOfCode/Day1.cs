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
            PartTwo();
        }

        static void PartOne() {
            Console.WriteLine("\t-- Part one --");
            string[] example = new string[] { "1122", "1111", "1234", "91212129", };

            foreach (string input in example) {
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


        static void PartTwo() {
            Console.WriteLine("\n\n\t-- Part two --");
            string[] example = new string[] { "1212", "1221", "123425", "123123", "12131415", };

            foreach (string input in example) {
                Console.WriteLine(string.Format("\tExample data: {0}; result: {1}", input, InverseCaptchaPartTwo(input)));
            }

            Console.WriteLine(string.Format("\n\tTest data: {0}...; result: {1}", Advent.DATA1.Substring(0, 5), InverseCaptchaPartTwo(Advent.DATA1)));
        }
        public static int InverseCaptchaPartTwo(string input) {
            int result = 0;
            for (int i = 0; i < input.Length; i++) {
                if (input[i].Equals(input[(i + (input.Length / 2)) % input.Length])) {
                    result += input[i] - '0'; //ascii to int
                }
            }
            return result;
        }
    }
}
