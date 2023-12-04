namespace AdventOfCode.Day4;

public class Day4
{
    public void EvaluateDay()
    {
        var lines = File.ReadAllLines("Day4/Day4.txt");

        int resultPart1 = 0;
        List<int> cardsCount = new List<int>();
        int copyIndex = 1;
        int result = 0;
        
        foreach (var line in lines)
        {
            var splitCard = line.Split(':');
            var splitNumbers = splitCard[1].Split('|', StringSplitOptions.TrimEntries);

            var winningNumbersArray = splitNumbers[0].Split(' ');
            var winningNumbers = new List<int>();
            foreach (string number in winningNumbersArray)
            {
                if (string.IsNullOrWhiteSpace(number)) continue;
                winningNumbers.Add(int.Parse(number));
            }

            var drawnNumbers = splitNumbers[1].Split(' ');
            var myNumbers = new List<int>();
            foreach (string number in drawnNumbers)
            {
                if (string.IsNullOrWhiteSpace(number)) continue;
                myNumbers.Add(int.Parse(number));
            }

            var matches = winningNumbers.Intersect(myNumbers);
            
            
            int numberCount = matches.Count();

            if (numberCount == 1) resultPart1 = resultPart1 + numberCount;
            else if (numberCount > 1) resultPart1 = resultPart1 + (int) Math.Pow(2, numberCount - 1);
            
            
            if (cardsCount.Count < copyIndex) cardsCount.Add(1);
            else cardsCount[copyIndex - 1]++;

            int intersectCount = matches.Count();
            int currentCardCopies = cardsCount[copyIndex - 1];

            for (int cardNumber = copyIndex + 1; cardNumber <= copyIndex + intersectCount; cardNumber++)
            {
                if (cardNumber > cardsCount.Count) cardsCount.Add(currentCardCopies);
                else cardsCount[cardNumber - 1] += currentCardCopies;
            }

            copyIndex++;
            
            
        }
        
        Console.WriteLine("Day 4 Part 1: " + resultPart1);
        Console.WriteLine("Day 4 Part 2: " + cardsCount.Sum());
    }
}