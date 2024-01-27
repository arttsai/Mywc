using System.Text;
using Mywc;

public class WcCounter
{
    public async Task<(int lines, int words, int chars)> RunAsync(Option option)
    {
        (int lines, int words, int chars) = (0, 0, 0);

        // 如果 option.Path 是檔案, option.IsFile 設為 true
        if (File.Exists(option.Path))
        {
            option.IsFile = true;
            (lines, words, chars) = await CountFileAsync(option, option.Path);
        }
        else if (Directory.Exists(option.Path))
        {
            option.IsFile = false;
            (lines, words, chars) = await CountDirectoryAsync(option, option.Path);
        }
        else
        {
            Console.WriteLine($"Path not found: {option.Path}");
            return (lines, words, chars);
        }



        return (lines, words, chars);
    }

    private async Task<(int lines, int words, int chars)> CountDirectoryAsync(Option option, string dirPath)
    {
        var (lines, words, chars) = (0, 0, 0);



        return (lines, words, chars);
    }

    private async Task<(int lines, int words, int chars)> CountFileAsync(Option option, string filePath)
    {
        var (lines, words, chars) = (0, 0, 0);

        using var reader = new StreamReader(filePath);
        while (await reader.ReadLineAsync() is { } line)
        {
            lines++;
            if (option.CountingWord) words += line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Length;
            if (option.CountingChar) chars += line.Length;
        }

        if (!option.Silent)
        {
            // 依照 lines, words, chars 的值, 顯示對應的資訊, 並格式化輸出，做成一行
            // 依據 option 的 CountingLine, CountingWord, CountingChar, 來決定要顯示的資訊
            // 格式如下: [lines, words, chars] path
            var sb = new StringBuilder();
            sb.Append('[');
            if (option.CountingLine) sb.Append(lines);
            if (option.CountingWord) sb.Append(", ").Append(words);
            if (option.CountingChar) sb.Append(", ").Append(chars);
            sb.Append("] ").Append(option.Path);
            Console.WriteLine(sb.ToString());
        }

        return (lines, words, chars);
    }
}