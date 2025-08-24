using System.Text;
using BenchmarkDotNet.Running;

class Program
{
    public static void Mainf()
    {
        // ** Exemple 1 : string **
        Console.WriteLine("=== Exemple 1 : String immuable ===");
        
        string s1 = "Hello";
        string s2 = s1; // s2 pointe vers la même référence que s1

        s1 += " World"; // une NOUVELLE string est créée en mémoire
        
        Console.WriteLine($"s1 = {s1}");
        Console.WriteLine($"s2 = {s2}");
        Console.WriteLine($"Référence s1 : {s1.GetHashCode()}");
        Console.WriteLine($"Référence s2 : {s2.GetHashCode()}");


        // ** Exemple 2 : StringBuilder (mutable avec buffer) **
        Console.WriteLine();
        Console.WriteLine("=== Exemple 2 : StringBuilder mutable ===");
        
        StringBuilder sb1 = new StringBuilder("Hello");
        StringBuilder sb2 = sb1; // les deux pointent vers le même buffer

        sb1.Append(" World"); // on modifie directement le buffer
        
        Console.WriteLine($"sb1 = {sb1}");
        Console.WriteLine($"sb2 = {sb2}");
        Console.WriteLine($"HashCode sb1 : {sb1.GetHashCode()}");
        Console.WriteLine($"HashCode sb2 : {sb2.GetHashCode()}");
    }
    
    /* Uncomment to run the benchmark

    */
     public static void Main()
    {
        BenchmarkRunner.Run<StringVsStringBuilderBenchmark>();
    }

}