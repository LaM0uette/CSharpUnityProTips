using System.Text;
using BenchmarkDotNet.Running;

class Program
{
    /* Uncomment to run the benchmark
     
     public static void Main()
    {
        BenchmarkRunner.Run<StringVsStringBuilderBenchmark>();
    }
    
    */
    
    public static void Main()
    {
        /*
         * ======================= IMMUTABILITÉ DES STRING =======================
         * En C#, une string est IMMUTABLE :
         *  - Cela signifie qu'une fois créée, son contenu ne peut pas être modifié en mémoire.
         *  - Quand on "modifie" une string (ex: concaténation), en réalité une NOUVELLE string est créée,
         *    avec une nouvelle zone mémoire.
         *  - L'ancienne string reste inchangée et sera nettoyée plus tard par le Garbage Collector.
         */
        
        // ** Exemple 1 : string **
        string s1 = "Hello";
        string s2 = s1; // s2 pointe vers la même référence que s1

        s1 += " World"; // une NOUVELLE string est créée en mémoire

        Console.WriteLine("=== Exemple 1 : String immuable ===");
        Console.WriteLine($"s1 = {s1}");
        Console.WriteLine($"s2 = {s2}");
        Console.WriteLine($"Référence s1 : {s1.GetHashCode()}");
        Console.WriteLine($"Référence s2 : {s2.GetHashCode()}");
        Console.WriteLine();


        /*
         * ======================= STRINGBUILDER ET BUFFER =======================
         * Contrairement à string, un StringBuilder utilise un BUFFER INTERNE (un tableau de char).
         *  - Ce buffer est MUTABLE : on écrit directement dedans sans recréer d'objet à chaque modification.
         *  - Par défaut, le buffer fait 16 caractères (mais il s'agrandit automatiquement si besoin).
         *  - Si on fait plusieurs Append, tant que la capacité n'est pas dépassée,
         *    aucune réallocation mémoire n'a lieu.
         *  - Plusieurs variables peuvent pointer vers le même StringBuilder :
         *    toute modification sera visible car elles partagent le même buffer.
         */

        // ** Exemple 2 : StringBuilder (mutable avec buffer) **
        StringBuilder sb1 = new StringBuilder("Hello");
        StringBuilder sb2 = sb1; // les deux pointent vers le même buffer

        sb1.Append(" World"); // on modifie directement le buffer

        Console.WriteLine("=== Exemple 2 : StringBuilder mutable ===");
        Console.WriteLine($"sb1 = {sb1}");
        Console.WriteLine($"sb2 = {sb2}");
        Console.WriteLine($"HashCode sb1 : {sb1.GetHashCode()}");
        Console.WriteLine($"HashCode sb2 : {sb2.GetHashCode()}");
        Console.WriteLine();


        /*
         * ======================= AGRANDISSEMENT DU BUFFER =======================
         * Le buffer de StringBuilder a une CAPACITÉ limitée (par défaut 16).
         * Quand on dépasse cette capacité :
         *  - StringBuilder crée un NOUVEAU buffer plus grand (souvent doublé).
         *  - Il copie l'ancien contenu dans ce nouveau buffer.
         *  - Ainsi, le StringBuilder reste performant : on n'a pas de réallocation
         *    à chaque modification, mais seulement quand la capacité est dépassée.
         *
         * 
         * StringBuilder sb = new StringBuilder("Hello", 16);
         *      -> [ H e l l o _ _ _ _ _ _ _ _ _ _ _ ]
         *
         * sb.Append(" World");
         *      -> [ H e l l o   W o r l d _ _ _ _ _ ]
         *
         * sb.Append(" !!!!!!");   // dépasse la capacité 16 -> nouveau buffer de 32
         *      -> [ H e l l o   W o r l d   ! ! ! ! ! ! _ _ _ _ _ _ _ _ _ _ _ _ _ _ ]
         */

        // ** Exemple 3 : Agrandissement du buffer **
        StringBuilder sb3 = new StringBuilder("Hello", 16);
        StringBuilder sb4 = sb3;

        sb3.Append(" World");
        sb3.Append(" !!!!!!"); // dépasse la capacité -> le buffer passe de 16 à 32

        Console.WriteLine("=== Exemple 3 : Agrandissement du buffer ===");
        Console.WriteLine($"sb3 = {sb3}");
        Console.WriteLine($"sb4 = {sb4}");
        Console.WriteLine($"HashCode sb3 : {sb3.GetHashCode()}");
        Console.WriteLine($"HashCode sb4 : {sb4.GetHashCode()}");
        Console.WriteLine($"Capacité du buffer : {sb3.Capacity}");
    }
}