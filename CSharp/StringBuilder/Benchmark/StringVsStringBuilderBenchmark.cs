using System.Text;
using BenchmarkDotNet.Attributes;

[MemoryDiagnoser, ShortRunJob]
public class StringVsStringBuilderBenchmark
{
    private const int N = 100_000;

    [Benchmark]
    public string String()
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
        StringBuilder sb = new StringBuilder();
        
        for (int i = 0; i < N; i++)
        {
            sb.Append('A');
        }
        
        return sb.ToString();
    }
}