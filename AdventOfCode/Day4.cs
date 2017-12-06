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
            Console.WriteLine("-- Part One --");
            foreach (string example in examples) {
                Console.WriteLine(string.Format("\tExample data: {0}, valid phrases: {1}", example, ValidateAll(example, Validate)));
            }

            string input = TestData.DATA4;
            Console.WriteLine(string.Format("\tScanning test data, valid phrases: {0}", ValidateAll(input, Validate)));

            string[] examples2 = new string[] {
                "abcde fghij",
                "abcde xyz ecdab",
                "a ab abc abd abf abj",
                "iiii oiii ooii oooi oooo",
                "oiii ioii iioi iiio",
            };
            Console.WriteLine("\n-- Part Two --");
            foreach (string example in examples2) {
                Console.WriteLine(string.Format("\tExample data: {0}, valid phrases: {1}", example, ValidateAll(example, ValidateSecure)));
            }
            Console.WriteLine(string.Format("\tScanning test data, extra valid phrases: {0}", ValidateAll(input, ValidateSecure)));
        }

        delegate bool ValidateAction(string passphrase);

        int ValidateAll(string input, ValidateAction ValidateDelegate) {
            int valid = 0;
            foreach (string passphrase in input.Split('\n')) {
                if (string.IsNullOrEmpty(passphrase)) continue;
                valid += ValidateDelegate(passphrase) ? 1 : 0;
            }
            return valid;
        }

        bool Validate(string passphrase) {
            string[] items = passphrase.Split(' ');
            HashSet<string> hash = new HashSet<string>(items);
            return items.Length == hash.Count; //fuck the system
        }

        bool ValidateSecure(string passphrase) {
            string[] words = passphrase.Split(' ');
            HashSet<string> hash = new HashSet<string>();
            for (int i = 0; i < words.Length; i++) {
                char[] word = words[i].ToCharArray();
                System.Array.Sort(word);
                hash.Add(new StringBuilder().Append(word).ToString());
            }
            
            return words.Length == hash.Count;
        }

        public void Print() {
            Run();
        }
    }
}
