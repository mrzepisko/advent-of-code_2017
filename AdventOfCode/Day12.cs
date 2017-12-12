using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode {
    class Day12 : IDay {
        public string Name => "--- Day 12: Digital Plumber ---";

        List<Node> graph = new List<Node>();

        void Run() {
            graph = Process(TestData.DATA12);
            int empty = 0;

            int target;
            Console.Write("\nSet source value: ");
            while (!int.TryParse(Console.ReadLine(), out target)) {
                Console.Write("\nInvalid value, try again: ");
            }

            foreach (Node node in graph) {
                if (PassesThrought(node.id, target)) {
                    empty++;
                }
            }
            Console.Write("Unconnected to source ({0}): {1}", target, empty);

            List<HashSet<int>> programs = new List<HashSet<int>>();
            foreach (Node n in graph) {
                if (programs.Select(hs => hs.Contains(n.id)).Contains(true)) continue; //node was already used
                programs.Add(DefineProgram(n.id));
            }

            Console.Write("\nTotal number of programs: {0}", programs.Count());
        }

        

        List<Node> Process(string input) {
            List<Node> nodes = new List<Node>();

            const string pattern = @"^(?<id>\d+).*\<\-\>.*?(?<values>\d.*)";


            foreach (Match m in Regex.Matches(input, pattern, RegexOptions.Multiline)) {
                int id = int.Parse(m.Groups["id"].Value);
                nodes.Add(new Node() { id = id, children = m.Groups["values"].Value.Split(',').Select(s => new Node(int.Parse(s.Trim()))).ToList() });
            }

            return nodes;
        }
        HashSet<int> DefineProgram(int source, HashSet<int> set = null) {
            if (set == null) set = new HashSet<int>();
            if (set.Contains(source)) return set;
            set.Add(source);
            foreach (Node n in graph[source].children) {
                if (set.Contains(n.id)) continue;
                DefineProgram(n.id, set);
            }

            return set;
        }
        bool PassesThrought(int source, int target, List<int> visited = null) {
            if (visited == null) {
                visited = new List<int>();
            } if (visited.Contains(source)) {
                return false;
            }
            if (graph[source].children.Contains((Node)target)) return true;

            visited.Add(source);
            foreach (Node n in graph[source].children) {
                if (visited.Contains(n.id)) continue;
                if (PassesThrought(n.id, target, visited)) {
                    return true;
                }
            }
            return false;
        }

        struct Node {
            public int id;
            public List<Node> children;
            public Node(int id) {
                this.id = id;
                children = null;
            }
            public override bool Equals(object obj) {
                if (obj is int) return ((int)obj).Equals(id);
                if (!(obj is Node)) return false;
                else return ((Node)obj).id.Equals(this.id);
            }
            public override int GetHashCode() {
                return id.GetHashCode();
            }

            public static explicit operator Node(int i) {
                return new Node(i);
            }
        }

        public void Print() {
            Run();
        }
    }
}
