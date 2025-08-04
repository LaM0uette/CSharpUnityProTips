using BenchmarkDotNet.Running;

class Program
{
    private static void Main()
    {
        BenchmarkRunner.Run<StringVsStringBuilderBenchmark>();
    }
}