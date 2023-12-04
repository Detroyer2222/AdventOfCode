namespace AdventOfCode.Day4;

public class Day4
{

    private string[] GetText()
    {
        return File.ReadAllLines("Day4/Day4.txt");
    }
    
    public int GetPart1()
    {
        var lines = GetText();
        foreach (var line in lines)
        {
            var splitCard = line.Split(':');
            var splitNumbers = splitCard[1].Split('|', StringSplitOptions.TrimEntries);

            var winningNumbers = splitNumbers[0].Split(' ');

        }


        return 0;
    }
}