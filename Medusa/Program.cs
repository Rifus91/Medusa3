using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Sandbox
{
    internal class Program
    {
        class Medusa
        {
            public int X;
            public int Y;
            public int Z;
            public int Radius;
            int test;
            
            public Medusa(string s)
            {
                var array = s.Split(' ').Select(int.Parse).ToArray();
                X = array[0];
                Y = array[1];
                Z = array[2];
                Radius = array[3];
            }

            public bool Overlaps3D(Medusa medusa)
            {
                return Math.Sqrt((medusa.X - X) * (medusa.X - X) + (medusa.Y - Y) * (medusa.Y - Y) + (medusa.Z - Z) * (medusa.Z - Z)) <= Radius + medusa.Radius;
            }

            public bool Overlaps2D(Medusa medusa)
            {
                return Math.Sqrt((medusa.X - X) * (medusa.X - X) + (medusa.Y - Y) * (medusa.Y - Y)) <= Radius + medusa.Radius;
            }
        }

        public static void Main(string[] args)
        {
            var n = int.Parse(Console.ReadLine());
            var medusas = new List<Medusa>();
            for (var i = 0; i < n; i++)
            {
                medusas.Add(new Medusa(Console.ReadLine()));
            }

            var wakedUp = new HashSet<int> { 0 };
            if (medusas.Count == 1)
            {
                Console.WriteLine(wakedUp.Count);
                Console.WriteLine(string.Join(" ", wakedUp.Select(i => i + 1).OrderBy(i => i).ToArray()));
                return;
            }

            var checkQueue = new Queue<int>();
            checkQueue.Enqueue(0);
            while (checkQueue.Any())
            {
                var medusa = medusas[checkQueue.Dequeue()];
                for (var i = 0; i < medusas.Count; i++)
                {
                    if (wakedUp.Contains(i))
                    {
                        continue;
                    }

                    var testMedusa = medusas[i];
                    if (testMedusa.Z <= medusa.Z && testMedusa.Overlaps3D(medusa)
                        || testMedusa.Z > medusa.Z && testMedusa.Overlaps2D(medusa))
                    {
                        wakedUp.Add(i);
                        checkQueue.Enqueue(i);
                    }
                }
            }

            Console.WriteLine(wakedUp.Count);
            Console.WriteLine(string.Join(" ", wakedUp.Select(i => i + 1).OrderBy(i => i).ToArray()));
            Console.ReadLine();
        }
    }
}