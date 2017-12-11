using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode {
    class Day11 : IDay {
        public string Name => "--- Day 11: Hex Ed ---";

        int CalculateSteps(string input) {
            HexCoords[] dirs = input.Split(',').Select(
                s => HexCoords.Direction(s)).ToArray();
            HexCoords coords = new HexCoords();
            int max = 0;
            foreach (HexCoords dir in dirs) {
                coords.x += dir.x;
                coords.y += dir.y;
                coords.z += dir.z;
                max = Math.Max(max, coords.Distance());
            }
            Console.Write("Max: {0}", max);
            return coords.Distance();
        }

        void Run() {
            Console.Write("\nDistance to target: {0}", CalculateSteps(TestData.DATA11));
        }

        public void Print() {
            Run();
        }

        struct HexCoords {
            public int x, y, z;
            public static HexCoords Direction(string dir) {
                switch (dir) {//q-even
                    case "n":
                        return new HexCoords() { x = 0, y = 1, z = -1, };
                    case "s":
                        return new HexCoords() { x = 0, y = -1, z = 1, };
                    case "ne":
                        return new HexCoords() { x = 1, y = 0, z = -1, };
                    case "nw":
                        return new HexCoords() { x = -1, y = 1, z = 0, };
                    case "sw":
                        return new HexCoords() { x = -1, y = 0, z = 1, };
                    case "se":
                        return new HexCoords() { x = 1, y = -1, z = 0, };
                    default: return default(HexCoords);
                }
            }

            public int Distance(HexCoords target = default(HexCoords)) {
                HexCoords a = this, b = target;
                //ctrl+c ctrl+v
                return (Math.Abs(a.x - b.x) + Math.Abs(a.y - b.y) + Math.Abs(a.z - b.z)) / 2;
            }
        }
    }
}
