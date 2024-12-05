using System.Collections.ObjectModel;

namespace advent_of_code_2024;

class Day04 : PuzzleBase
{
    public override List<string> GetExampleInputOfPuzzle1()
    {
        return
            """
                MMMSXXMASM
                MSAMXMSMSA
                AMXSXMAAMM
                MSAMASMSMX
                XMASAMXAMM
                XXAMMXXAMA
                SMSMSASXSS
                SAXAMASAAA
                MAMMMXMMMM
                MXMXAXMASX
                """.Split('\n').Select(line => line.Trim()).ToList();
    }

    public override List<string> GetExampleInputOfPuzzle2()
    {
        return
            """
                .M.S......
                ..A..MSMS.
                .M.S.MAA..
                ..A.ASMSM.
                .M.S.M....
                ..........
                S.S.S.S.S.
                .A.A.A.A..
                M.M.M.M.M.
                ..........
                """.Split('\n').Select(line => line.Trim()).ToList();
    }

    public override string GetExampleOutPutOfPuzzle1()
    {
        return "18";
    }

    public override string GetExampleOutPutOfPuzzle2()
    {
        return "9";
    }

    public override List<string> GetTestingInput()
    {
        return ReadLines("Day04");
    }

    public override string RunPuzzle1(ReadOnlyCollection<string> lines)
    {
        var array = ConvertTo2DArray(lines);

        var occurrences = FindWordOccurrences(array, "XMAS");

        return occurrences.ToString();
    }

    public override string RunPuzzle2(ReadOnlyCollection<string> lines)
    {
        var array = ConvertTo2DArray(lines);

        int rows = array.GetLength(0);
        int cols = array.GetLength(1);
        int count = 0;

        for (int i = 0; i < rows - 2; i++)
        {
            for (int j = 0; j < cols - 2; j++)
            {
                if (IsXMas(array, i, j))
                    count++;
            }
        }

        return count.ToString();
    }

    public static char[,] ConvertTo2DArray(ReadOnlyCollection<string> lines)
    {
        if (lines == null || lines.Count == 0)
            throw new ArgumentException("Input lines cannot be null or empty.");

        int rows = lines.Count;
        int cols = lines[0].Length;

        if (lines.Any(line => line.Length != cols))
            throw new ArgumentException("All lines must have the same length.");

        char[,] array = new char[rows, cols];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                array[i, j] = lines[i][j];
            }
        }

        return array;
    }

    private static int FindWordOccurrences(char[,] array, string word)
    {
        int rows = array.GetLength(0);
        int cols = array.GetLength(1);
        int count = 0;

        int[][] directions = new int[][]
        {
            new[] { 0, 1 }, // Right
            new[] { 0, -1 }, // Left
            new[] { 1, 0 }, // Down
            new[] { -1, 0 }, // Top
            new[] { 1, 1 }, // Diagonal down-right
            new[] { 1, -1 }, // Diagonal down-left
            new[] { -1, 1 }, // Diagonal up-right
            new[] { -1, -1 } // Diagonal up-left
        };

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                foreach (var direction in directions)
                {
                    int dx = direction[0];
                    int dy = direction[1];

                    if (CanFitWord(array, word, i, j, dx, dy))
                    {
                        if (IsWordMatch(array, word, i, j, dx, dy))
                        {
                            count++;
                        }
                    }
                }
            }
        }

        return count;
    }

    private static bool CanFitWord(char[,] array, string word, int row, int col, int dx, int dy)
    {
        int wordLength = word.Length;
        int rows = array.GetLength(0);
        int cols = array.GetLength(1);

        int endRow = row + (wordLength - 1) * dx;
        int endCol = col + (wordLength - 1) * dy;

        return endRow >= 0 && endRow < rows && endCol >= 0 && endCol < cols;
    }

    private static bool IsWordMatch(char[,] array, string word, int row, int col, int dx, int dy)
    {
        for (int k = 0; k < word.Length; k++)
        {
            if (array[row + k * dx, col + k * dy] != word[k])
            {
                return false;
            }
        }

        return true;
    }

    private static bool IsXMas(char[,] array, int row, int col)
    {
        // top left, top right
        if (array[row + 0, col + 0] == 'M' &&
            array[row + 1, col + 1] == 'A' &&
            array[row + 2, col + 2] == 'S' &&
            array[row + 2, col + 0] == 'M' &&
            array[row + 1, col + 1] == 'A' &&
            array[row + 0, col + 2] == 'S')
            return true;
        // top right, bottom right
        if (array[row + 2, col + 0] == 'M' &&
            array[row + 1, col + 1] == 'A' &&
            array[row + 0, col + 2] == 'S' &&
            array[row + 2, col + 2] == 'M' &&
            array[row + 1, col + 1] == 'A' &&
            array[row + 0, col + 0] == 'S')
            return true;
        // bottom right, bottom left
        if (array[row + 2, col + 2] == 'M' &&
            array[row + 1, col + 1] == 'A' &&
            array[row + 0, col + 0] == 'S' &&
            array[row + 0, col + 2] == 'M' &&
            array[row + 1, col + 1] == 'A' &&
            array[row + 2, col + 0] == 'S')
            return true;
        // bottom left, top left
        if (array[row + 0, col + 2] == 'M' &&
            array[row + 1, col + 1] == 'A' &&
            array[row + 2, col + 0] == 'S' &&
            array[row + 0, col + 0] == 'M' &&
            array[row + 1, col + 1] == 'A' &&
            array[row + 2, col + 2] == 'S')
            return true;

        return false;
    }
}