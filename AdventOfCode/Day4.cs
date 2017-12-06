using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode {
    public class Day4 : IDay {
        const string DATA_FILE = "data4.txt";

        public string Name => "--- Day 4: High-Entropy Passphrases ---";

        void Run() {
            string[] examples = new string[] {
                "aa bb cc dd ee",
                "aa bb cc dd aa",
                "aa bb cc dd aaa",
            };
            foreach (string example in examples) {
                Console.WriteLine(string.Format("\tExample data: {0}, valid phrases: {1}", example, ValidateAll(example)));
            }

            string input = TestData.DATA4;
            Console.WriteLine(string.Format("\tScanning test data, valid phrases: {0}", ValidateAll(input)));
        }

        int ValidateAll(string input) {
            int valid = 0;
            foreach (string passphrase in input.Split('\n')) {
                if (string.IsNullOrEmpty(passphrase)) continue;
                valid += Validate(passphrase) ? 1 : 0;
            }
            return valid;
        }

        bool Validate(string passphrase) {
            string[] items = passphrase.Split(' ');
            HashSet<string> hash = new HashSet<string>(items);
            return items.Length == hash.Count; //fuck the system
        }

        public void Print() {
            Run();
        }
    }
}
