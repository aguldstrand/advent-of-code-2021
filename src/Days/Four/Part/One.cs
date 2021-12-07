using System.Collections.Immutable;

namespace Days.Four.Parts
{
    class One
    {
        enum ParseState
        {
            Draws,
            Boards
        }

        record ParserAccumulator(
            ParseState State,
            ImmutableArray<int> Draws,
            ImmutableList<ImmutableList<ImmutableArray<int>>> Boards
        );

        public static async Task Run()
        {
            var input = await ParseInput();

            var score = Enumerable.Range(5, input.Draws.Length - 5)
                .Select(numDraws =>
                {
                    var lastDraw = input.Draws[numDraws - 1];
                    System.Console.WriteLine($"lastDraw:{lastDraw}");

                    var draws = input.Draws
                        .Take(numDraws)
                        .ToHashSet();

                    var winnerBoard = input.Boards
                        .FirstOrDefault(
                            board =>
                            {
                                var hasBingoRow = board
                                    .Any(row => row
                                        .All(draws.Contains));

                                var hasBingoCol = Enumerable.Range(0, 5)
                                    .Any(col => board
                                        .All(row => draws.Contains(row[col])));

                                return hasBingoRow || hasBingoCol;
                            }
                        );

                    if (winnerBoard == null)
                    {
                        return (int?)null;
                    }

                    var sumUnselected = winnerBoard
                        .SelectMany(row => row)
                        .Where(cell => !draws.Contains(cell))
                        .Sum();

                    System.Console.WriteLine($"sumUnselected:{sumUnselected}");

                    var score = sumUnselected * lastDraw;

                    return score;
                })
                .First(score => score is not null);

            System.Console.WriteLine(score);

        }

        private static async Task<ParserAccumulator> ParseInput() =>
            (await File.ReadAllLinesAsync("input/4.txt"))
                .Aggregate(
                    new ParserAccumulator(ParseState.Draws, ImmutableArray<int>.Empty, ImmutableList<ImmutableList<ImmutableArray<int>>>.Empty),
                    (acc, row) =>
                    {
                        System.Console.WriteLine($"state:{acc.State} row:{row}");

                        return acc.State switch
                        {
                            ParseState.Draws => acc with
                            {
                                State = ParseState.Boards,
                                Draws = row.Split(",")
                                    .Select(int.Parse)
                                    .ToImmutableArray(),
                            },

                            ParseState.Boards =>
                                row switch
                                {
                                    "" => acc with
                                    {
                                        Boards = acc.Boards.Add(ImmutableList<ImmutableArray<int>>.Empty)
                                    },
                                    _ => acc with
                                    {
                                        Boards = acc.Boards
                                            .Replace(
                                                acc.Boards.Last(),
                                                acc.Boards.Last()
                                                    .Add(row
                                                        .Replace("  ", " ")
                                                        .Trim()
                                                        .Split(" ")
                                                        .Select(int.Parse)
                                                        .ToImmutableArray()))
                                    },
                                },

                            _ => throw new Exception()
                        };
                    });
    }
}
