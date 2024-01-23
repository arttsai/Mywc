namespace Mywc;

public class MyCounter
{
    private Option _option;

    public MyCounter(Option option)
    {
        _option = option;
    }

    public async Task Run()
    {
        if (_option.IsFile)
            await CountFileAsync(_option.Path!);
        else
            await CountDirectoryAsync(_option.Path!);
    }

    private Task CountDirectoryAsync(string path)
    {
        throw new NotImplementedException();
    }

    private async Task CountFileAsync(string path)
    {
        throw new NotImplementedException();
    }
}
