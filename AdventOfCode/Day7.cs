using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode {
    class Day7 : IDay {
        public string Name => "--- Day 7: Recursive Circus ---";

        void Run() {
            Console.WriteLine(CreateTower(TestData.DATA7).name);
        }

        Program CreateTower(string input) {
            Dictionary<string, Program> programs = new Dictionary<string, Program>();

            string pattern = @"(?<program>\w+).*\((?<weight>\d+)\)(?>.*\-\>\s*(?<subtowers>.+)$)?";
            foreach (Match m in Regex.Matches(input, pattern, RegexOptions.Multiline)) {
                programs.Add(m.Groups["program"].Value,
                    new Program(m.Groups["program"].Value, m.Groups["weight"].Value,
                    m.Groups["subtowers"].Value.Split(
                        new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries)));
            }
            foreach (Program program in programs.Values.Where(p => p.subtowersId != null)) {
                foreach (string sub in program.subtowersId) {
                    programs[sub].SetParent(program.name);
                }
            }
            Program[] find = programs.Values.Where(p => p.parentId == null).ToArray();
            Console.WriteLine(find.Length);
            return find[0];
        }

        string FindLowestWeight(string input) {
            string pattern = @"(?<program>\w+).*\((?<weight>\d+)\).*$";
            string result ="";
            int weight = 1000;
            foreach (Match m in Regex.Matches(input, pattern)) {
                int w = int.Parse(m.Groups["weight"].Value);
                if (w < weight) {
                    weight = w;
                    result = m.Groups["program"].Value;
                }
            }
            return result;
        }

        class Program {
            public string name;
            public int weight;
            public string[] subtowersId;
            public string parentId;

            public Program(string name, int weight, params string[] discs) {
                this.name = name;
                this.weight = weight;
                if (discs != null && discs.Length > 0) {
                    subtowersId = new string[discs.Length];

                    for (int i = 0; i < subtowersId.Length; i++) {
                        subtowersId[i] = discs[i];
                    }
                } else {
                    subtowersId = null;
                }
                parentId = null;
            }

            public Program(string name, string weight, params string[] discs) : this(name, int.Parse(weight), discs) { }

            public void SetParent(string parent) {
                this.parentId = parent;
            }
        }

        public void Print() {
            Run();
        }
    }
}
