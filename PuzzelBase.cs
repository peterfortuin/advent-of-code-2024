using System.Collections.ObjectModel;

namespace advent_of_code_2024;

abstract class PuzzleBase
{
    public abstract List<string> GetExampleInputOfPuzzle1();

    private List<string> GetExampleInputOfPuzzle1WithTryCatch()
    {
        try
        {
            return GetExampleInputOfPuzzle1();
        }
        catch (NotImplementedException)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Puzzle 1 failed.");
            Console.ResetColor();
            Console.WriteLine("No example input.");
            Console.WriteLine();
            Environment.Exit(1);
            
            return null;
        }
    }
    
    public abstract List<string> GetExampleInputOfPuzzle2();
    
    private List<string> GetExampleInputOfPuzzle2WithTryCatch()
    {
        try
        {
            return GetExampleInputOfPuzzle2();
        }
        catch (NotImplementedException)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Puzzle 2 failed.");
            Console.ResetColor();
            Console.WriteLine("No example input.");
            Console.WriteLine();
            Environment.Exit(1);
            
            return null;
        }
    }
    public abstract string GetExampleOutPutOfPuzzle1();
    
    private string GetExampleOutPutOfPuzzle1WithTryCatch()
    {
        try
        {
            return GetExampleOutPutOfPuzzle1();
        }
        catch (NotImplementedException)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Puzzle 1 failed.");
            Console.ResetColor();
            Console.WriteLine("No example output.");
            Console.WriteLine();
            Environment.Exit(1);
            
            return null;
        }
    }
    
    public abstract string GetExampleOutPutOfPuzzle2();
    
    private string GetExampleOutPutOfPuzzle2WithTryCatch()
    {
        try
        {
            return GetExampleOutPutOfPuzzle2();
        }
        catch (NotImplementedException)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Puzzle 2 failed.");
            Console.ResetColor();
            Console.WriteLine("No example output.");
            Console.WriteLine();
            Environment.Exit(1);
            
            return null;
        }
    }

    public abstract List<string> GetTestingInput();
    
    private List<string> GetTestingInputWithTryCatch()
    {
        try
        {
            return GetTestingInput();
        }
        catch (NotImplementedException)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Puzzle failed.");
            Console.ResetColor();
            Console.WriteLine("No testing input for puzzle.");
            Console.WriteLine();
            Environment.Exit(1);
            
            return null;
        }
    }

    public abstract string RunPuzzle1(ReadOnlyCollection<string> lines);
    
    private string RunPuzzle1WithTryCatch(ReadOnlyCollection<string> lines)
    {
        try
        {
            return RunPuzzle1(lines);
        }
        catch (NotImplementedException)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Puzzle 1 failed.");
            Console.ResetColor();
            Console.WriteLine("No implementation.");
            Console.WriteLine();
            Environment.Exit(1);
            
            return null;
        }
    }
    
    public abstract string RunPuzzle2(ReadOnlyCollection<string> lines);
    
    private string RunPuzzle2WithTryCatch(ReadOnlyCollection<string> lines)
    {
        try
        {
            return RunPuzzle2(lines);
        }
        catch (NotImplementedException)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Puzzle 2 failed.");
            Console.ResetColor();
            Console.WriteLine("No implementation.");
            Console.WriteLine();
            Environment.Exit(1);
            
            return null;
        }
    }

    public void Run()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("Advent of Code 2024");
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Running " + GetType().Name);
        Console.WriteLine();

        Console.ResetColor();
        var exampleInputForPuzzle1 = GetExampleInputOfPuzzle1WithTryCatch().AsReadOnly();
        var resultFromPuzzle1WithExampleInput = RunPuzzle1WithTryCatch(exampleInputForPuzzle1);
        if (GetExampleOutPutOfPuzzle1WithTryCatch() != resultFromPuzzle1WithExampleInput)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Puzzle 1 failed.");
            Console.ResetColor();
            Console.WriteLine("Expected result : " + GetExampleOutPutOfPuzzle1WithTryCatch());
            Console.WriteLine("Actual result   : " + resultFromPuzzle1WithExampleInput);
            Console.WriteLine();
            Environment.Exit(1);
        }

        Console.WriteLine();
        Console.WriteLine("Running Puzzle 1 ....");
        Console.Write("Puzzle 1 completed. Result = ");
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine(RunPuzzle1WithTryCatch(GetTestingInputWithTryCatch().AsReadOnly()));
        Console.ResetColor();
        Console.WriteLine();

        var exampleInputForPuzzle2 = GetExampleInputOfPuzzle2WithTryCatch().AsReadOnly();
        var resultFromPuzzle2WithExampleInput = RunPuzzle2WithTryCatch(exampleInputForPuzzle2);
        if (GetExampleOutPutOfPuzzle2WithTryCatch() != resultFromPuzzle2WithExampleInput)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Puzzle 2 failed.");
            Console.ResetColor();
            Console.WriteLine("Expected result : " + GetExampleOutPutOfPuzzle2WithTryCatch());
            Console.WriteLine("Actual result   : " + resultFromPuzzle2WithExampleInput);
            Console.WriteLine();
            Environment.Exit(1);
        }

        Console.WriteLine();
        Console.WriteLine("Running Puzzle 2 ....");
        Console.Write("Puzzle 2 completed. Result = ");
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine(RunPuzzle2WithTryCatch(GetTestingInputWithTryCatch().AsReadOnly()));
        Console.WriteLine();
    }

    public List<String> ReadLines(string puzzle) => [.. File.ReadAllLines("../../../PuzzleInput/" + puzzle + ".txt")];
}