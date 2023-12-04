using System.Text.RegularExpressions;

namespace AdventOfCode.Day3;

public class Day3
{
        private string[] grid = File.ReadAllLines("Day3/Day3.txt");

        private char GetTile(VectorRC coord)
        {
            if (coord.Row < 0 || coord.Row >= grid.Length)
            {
                return '.';
            }
            if (coord.Col < 0 || coord.Col >= grid[coord.Row].Length)
            {
                return '.';
            }
            return grid[coord.Row][coord.Col];
        }

        public int Part1()
        {
            int sum = 0;
            for (int row = 0; row < grid.Length; row++)
            {
                var matches = Regex.EnumerateMatches(grid[row], @"\d+");
                foreach (var match in matches)
                {
                    bool foundSymbol = false;
                    for (int i = 0; i < match.Length && !foundSymbol; i++)
                    {
                        VectorRC coord = new VectorRC(row, match.Index + i);
                        if (coord.EightNeighbors().Select(GetTile).Any(c => !char.IsDigit(c) && c != '.'))
                        {
                            sum += int.Parse(grid[row].AsSpan().Slice(match.Index, match.Length));
                            foundSymbol = true;
                        }
                    }
                }
            }
            return sum;
        }

        public int Part2()
        {
            Dictionary<VectorRC, (VectorRC start, int value)> gridNumbers = new();
            for (int row = 0; row < grid.Length; row++)
            {
                var matches = Regex.EnumerateMatches(grid[row], @"\d+");
                foreach (var match in matches)
                {
                    VectorRC start = new VectorRC(row, match.Index);
                    int value = int.Parse(grid[row].AsSpan().Slice(match.Index, match.Length));
                    for (int i = 0; i < match.Length; i++)
                    {
                        VectorRC coord = new VectorRC(row, match.Index + i);
                        gridNumbers[coord] = (start, value);
                    }
                }
            }

            int sum = 0;
            for (int row = 0; row < grid.Length; row++)
            {
                var matches = Regex.EnumerateMatches(grid[row], @"\*");
                foreach (var match in matches)
                {
                    VectorRC coord = new VectorRC(row, match.Index);
                    var adjacentNumbers = coord.EightNeighbors().Where(gridNumbers.ContainsKey).Select(c => gridNumbers[c]).Distinct().ToList();
                    if (adjacentNumbers.Count == 2)
                    {
                        sum += adjacentNumbers[0].value * adjacentNumbers[1].value;
                    }
                }
            }
            return sum;
        }
}

internal record struct VectorRC(int Row, int Col)
{
    public static VectorRC operator +(VectorRC left, VectorRC right)
    {
        return new VectorRC(left.Row + right.Row, left.Col + right.Col);
    }
    public static VectorRC operator -(VectorRC left, VectorRC right)
    {
        return new VectorRC(left.Row - right.Row, left.Col - right.Col);
    }
    public static VectorRC operator -(VectorRC val)
    {
        return new VectorRC(-val.Row, -val.Col);
    }

    public readonly int Dot(VectorRC that)
    {
        return this.Row * that.Row + this.Col * that.Col;
    }
    public readonly VectorRC RotatedLeft()
    {
        return new VectorRC(-Col, Row);
    }
    public readonly VectorRC RotatedRight()
    {
        return new VectorRC(Col, -Row);
    }
    public readonly VectorRC[] EightNeighbors()
    {
        return new VectorRC[]
        {
            this + new VectorRC(-1, -1),
            this + new VectorRC(-1, 0),
            this + new VectorRC(-1, +1),
            this + new VectorRC(0, -1),
            this + new VectorRC(0, +1),
            this + new VectorRC(+1, -1),
            this + new VectorRC(+1, 0),
            this + new VectorRC(+1, +1),
        };
    }
}
