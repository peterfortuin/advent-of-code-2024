using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

namespace advent_of_code_2024;

class Day03 : PuzzleBase
{
    public override List<string> GetExampleInputOfPuzzle1()
    {
        return ["xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))"];
    }

    public override List<string> GetExampleInputOfPuzzle2()
    {
        return
        [
            "xmul(2,4)&mul[3,7]!^don't()_mul(5,",
            "5)+mul(32,64](mul(11,8)undo()?mul(8,5))"
        ];
    }

    public override string GetExampleOutPutOfPuzzle1()
    {
        return "161";
    }

    public override string GetExampleOutPutOfPuzzle2()
    {
        return "48";
    }

    public override List<string> GetTestingInput()
    {
        return ReadLines("Day03");
    }

    public override string RunPuzzle1(ReadOnlyCollection<string> lines)
    {
        string pattern = @"mul\((\d+),(\d+)\)";

        var total = lines
            .Select(line => Regex.Matches(line, pattern)
                .Cast<Match>()
                .Select(m => new
                    {
                        FullMatch = m.Value,
                        X = int.Parse(m.Groups[1].Value),
                        Y = int.Parse(m.Groups[2].Value)
                    }
                )
                .Select(match => match.X * match.Y)
                .Sum()
            ).Sum();

        return total.ToString();
    }

    public override string RunPuzzle2(ReadOnlyCollection<string> lines)
    {
        var memory = lines.Aggregate((current, next) => current + next);

        var total = GetMultiplication(memory, true);

        return total.ToString();
    }

    public int GetMultiplication(string memory, bool enabled)
    {
        var mulMatch = Regex.Match(memory, @"mul\((\d+),(\d+)\)");
        var doMatch = Regex.Match(memory, @"do\(\)");
        var dontMatch = Regex.Match(memory, @"don't\(\)");

        var matches = new List<Match> { mulMatch, doMatch, dontMatch }
            .Where(m => m.Success)
            .OrderBy(m => m.Index)
            .ToList();

        if (matches.Count == 0)
        {
            return 0;
        }
        if (matches[0] == mulMatch)
        {
            if (enabled)
            {
                // Mul
                var a = int.Parse(mulMatch.Groups[1].Value);
                var b = int.Parse(mulMatch.Groups[2].Value);

                var remainingMemory = memory.Substring(mulMatch.Index + mulMatch.Length);
                return GetMultiplication(remainingMemory, true) + a * b;
            }
            else
            {
                // Ignored Mul
                var remainingMemory = memory.Substring(mulMatch.Index + mulMatch.Length);
                return GetMultiplication(remainingMemory, false);
            }
        }
        else if (matches[0] == doMatch)
        {
            // Do
            var remainingMemory = memory.Substring(doMatch.Index + doMatch.Length);
            return GetMultiplication(remainingMemory, true);
        }
        else if (matches[0] == dontMatch)
        {
            // Don't
            var remainingMemory = memory.Substring(dontMatch.Index + dontMatch.Length);
            return GetMultiplication(remainingMemory, false);
        }
        else
        {
            return 0;
        }
    }
}