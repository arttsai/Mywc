using System.Text;

namespace Mywc;

public class WcCounter
{
    public async Task<(int lines, int words, int chars)> RunAsync(Option option)
    {
        var (lines, words, chars) = (0, 0, 0);

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

        // 取得 dirPath 資料夾下的所有檔案
        var files = Directory.GetFiles(dirPath, "*.*", SearchOption.AllDirectories);
        
        //foreach (var file in files)
        for (var i = 0; i < files.Length; i++)
        {
            var file = files[i];
            // 如果不是 option.Types 中的檔案類型 (記得拿掉 '.'), 跳過
            var ext = Path.GetExtension(file).TrimStart('.');
            if (!option.Types!.Contains(ext)) continue;

            // 計算檔案的行數, 字數, 字元數
            var (fileLines, fileWords, fileChars) = await CountFileAsync(option, file);
            lines += fileLines;
            words += fileWords;
            chars += fileChars;
        }

        // 接下來，取得 dirPath 資料夾下的所有子資料夾
        var dirs = Directory.GetDirectories(dirPath, "*", SearchOption.AllDirectories);
        foreach (var dir in dirs)
        {
            // 計算資料夾的行數, 字數, 字元數
            var (dirLines, dirWords, dirChars) = await CountDirectoryAsync(option, dir);
            lines += dirLines;
            words += dirWords;
            chars += dirChars;
        }

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
            sb.Append("] ").Append(filePath);
            Console.WriteLine(sb.ToString());
        }

        return (lines, words, chars);
    }
}