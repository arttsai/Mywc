using System.Text;

var dir = args[0];
var option = new Option()
{
    CountingLine = true,
    CountingWord = true,
    CountingChar = true,
    Types = new[] { "cs"}
};

var (lines, words, chars) = await CountDirectoryAsync(option, dir);
Console.WriteLine($"Total lines: {lines}");
Console.WriteLine($"Total words: {words}");
Console.WriteLine($"Total chars: {chars}");


async Task<(int lines, int words, int chars)> CountDirectoryAsync(Option option, string dirPath)
{
    var (lines, words, chars) = (0, 0, 0);

    var files = Directory.GetFiles(dirPath, "*.*", SearchOption.AllDirectories);
        
    for (var i = 0; i < files.Length; i++)
    {
        var file = files[i];
        var ext = Path.GetExtension(file).TrimStart('.');
        if (!option.Types!.Contains(ext)) continue;

        var (fileLines, fileWrods, fileChars) = await CountFileAsync(option, file);
        lines += fileLines;
        words += fileWrods;
        chars += fileChars;
    }

    return (lines, words, chars);   
}

async Task<(int lines, int words, int chars)> CountFileAsync(Option option, string filePath)
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

public class Option
{
    public bool CountingLine { get; set; }
    public bool CountingWord { get; set; }
    public bool CountingChar { get; set; }
    public bool Silent { get; set; }
    public IEnumerable<string>? Types { get; set; }
}