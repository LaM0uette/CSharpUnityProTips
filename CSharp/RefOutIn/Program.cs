namespace RefOutIn;

class Program
{
    public static void Main()
    {
        // ===== REF =====
        Console.WriteLine("=== ref ===");
        
        int a = 10;
        Console.WriteLine($"Avant : a = {a}");
        AddTen(a);
        Console.WriteLine($"Après (sans ref) : a = {a}");

        int a2 = 10;
        Console.WriteLine($"Avant : a2 = {a2}");
        AddTen(ref a2);
        Console.WriteLine($"Après (avec ref) : a2 = {a2}");
        
        
        // ===== OUT =====
        Console.WriteLine();
        Console.WriteLine("=== out ===");
        
        int b = 0;
        Console.WriteLine($"Avant : b = {b}");
        InitValue(b);
        Console.WriteLine($"Après (sans out) : b = {b}");

        int b2;
        Console.WriteLine("Avant : b2 n'est pas initialisé");
        InitValue(out b2);
        Console.WriteLine($"Après (avec out) : b2 = {b2}");

        if (TryGetFirstChar("Hello", out char? first))
        {
            Console.WriteLine($"Premier caractère : {first}");
        }
        else
        {
            Console.WriteLine("Chaîne vide");
        }
        

        // ===== IN =====
        Console.WriteLine();
        Console.WriteLine("=== in ===");
        
        int c = 10;
        ShowValue(c);

        int c2 = 10;
        ShowValue(in c2);
    }
    
    private static void AddTen(int x) => x += 10; // copie locale → ne change pas l'original
    private static void AddTen(ref int x) => x += 10; // modifie la variable d’origine
    
    private static void InitValue(int y) => y = 100; // copie locale → ne change pas l'original
    private static void InitValue(out int y) => y = 100; // obligatoire d’assigner
    
    private static bool TryGetFirstChar(string input, out char? c)
    {
        c = null;

        if (string.IsNullOrEmpty(input))
        {
            return false;
        }
        
        c = input.First();
        return true;
    }
    
    private static void ShowValue(int z) => Console.WriteLine($"Valeur reçue (sans in) = {++z}");
    private static void ShowValue(in int z) => Console.WriteLine($"Valeur reçue (avec in, lecture seule) = {z}");
}