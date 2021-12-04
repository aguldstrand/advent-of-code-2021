namespace Days.Two.Parts
{
    class Two
    {
        public static async Task Run()
        {
            var accumulated = (await File.ReadAllLinesAsync("input/2.txt"))
                .Select(ParseLine)
                .Aggregate(
                    new Accumulator(0, 0, 0),
                    (acc, cur) => cur.Dir switch
                    {
                        Direction.Up => acc with { Aim = acc.Aim - cur.Dst },
                        Direction.Down => acc with { Aim = acc.Aim + cur.Dst },
                        Direction.Forward => acc with { X = acc.X + cur.Dst, Y = acc.Y + acc.Aim * cur.Dst },
                        _ => acc
                    }
                );

            var (X, Y, _) = accumulated;
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

        record Accumulator(int X, int Y, int Aim);
    }
}
