using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PercentageDistribution
{
    class Program
    {
        static void Main(string[] args)
        {
            var options = new[]
            {
                new Option() { ExpectedPercentage=10 },
                new Option() { ExpectedPercentage=25 },
                new Option() { ExpectedPercentage=35 },
                new Option() { ExpectedPercentage=30 }
            };

            var totalCount = 0;
            int value = 0;
            while ((value = int.Parse(Console.ReadLine())) != 0)
            {
                for (int ix = 0; ix < value; ix++)
                {
                    var errors = options.Select((o, sx) =>
                    {
                        var currentPercentage = totalCount == 0 ? 0 : (double)o.Count / totalCount * 100;
                        var currentError = o.ExpectedPercentage - currentPercentage;
                        return new { Error = currentError, Index = sx };

                    }).ToArray();

                    var selectedOption = errors.Where(er => er.Error >= 0).OrderBy(er => er.Error).Last();
                    options[selectedOption.Index].Count++;
                    totalCount++;
                }

                Console.WriteLine($"Total count{totalCount}");
                foreach (var ops in options)
                {
                    Console.WriteLine($"{ops.ExpectedPercentage}%=>{ops.Count} {ops.Count / (double)totalCount * 100}%");
                }
            }
        }

        class Option
        {
            public int ExpectedPercentage { get; set; }

            public int Count { get; set; }
        }
    }
}
