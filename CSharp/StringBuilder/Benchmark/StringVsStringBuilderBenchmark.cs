using System.Text;
using BenchmarkDotNet.Attributes;

[MemoryDiagnoser, ShortRunJob]
public class StringVsStringBuilderBenchmark
{
    private const int N = 100000;

    [Benchmark]
    public string Concat()
    {
        string result = string.Empty;
        
        for (int i = 0; i < N; i++)
        {
            result += 'A';
        }
        
        return result;
    }

    [Benchmark]
    public string StringBuilder()
    {
        StringBuilder stringBuilder = new();
        
        for (int i = 0; i < N; i++)
        {
            stringBuilder.Append('A');
        }
        
        return stringBuilder.ToString();
    }
}