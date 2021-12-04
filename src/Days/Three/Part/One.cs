using System.Collections.Immutable;

namespace Days.Three.Parts
{
    class One
    {
        public static async Task Run()
        {
            var entries = (await File.ReadAllLinesAsync("input/3.txt"))
                .Select(ParseLine)
                .ToImmutableArray();

            var gammaArr = Enumerable.Range(0, entries[0].Length)
                .Select(bit =>
                {
                    var acc = entries
                        .Aggregate(
                            new Accumulator(0, 0),
                            (acc, cur) => cur[bit] switch
                            {
                                0 => acc with { Zero = acc.Zero + 1 },
                                1 => acc with { One = acc.One + 1 },
                                _ => acc
                            }
                        );

                    return acc.Zero > acc.One ? 0 : 1;
                });

            var epsilonArr = gammaArr.Select(bit => (bit + 1) % 2);

            var gamma = Convert.ToInt32(string.Join("", gammaArr), 2);
            var epsilon = Convert.ToInt32(string.Join("", epsilonArr), 2);

            Console.WriteLine(gamma * epsilon);
        }

        static ImmutableArray<int> ParseLine(string line)
        {
            return line
                .Select(i => int.Parse(i.ToString()))
                .ToImmutableArray();
        }

        enum Direction
        {
            Forward,
            Up,
            Down
        }

        record Accumulator(int Zero, int One);
    }
}
