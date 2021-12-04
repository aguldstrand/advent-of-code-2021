namespace Days.Two.Parts
{
    class One
    {
        public static async Task Run()
        {
            var accumulated = (await File.ReadAllLinesAsync("input/2.txt"))
                .Select(ParseLine)
                .Aggregate(
                    new Accumulator(0, 0),
                    (acc, cur) => cur.Dir switch
                    {
                        Direction.Up => acc with { Y = acc.Y - cur.Dst },
                        Direction.Down => acc with { Y = acc.Y + cur.Dst },
                        Direction.Forward => acc with { X = acc.X + cur.Dst },
                        _ => acc
                    }
                );

            var (X, Y) = accumulated;
            System.Console.WriteLine(accumulated);

            Console.WriteLine(X * Y);
        }

        static Entry ParseLine(string line)
        {
            var pieces = line.Split(" ");

            return new Entry(
                Enum.Parse<Direction>(pieces[0], true),
                int.Parse(pieces[1])
            );
        }

        enum Direction
        {
            Forward,
            Up,
            Down
        }

        record Entry(Direction Dir, int Dst);

        record Accumulator(int X, int Y);
    }
}
