//Mywc 
//    -l, --line: 計算列數 (default) 
//    -w, --word: 計算字數
//    -c, --char: 計算字元數 
//    -v, --verbose: 顯示詳細資訊 
//    -s, --silent: 不顯示詳細資訊
//    -h, --help: 程式說明

using CommandLine;
using Mywc;

var rlt = Parser.Default.ParseArguments<Option>(args)
    .WithParsedAsync(Run);

return;

async Task Run(Option option)
{
    //var json = JsonSerializer.Serialize(option);
    //Console.WriteLine(json);

}