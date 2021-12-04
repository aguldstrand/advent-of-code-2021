namespace Days.One.Parts
{
    class Two
    {
        public static async Task Run()
        {

            var lines0 = (await File.ReadAllLinesAsync("input/1.txt"))
                .Select(line => int.Parse(line));

            var lines1 = lines0.Skip(1);
            var lines2 = lines1.Skip(1);

            var result = lines0.Zip(lines1, lines2)
                .Select(i => i.First + i.Second + i.Third)
                .Aggregate(
                    new Accumulator(0, 0, null),
                    (acc, cur) =>
                    {
                        var updatedCounter = acc switch
                        {
                            { Prev: var prev } when cur > prev => acc with { Inc = acc.Inc + 1 },
                            { Prev: var prev } when cur < prev => acc with { Dec = acc.Dec + 1 },
                            _ => acc
                        };

                        return updatedCounter with { Prev = cur };
                    }
                );

            Console.WriteLine(result);
        }

        record Accumulator(int Inc, int Dec, int? Prev);
    }
}
