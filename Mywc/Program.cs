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

var str = string.Join(',', args);
Console.WriteLine();

var rlt = Parser.Default.ParseArguments<Option>(args)
    .WithParsed(Run);

return;

void Run(Option option)
{
    var jsonOption = new JsonSerializerOptions { WriteIndented = true};
    var json = JsonSerializer.Serialize(option, jsonOption);
    Console.WriteLine(json);

}