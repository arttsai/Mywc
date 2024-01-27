using CommandLine;

namespace Mywc;

// mywc 
//    -l, --line: 計算列數 (default) 
//    -w, --word: 計算字數
//    -c, --char: 計算字元數 
//    -s, --silent: 不顯示詳細資訊, 僅列出總和
//    -t, --type: 檔案類型 (附加名)
//    -h, --help: 程式說明 (command line parser 會自動產生)
//    path: 檔案或資料夾路徑


public class Option
{
    [Option('l', "line", Default = true, HelpText = "計算列數 (default)")]
    public bool CountingLine { get; set; }

    [Option('w', "word", HelpText = "計算字數")]
    public bool CountingWord { get; set; }
    
    [Option('c', "char", HelpText = "計算字元數")]
    public bool CountingChar { get; set; }

    [Option('s', "silent", HelpText = "不顯示詳細資訊, 僅列出總和")]
    public bool Hide { get; set; }

    [Option('t', "type", HelpText = "檔案類型 (附加名)")]
    public IEnumerable<string>? Types { get; set; }

    [Value(0, HelpText = "檔案或資料夾路徑")]
    public string? Path { get; set; }

    /* not relevant with CommandLineParser below */

    public bool IsFile; 

}