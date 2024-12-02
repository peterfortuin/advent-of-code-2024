using System.Collections.ObjectModel;

namespace advent_of_code_2024;

class Day02 : PuzzleBase
{
    public override List<string> GetExampleInputOfPuzzle1()
    {
        return
            """
                7 6 4 2 1
                1 2 7 8 9
                9 7 6 2 1
                1 3 2 4 5
                8 6 4 4 1
                1 3 6 7 9
                """.Split('\n').Select(line => line.Trim()).ToList();
    }

    public override List<string> GetExampleInputOfPuzzle2()
    {
        return GetExampleInputOfPuzzle1();
    }

    public override string GetExampleOutPutOfPuzzle1()
    {
        return "2";
    }

    public override string GetExampleOutPutOfPuzzle2()
    {
        return "4";
    }

    public override List<string> GetTestingInput()
    {
        return ReadLines("Day02");
    }

    public override string RunPuzzle1(ReadOnlyCollection<string> lines)
    {
        var safeReports = lines
            .Select(line => line
                .Split(' ')
                .Select(int.Parse))
            .Count(IsSafe);

        return safeReports.ToString();
    }

    private static bool IsSafe(IEnumerable<int> report)
    {
        return report.Zip(report.Skip(1), (a, b) => a < b && b - a <= 3).All(x => x) ||
               report.Zip(report.Skip(1), (a, b) => a > b && a - b <= 3).All(x => x);
    }

    public override string RunPuzzle2(ReadOnlyCollection<string> lines)
    {
        var safeReports = lines
            .Select(line => line
                .Split(' ')
                .Select(int.Parse))
            .Count(report =>
            {
                if (IsSafe(report))
                    return true;

                foreach (var (reportIndex, i) in report.Index())
                {
                    if (IsSafe(report.Where((item, index) => index != reportIndex)))
                        return true;
                }

                return false;
            });

        return safeReports.ToString();
    }
}