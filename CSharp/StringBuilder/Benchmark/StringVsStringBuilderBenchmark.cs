using System.Text;
using BenchmarkDotNet.Attributes;

[MemoryDiagnoser, ShortRunJob]
public class StringVsStringBuilderBenchmark
{
    private const int N = 1000;

    [Benchmark]
    public string String()
    {
        string s = string.Empty;
        
        for (int i = 0; i < N; i++)
        {
            s += 'A';
        }
        
        return s;
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