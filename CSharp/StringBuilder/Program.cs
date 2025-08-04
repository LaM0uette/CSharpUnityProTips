using System.Text;

class Program
{
    public static void Main()
    {
        // Concaténation :
        string result = string.Empty;
        
        for (int i = 0; i < 100; i++)
        {
            result += 'A';
        }
        
        Console.WriteLine(result);
        
        
        
        // StringBuilder :
        StringBuilder stringBuilder = new();
        
        for (int i = 0; i < 100; i++)
        {
            stringBuilder.Append('A');
        }
        
        Console.WriteLine(stringBuilder.ToString());
    }
}