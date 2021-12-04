using System.Collections.Immutable;

namespace Days.Three.Parts
{
    class Two
    {
        public static async Task Run()
        {
            var entries = (await File.ReadAllLinesAsync("input/3.txt"))
                .Select(ParseLine)
                .ToImmutableArray();

            ImmutableArray<int> Get(Func<int, int, bool> cmp) =>
                Enumerable.Range(0, entries[0].Length)
                    .Aggregate(entries, (acc, bit) =>
                    {
                        if (acc.Length == 1)
                        {
                            return acc;
                        }

                        var bitCounter = acc
                            .Aggregate(
                               new Accumulator(0, 0),
                               (acc, cur) => cur[bit] switch
                               {
                                   0 => acc with { Zero = acc.Zero + 1 },
                                   1 => acc with { One = acc.One + 1 },
                                   _ => acc
                               }
                            );

                        var bitValueToKeep = cmp(bitCounter.Zero, bitCounter.One) ? 0 : 1;

                        var outp = acc
                            .Where(bits => bits[bit] == bitValueToKeep)
                            .ToImmutableArray();

                        System.Console.WriteLine($"bit:{bit} keep:{bitValueToKeep} count:{outp.Length}");

                        return outp;
                    })
                    .Single();

            var oxygenArr = Get((zero, one) => zero > one);
            var co2Arr = Get((zero, one) => zero <= one);

            System.Console.WriteLine(string.Join("", oxygenArr));
            System.Console.WriteLine(string.Join("", co2Arr));

            var oxygen = Convert.ToInt32(string.Join("", oxygenArr), 2);
            var co2 = Convert.ToInt32(string.Join("", co2Arr), 2);

            Console.WriteLine(oxygen * co2);
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
