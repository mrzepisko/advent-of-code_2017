using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace AdventOfCode {
    class Advent {
        public static void Main(params string[] args) {
            App app = new App();
            app.Intro();

            while(app.DaysMenu() > 0) {
                Console.Clear();
            }

        }
    }

    public class App {
        IDay[] days;
        public App() {
            days = new IDay[] {
                new Day1(),
                new Day2(),
                new Day3(),
                new Day4(),
                new Day5(),
                new Day6(),
                new Day7(),
                new Day8(),
                new Day9(),
                new Day10(),
                new Day11(),
                };
        }
        public void Intro() {
            Console.WriteLine(TestData.INTRO_TEXT);
            
            Console.ReadKey();
        }
        public int DaysMenu() {
            Console.Clear();
            Console.WriteLine(string.Format("Choose day (1 - {0}) Q to quit)", days.Length));
            for (int i = 0; i < days.Length; i++) {
                Console.WriteLine(string.Format("\t{0}. {1}", i + 1, days[i].Name));
            }
            bool valid = false;
            string input;
            int selection = 0;
            for (; !valid; ) {
                input = Console.ReadLine();
                if ("q".Equals(input.ToLower())) return 0;
                int idx;
                if (int.TryParse(input, out idx)) {
                    valid = idx > 0 && idx <= days.Length;
                }

                if (!valid) {
                    Console.Clear();
                    Console.WriteLine(string.Format("Invalid value, try again (1 - {0}): ", days.Length));
                } else {
                    selection = idx - 1;
                }
            }

            IDay day = days[selection];
            Console.Clear();
            Console.WriteLine(day.Name);
            Console.WriteLine("\n");
            day.Print();
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            return 1;
        }
    }


    public interface IDay {
        string Name { get; }
        void Print();
    }
}
