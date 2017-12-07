using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode {
    class Day7 : IDay {
        public string Name => "--- Day 7: Recursive Circus ---";

        Dictionary<string, Program> programs = new Dictionary<string, Program>();

        void Run() {
            Program main = CreateTower(TestData.DATA7);
            Console.WriteLine(string.Format("Base program: {0}", main.name));
            int weight = FindInvalidWeightDiff(main);

            Console.WriteLine(string.Format("Fixing weight: {0}", weight));
            
        }

        Program CreateTower(string input) {
            programs.Clear();
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
            Program main = programs.Values.Where(p => p.parentId == null).ToArray()[0];

            CreateTree(main);
            CalculateWeights(main);

            return main;
        }

        void CreateTree(Program program) {
            if (program.subtowersId == null) {
                return;
            } else {
                for (int i = 0; i < program.subtowersId.Length; i++) {
                    program.subtowers[i] = programs[program.subtowersId[i]];
                    CreateTree(program.subtowers[i]);
                }
            }
        }

        void CalculateWeights(Program program) {
            if (program.subtowers == null) return;
            program.subWeight = 0;
            foreach (Program sub in program.subtowers) {
                CalculateWeights(sub);
                program.subWeight += sub.totalWeight;
            }
        }
        string FindLowestWeight(string input) {
            string pattern = @"(?<program>\w+).*\((?<weight>\d+)\).*$";
            string result = "";
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

        int FindInvalidWeightDiff(Program program) {
            if (program.subtowers == null) {
                return 0;
            }
            for (int i = 1; i < program.subtowers.Length; i++) {
                int diff = program.subtowers[i].totalWeight - program.subtowers[i - 1].totalWeight;
                if (diff != 0) {
                    return Math.Abs(diff);
                }
            }

            foreach (Program sub in program.subtowers) {
                int diff = FindInvalidWeightDiff(sub);
                if (diff != 0) {
                    return diff;
                }
            }
            return 0;
        }

        int Max(params int[] values) {
            int max = 0;
            foreach (int i in values) {
                max = Math.Max(i, max);
            }
            return max;
        }

        class Program {
            public string name;
            public int weight;
            public int subWeight;
            public string[] subtowersId;
            public Program[] subtowers;
            public string parentId;

            public int totalWeight {  get { return weight + subWeight; } }

            public Program(string name, int weight, params string[] discs) {
                this.name = name;
                this.weight = weight;
                if (discs != null && discs.Length > 0) {
                    subtowersId = new string[discs.Length];
                    subtowers = new Program[discs.Length];

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

            //public void Print(Dictionary<string, Program> programs, int indent = 0) {
            //    string ind = new System.String(' ', indent);
            //    Console.WriteLine(ind + "|");
            //    Console.WriteLine(ind + "--" + name);
            //    Console.WriteLine(ind + string.Format("    {0:000} ({1:000}", weight, subWeight));
            //    if (subtowersId != null) {
            //        foreach (Program sub in subtowersId.Select(sub => programs[sub])) {
            //            sub.Print(programs, indent + 1);
            //        }
            //    }
            //}

            public override string ToString() {
                return string.Format("{0} {1} ({2})", name, weight, totalWeight);
            }
        }

        public void Print() {
            Run();
        }
    }
}
