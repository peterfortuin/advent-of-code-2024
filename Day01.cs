using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

namespace advent_of_code_2024;

class Day01 : PuzzleBase
{
    public override List<string> GetExampleInputOfPuzzle1()
    {
        return
        [
            "3   4",
            "4   3",
            "2   5",
            "1   3",
            "3   9",
            "3   3",
        ];
    }

    public override List<string> GetExampleInputOfPuzzle2()
    {
        return GetExampleInputOfPuzzle1();
    }

    public override string GetExampleOutPutOfPuzzle1()
    {
        return "11";
    }

    public override string GetExampleOutPutOfPuzzle2()
    {
        return "31";
    }

    public override List<string> GetTestingInput()
    {
        return ReadLines("Day01");
    }

    public override string RunPuzzle1(ReadOnlyCollection<string> lines)
    {
        var numbers = lines
            .Select(s => Regex.Split(s.Trim(), @"\s+")
                .Select(int.Parse)
                .ToArray())
            .ToList();

        var firstList = numbers.Select(pair => pair[0]).ToList();
        var secondList = numbers.Select(pair => pair[1]).ToList();

        firstList.Sort();
        secondList.Sort();

        int totalDifferences = 0;
        for (var i = 0; i < firstList.Count; i++)
        {
            totalDifferences += Math.Abs(firstList[i] - secondList[i]);
        }

        return totalDifferences.ToString();
    }

    public override string RunPuzzle2(ReadOnlyCollection<string> lines)
    {
        var numbers = lines
            .Select(s => Regex.Split(s.Trim(), @"\s+")
                .Select(int.Parse)
                .ToArray())
            .ToList();

        var firstList = numbers.Select(pair => pair[0]).ToList();
        var secondList = numbers.Select(pair => pair[1]).ToList();

        var similarityScore = firstList
            .Select(leftListNumber => secondList
                .FindAll(rightListNumber => rightListNumber == leftListNumber)
                .Count * leftListNumber)
            .Sum();

        return similarityScore.ToString();
    }
}