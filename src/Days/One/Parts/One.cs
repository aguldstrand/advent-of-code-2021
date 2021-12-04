namespace Days.One.Parts
{
    class One
    {
        public static async Task Run()
        {
            var result = (await File.ReadAllLinesAsync("input/1.txt"))
                .Select(line => int.Parse(line))
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
