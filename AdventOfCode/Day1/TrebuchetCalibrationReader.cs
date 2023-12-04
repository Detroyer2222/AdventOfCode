using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace AdventOfCode.Day1;

public class TrebuchetCalibrationReader
{
    
    public int GetTrebuchetCalibration()
    {
        int trebuchetCalibration = 0;
        var lines = GetLineFromCalibrationText();

        foreach (var line in lines)
        {
            //trebuchetCalibration += GetCalibrationValueFromLine(line);
            trebuchetCalibration += GetCalibrationValueFromLineV2(line);
        }

        return trebuchetCalibration;
    }
    
    
    private IEnumerable<string> GetLineFromCalibrationText()
    {
        var lines = File.ReadAllLines("Day1/trebuchetcalibration.txt");
        return lines;
    }

    private int GetCalibrationValueFromLine(string line)
    {
        int lineValue = 0;

        List<int> lineNumbers = new List<int>();
        int arrayIndex = 0;
        string lineNumber = String.Empty;
        foreach (char c in line)
        {
            int number;
            if (!int.TryParse(c.ToString(), out number))
            {
                continue;
            }

            lineNumbers.Add(number);
        }
        
        if (lineNumbers.Any())
        {
            string stringLineValue = lineNumbers.First().ToString() + lineNumbers.Last().ToString();
            lineValue = int.Parse(stringLineValue);
        }
        
        return lineValue;
    }
    
    private int GetCalibrationValueFromLineV2(string line)
    {
        line = ReplaceNumbersWithDigits(line);
        int lineValue = 0;

        List<int> lineNumbers = new List<int>();
        int arrayIndex = 0;
        string lineNumber = String.Empty;
        foreach (char c in line)
        {
            int number;
            if (!int.TryParse(c.ToString(), out number))
            {
                continue;
            }

            lineNumbers.Add(number);
        }
        
        if (lineNumbers.Any())
        {
            string stringLineValue = lineNumbers.First().ToString() + lineNumbers.Last().ToString();
            lineValue = int.Parse(stringLineValue);
        }
        
        return lineValue;
    }
    
    private static string ReplaceNumbersWithDigits(string input)
    {
        var parsed = input.Replace("one", "o1e");
        parsed = parsed.Replace("two", "t2o");
        parsed = parsed.Replace("three", "t3e");
        parsed = parsed.Replace("four", "f4r");
        parsed = parsed.Replace("five", "f5e");
        parsed = parsed.Replace("six", "s6x");
        parsed = parsed.Replace("seven", "s7n");
        parsed = parsed.Replace("eight", "e8t");
        parsed = parsed.Replace("nine", "n9e");
        return parsed;
    }
}