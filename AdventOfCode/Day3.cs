using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode {
    class Day3 : IDay {
        public string Name => "--- Day 3: Spiral Memory ---";

        public void Print() {
            Run();
        }

        void Run() {
            int[] examples = new int[] {
                1, 12, 23, 1024,
            };

            foreach (int i in examples) {
                Console.WriteLine(string.Format("Example data: {1}; Distance: {0}", FindDistance(i), i));
            }

            Console.WriteLine(string.Format("Test data: {0}; Distance: {1}", TestData.DATA3, FindDistance(int.Parse(TestData.DATA3))));
            
            Console.WriteLine("Part 2. Input spiral width:");
            int width = int.Parse(Console.ReadLine());
            Console.WriteLine("Input target value:");
            Spiral s = new Spiral(width, int.Parse(Console.ReadLine()));
            Console.WriteLine("Spiral created.");
            Console.ReadKey();
            Console.WriteLine(s.ToString());
        }

        /// <summary>
        /// Close enough but varies vy +/- 1
        /// </summary>
        int FindDistance(int value) {

            int evenOddCoefficient = 0; //TODO

            int n = (int)Math.Ceiling(Math.Sqrt(value));
            int m = n - 1;

            int[] candidates = new int[] {
                n * n,
                m * m,
                n * (n + 1),
                m * (m + 1),
            };

            Array.Sort(candidates);

            int next = candidates.Max(), prev = candidates.Min();
            for (int i = 0; i < candidates.Length; i++) {
                int c = candidates[i];
            }
            foreach (int c in candidates) {
                prev = c > prev && c < value ? c : prev;
                next = c < next && c >= value ? c : next;
            }

            int diff = next - prev;
            int h = diff / 2;
            int w = Math.Abs(prev + h - value);
            int s = h + w;

            return s + evenOddCoefficient;
        }


        public class Spiral {
            Cell[,] cells;
            int middle;
            int totalWidth;
            int lookForValue = 0;
            public Spiral(int width, int lookForValue = 0) {
                totalWidth = width + (1 - width % 2); //ensure it's odd number
                cells = new Cell[totalWidth, totalWidth];
                this.lookForValue = lookForValue;//wtf why
                int max = totalWidth * totalWidth;
                int idx = 1;
                int steps = 1;
                middle = (totalWidth - 1) / 2;

                for (int x = middle, y = middle; (x <= totalWidth && x >= 0) && (y <= totalWidth && y >= 0);) {
                    //left to right
                    for (int i = 0; i < steps && x < totalWidth; i++) {
                        cells[x, y] = new Cell() { id = idx++, value = CalculateValue(x, y) };
                        x++;
                    }
                    if (x >= totalWidth) break;
                    //bottom to top
                    for (int i = 0; i < steps && y < totalWidth; i++) {
                        cells[x, y] = new Cell() { id = idx++, value = CalculateValue(x, y) };
                        y++;
                    }
                    if (y >= totalWidth) break;

                    steps++;

                    //right to left
                    for (int i = 0; i < steps && x >= 0; i++) {
                        cells[x, y] = new Cell() { id = idx++, value = CalculateValue(x, y) };
                        x--;
                    }
                    if (x < 0) break;
                    //top to bottom
                    for (int i = 0; i < steps && y >= 0; i++) {
                        cells[x, y] = new Cell() { id = idx++, value = CalculateValue(x, y) };
                        y--;
                    }
                    if (y < 0) break;
                }

                long CalculateValue(int x, int y) {
                    if (x == middle && y == middle) return 1;
                    long val = 0;
                    for (int dx = -1; dx <= 1; dx++) {
                        for (int dy = -1; dy <= 1; dy++) {
                            if (dx == 0 && dy == 0) continue;
                            val += GetValue(x + dx,y + dy);
                        }
                    }
                    if (lookForValue > 0 && lookForValue == val) {
                        Console.WriteLine("Found value at: " + GetId(x,y));
                    }
                    return val;
                }
            }
            public long GetValue(int x, int y) {
                    if (x < 0 || x >= totalWidth || y < 0 || y >= totalWidth) return 0;
                    else return cells[x, y].value;
            }

            public long GetId(int x, int y) {
                if (x < 0 || x >= totalWidth || y < 0 || y >= totalWidth) return -1;
                else return cells[x, y].id;
            }

            public override string ToString() {
                StringBuilder sb = new StringBuilder();
                for (int x = 0; x < totalWidth; x++) {
                    for (int y = 0; y < totalWidth; y++) {
                        sb.AppendFormat("{000}\t", GetId(x,y));
                    }
                    sb.Append("\n");
                }
                return sb.ToString();
            }
        }

        struct Cell {
            public long id;
            public long value;

            public override string ToString() {
                return string.Format("id: {0}; v: {1}", id, value);
            }
        }
    }
}
