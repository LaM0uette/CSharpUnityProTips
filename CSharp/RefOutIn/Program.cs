namespace RefOutIn;

class Program
{
    public static void Main()
    {
        // ===== REF =====
        int a = 10;
        Console.WriteLine("=== ref ===");
        Console.WriteLine($"Avant : a = {a}");
        AddTen(a);
        Console.WriteLine($"Après (sans ref) : a = {a}");

        int a2 = 10;
        Console.WriteLine($"Avant : a2 = {a2}");
        AddTen(ref a2);
        Console.WriteLine($"Après (avec ref) : a2 = {a2}");
        Console.WriteLine();


        // ===== OUT =====
        Console.WriteLine("=== out ===");
        int b = 0; // obligé d’initialiser manuellement sinon erreur
        InitValue(b);
        Console.WriteLine($"Sans out : b = {b}");

        int b2; // non initialisé
        InitValue(out b2);
        Console.WriteLine($"Avec out : b2 = {b2}");
        Console.WriteLine();


        // ===== IN =====
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
    
    private static void ShowValue(int z) => Console.WriteLine($"Valeur reçue = {++z}");
    private static void ShowValue(in int z) => Console.WriteLine($"Valeur reçue = {z}");
}