// mywc 
//    -l, --line: 計算列數 (default) 
//    -w, --word: 計算字數
//    -c, --char: 計算字元數 
//    -s, --silent: 不顯示詳細資訊, 僅列出總和
//    -t, --type: 檔案類型 (附加名)
//    -h, --help: 程式說明 (command line parser 會自動產生)
//    path: 檔案或資料夾路徑


using System.Text.Json;
using CommandLine;
using Mywc;

var argsStr = string.Join(' ', args);
Console.WriteLine(argsStr);
Console.WriteLine();

var result = Parser.Default.ParseArguments<Option>(args)
    .WithParsedAsync(option => RunAsync(option));
return;

async Task RunAsync(Option option)
{
    //var jso = new JsonSerializerOptions() { WriteIndented = true };
    //var json = JsonSerializer.Serialize(option, jso);
    //Console.WriteLine(json);

    var wcCounter = new WcCounter();
    (int lines, int words, int chars) = await wcCounter.RunAsync(option); 
    if (lines > 0) Console.WriteLine($"Total Lines: {lines}");
    if (words > 0) Console.WriteLine($"Total Words: {words}");
    if (chars > 0) Console.WriteLine($"Total Chars: {chars}");
}