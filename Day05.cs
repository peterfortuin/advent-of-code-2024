using System.Collections.ObjectModel;

namespace advent_of_code_2024;

class Day05 : PuzzleBase
{
    public override List<string> GetExampleInputOfPuzzle1()
    {
        return
            """
                47|53
                97|13
                97|61
                97|47
                75|29
                61|13
                75|53
                29|13
                97|29
                53|29
                61|53
                97|53
                61|29
                47|13
                75|47
                97|75
                47|61
                75|61
                47|29
                75|13
                53|13

                75,47,61,53,29
                97,61,53,29,13
                75,29,13
                75,97,47,61,53
                61,13,29
                97,13,75,29,47
                """.Split('\n').Select(line => line.Trim()).ToList();
    }

    public override List<string> GetExampleInputOfPuzzle2()
    {
        return GetExampleInputOfPuzzle1();
    }

    public override string GetExampleOutPutOfPuzzle1()
    {
        return "143";
    }

    public override string GetExampleOutPutOfPuzzle2()
    {
        return "123";
    }

    public override List<string> GetTestingInput()
    {
        return ReadLines("Day05");
    }

    public override string RunPuzzle1(ReadOnlyCollection<string> lines)
    {
        int emptyLineIndex = lines.IndexOf("");

        List<string[]> pageOrderRules = lines
            .Take(emptyLineIndex)
            .Select(pageOrderRuleText => pageOrderRuleText.Split('|'))
            .ToList();
        List<string[]> pagesToProduce = lines
            .Skip(emptyLineIndex + 1)
            .Select(pageOrderRuleText => pageOrderRuleText.Split(','))
            .ToList();

        var sum = pagesToProduce.Select(pagesToProduce =>
        {
            return pageOrderRules
                .Where(pageOrderRule => pagesToProduce.Contains(pageOrderRule[0]) && pagesToProduce.Contains(pageOrderRule[1]))
                .Select(pageOrderRule => Array.IndexOf(pagesToProduce, pageOrderRule[0]) < Array.IndexOf(pagesToProduce, pageOrderRule[1]))
                .All(correct => correct)
                ? int.Parse(pagesToProduce[pagesToProduce.Length / 2])
                : 0;
        }).Sum();

        return sum.ToString();
    }

    public override string RunPuzzle2(ReadOnlyCollection<string> lines)
    {
        int emptyLineIndex = lines.IndexOf("");

        List<string[]> pageOrderRules = lines
            .Take(emptyLineIndex)
            .Select(pageOrderRuleText => pageOrderRuleText.Split('|'))
            .ToList();
        List<string[]> pagesToProduce = lines
            .Skip(emptyLineIndex + 1)
            .Select(pageOrderRuleText => pageOrderRuleText.Split(','))
            .ToList();

        Comparison<string> pagesComparator = (x, y) =>
        {
            if (pageOrderRules.Any(rule => rule[0] == x && rule[1] == y))
            {
                return -1;
            }

            if (pageOrderRules.Any(rule => rule[0] == y && rule[1] == x))
            {
                return 1;
            }

            return 0;
        };

        var sum = pagesToProduce
            .Select(pagesToProduce =>
            {
                return pageOrderRules
                    .Where(pageOrderRule => pagesToProduce.Contains(pageOrderRule[0]) && pagesToProduce.Contains(pageOrderRule[1]))
                    .Select(pageOrderRule => Array.IndexOf(pagesToProduce, pageOrderRule[0]) < Array.IndexOf(pagesToProduce, pageOrderRule[1]))
                    .Any(correct => !correct)
                    ? pagesToProduce
                    : null;
            })
            .Where(pagesToProduce => pagesToProduce != null)
            .Select(pagesToProduce =>
            {
                Array.Sort(pagesToProduce, pagesComparator);
                return pagesToProduce;
            })
            .Select(pagesToProduce => int.Parse(pagesToProduce[pagesToProduce.Length / 2]))
            .Sum();

        return sum.ToString();
    }
}