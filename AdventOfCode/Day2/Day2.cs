using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Xml.XPath;

namespace AdventOfCode.Day2;

public class Day2
{
    private const int _maxRedCubes = 12;
    private const int _maxGreenCubes = 13;
    private const int _maxBlueCubes = 14;

    public int GetSumOfIds()
    {
        int part1Answer = 0;
        int part2Answer = 0;
        var lines = File.ReadAllLines("Day2\\Day2.txt");
        foreach (string line in lines)
        {
            int gameID = int.Parse(line[line.IndexOf(' ')..line.IndexOf(':')]);
     
            //Do you lke making assumptions about how data looks? 
            List<(int cubeCount, string cubeColor)> splits = 
                Regex.Matches(line[(line.IndexOf(':')+1)..], @"(\d+.(?:red|green|blue))")
                    .Select(x => x.Value.Split(' '))
                    .Select(x => (int.Parse(x[0]), x[1]))
                    .ToList(); 

            int maxRed = splits.Where(w => w.cubeColor == "red").Max(x => x.cubeCount);
            int maxGreen = splits.Where(w => w.cubeColor == "green").Max(x => x.cubeCount);
            int maxBlue = splits.Where(w => w.cubeColor == "blue").Max(x => x.cubeCount);
            int theMax = splits.Max(x => x.cubeCount);

            if (_maxRedCubes >= maxRed && _maxGreenCubes >= maxGreen && _maxBlueCubes >= maxBlue)
            {
                part1Answer += gameID;  
            }

            part2Answer += (maxRed * maxGreen * maxBlue);
    
        }

        return part1Answer;
    }
}