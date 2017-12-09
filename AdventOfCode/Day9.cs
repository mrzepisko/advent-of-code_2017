using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode {
    class Day9 : IDay {
        const string cleanup = @"(?>\<[^\>]*\>)";
        const string pattern = @"\{(?<next_group>.*)\}";
        public string Name => "--- Day 9: Stream Processing ---";

        void Run() {
            int x = Process(TestData.DATA9);
            Console.WriteLine(string.Format("Groups score: {0}", x));
        }

        int Process(string input) {
            string unignored = Regex.Replace(input, "!.", "");
            int garbage;
            string cleaned = CleanNestedGarbage(unignored, out garbage);
            Console.WriteLine(string.Format("Cleared garbage: {0}", garbage));
            return CalculateScore(cleaned);
        }

        string CleanNestedGarbage(string input, out int removedGarbage) {
            removedGarbage = 0;
            StringBuilder cleand = new StringBuilder();
            int nestCounter = 0;
            for (int i = 0; i < input.Length; i++) {
                if (input[i] == '<' && nestCounter == 0) {
                    nestCounter++;
                } else if (input[i] == '>') {
                    nestCounter--;
                } else if (nestCounter > 0) {
                    removedGarbage++;
                }

                if (nestCounter == 0 && (input[i] != ',' && input[i] != '>')) {
                    cleand.Append(input[i]);
                }
            }
            return cleand.ToString();
        }

        string CleanUp(string input) {
            return Regex.Replace(input, cleanup, "");
        }

        
        int CalculateScore(string input) {
            int currentScore = 0;
            int totalScore = 0;
            foreach (char c in input) {
                if (c == '{') {
                    currentScore++;
                } else if (c == '}') {
                    totalScore += currentScore;
                    currentScore--;
                }
            }
            return totalScore;
        }


        public void Print() {
            Run();
        }
    }
}

