using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode {
    class Day8 : IDay {
        const string pattern = @"^(?<reg>\w+)\s*(?<cmd>\w+)\s*(?<val>\-?\d+)(?>\s*if\s*)(?<tar>\w+)\s*(?<chk>\S+)\s*(?<val2>\-?\d+)$";
        public string Name => "--- Day 8: I Heard You Like Registers ---";

        void Run() {

            Box box = ProcessInput(TestData.DATA8);
            box.ProcessCommands();
            Console.WriteLine(string.Format("Processed {0} commands, the highest register value is {1}, max value was {2}", box.commands.Count, box.registers.Values.Max(), box.max));
        }

        Box ProcessInput(string input) {
            Box box = new Box();
            foreach (Match m in Regex.Matches(input, pattern, RegexOptions.Multiline)) {
                string reg, cmd, val, tar, chk, val2;
                reg = m.Groups["reg"].Value;
                cmd = m.Groups["cmd"].Value;
                val = m.Groups["val"].Value;
                tar = m.Groups["tar"].Value;
                chk = m.Groups["chk"].Value;
                val2 = m.Groups["val2"].Value;

                if (!box.registers.ContainsKey(reg)) {
                    box.registers.Add(reg, 0);
                }
                box.commands.Add(new Command() {
                    source = reg,
                    addValue = cmd.Equals("inc") ? int.Parse(val) : -int.Parse(val),
                    target = tar,
                    check = chk,
                    checkValue = int.Parse(val2),
                });
            }

            return box;
        }

        public void Print() {
            Run();
        }
        class Box {
            public int max = int.MinValue;
            public Dictionary<string, int> registers = new Dictionary<string, int>();
            public List<Command> commands = new List<Command>();

            public void ProcessCommands() {
                foreach (Command cmd in commands) {
                    max = Math.Max(max, cmd.Execute(registers));
                }
            }
        }
        struct Command {
            public string source;
            public int addValue;
            public string target;
            public string check;
            public int checkValue;

            public enum Check {
                NOP,
                EQ,
                NEQ,
                GT,
                LT,
                GE,
                LE,
            }
            

            public static bool CheckIf(string check, int lval, int rval) {
                switch (check) {
                    case "==":
                        return lval == rval;
                    case "!=":
                        return lval != rval;
                    case ">":
                        return lval > rval;
                    case "<":
                        return lval < rval;
                    case ">=":
                        return lval >= rval;
                    case "<=":
                        return lval <= rval;
                    default:
                        return false;
                }
            }

            public int Execute(Dictionary<string, int> registers) {
                if (CheckIf(check, registers[target], checkValue)) {
                    registers[source] += addValue;
                }
                return registers[source];
            }
        }
    }
}
