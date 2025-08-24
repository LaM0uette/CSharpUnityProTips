# Mots-clés ref, out et in en C#

## 📚 Introduction

En C#, les mots-clés `ref`, `out` et `in` permettent de contrôler comment les paramètres sont passés aux méthodes. Par défaut, C# passe les **types valeur par copie** et les **types référence par référence de l'objet**. Ces mots-clés modifient ce comportement pour optimiser les performances ou permettre des modifications directes.

## 🔄 ref - Passage par référence

### Concept clé
Le mot-clé `ref` permet de passer une variable **par référence** au lieu de par valeur :
- La méthode reçoit une **référence directe** vers la variable originale
- Toute modification dans la méthode **modifie la variable d'origine**
- La variable **doit être initialisée** avant l'appel
- Utilisé pour les paramètres **d'entrée ET de sortie**

### Exemple pratique
```csharp
// Sans ref - passage par copie
private static void AddTen(int x) => x += 10; // Modifie la copie locale

int a = 10;
AddTen(a);
Console.WriteLine(a); // 10 (inchangé)

// Avec ref - passage par référence
private static void AddTen(ref int x) => x += 10; // Modifie l'original

int a2 = 10;
AddTen(ref a2);
Console.WriteLine(a2); // 20 (modifié)
```

### Points importants
- ✅ Variable **doit être initialisée** avant l'appel
- ✅ La méthode **peut lire ET modifier** la valeur
- ✅ Évite la copie pour les **gros types valeur** (struct)

## 📤 out - Paramètre de sortie

### Concept clé
Le mot-clé `out` indique qu'un paramètre est **uniquement en sortie** :
- La méthode **doit assigner** une valeur au paramètre
- La variable **n'a pas besoin d'être initialisée** avant l'appel
- Utilisé quand une méthode doit **retourner plusieurs valeurs**
- Souvent utilisé dans le pattern **TryParse/TryGet**

### Exemple pratique
```csharp
// Sans out - ne modifie pas l'original
private static void InitValue(int y) => y = 100; // Copie locale

int b = 0;
InitValue(b);
Console.WriteLine(b); // 0 (inchangé)

// Avec out - doit assigner une valeur
private static void InitValue(out int y) => y = 100; // Obligatoire

int b2; // Pas besoin d'initialiser
InitValue(out b2);
Console.WriteLine(b2); // 100
```

### Pattern TryGet typique
```csharp
private static bool TryGetFirstChar(string input, out char? c)
{
    c = null; // Assignment obligatoire

    if (string.IsNullOrEmpty(input))
        return false;
    
    c = input.First();
    return true;
}

// Utilisation
if (TryGetFirstChar("Hello", out char? first))
{
    Console.WriteLine($"Premier caractère : {first}"); // H
}
```

### Points importants
- ❌ Variable **n'a pas besoin d'être initialisée**
- ✅ La méthode **doit assigner** une valeur
- ✅ Idéal pour **retourner plusieurs valeurs**

## 📥 in - Paramètre en lecture seule

### Concept clé
Le mot-clé `in` passe un paramètre **par référence en lecture seule** :
- **Évite la copie** des gros types valeur
- Le paramètre est **immutable** dans la méthode
- **Optimisation de performance** pour les structures importantes
- Introduit en C# 7.2

### Exemple pratique
```csharp
// Sans in - copie de la valeur
private static void ShowValue(int z)
{
    Console.WriteLine($"Valeur = {++z}"); // Peut modifier la copie
}

// Avec in - référence en lecture seule
private static void ShowValue(in int z)
{
    Console.WriteLine($"Valeur = {z}"); // Ne peut PAS modifier
    // z++; // ❌ Erreur de compilation
}

int c = 10;
ShowValue(c);      // Copie la valeur
ShowValue(in c);   // Passe par référence (lecture seule)
```

### Cas d'usage optimal
```csharp
// Avec une grosse structure
public struct BigStruct
{
    public double[] Data = new double[1000];
    public int Count;
    // ... autres membres
}

// Sans in - copie de toute la structure (coûteux)
private static void ProcessStruct(BigStruct bigData) { /* ... */ }

// Avec in - pas de copie, lecture seule (performant)
private static void ProcessStruct(in BigStruct bigData) { /* ... */ }
```

### Points importants
- ✅ **Évite la copie** des gros types valeur
- ❌ Paramètre **en lecture seule** uniquement
- ✅ **Optimisation de performance**

## 📊 Comparaison des trois mots-clés

| Mot-clé | Initialisation requise | Lecture | Écriture | Cas d'usage |
|---------|----------------------|---------|----------|-------------|
| `ref` | ✅ Oui | ✅ Oui | ✅ Oui | Modifier une variable existante |
| `out` | ❌ Non | ❌ Non* | ✅ Oui (obligatoire) | Retourner des valeurs |
| `in` | ✅ Oui | ✅ Oui | ❌ Non | Éviter la copie (performance) |

*\* La méthode peut lire la valeur après l'avoir assignée*

## 🎯 Patterns courants

### Pattern TryParse avec out
```csharp
if (int.TryParse("123", out int number))
{
    Console.WriteLine($"Nombre : {number}");
}

// C# 7.0+ - déclaration inline
if (int.TryParse("123", out var num))
{
    Console.WriteLine($"Nombre : {num}");
}
```

### Pattern Multiple Return avec out
```csharp
private static bool TryDivide(int a, int b, out int quotient, out int remainder)
{
    if (b == 0)
    {
        quotient = 0;
        remainder = 0;
        return false;
    }
    
    quotient = a / b;
    remainder = a % b;
    return true;
}
```

### Pattern Performance avec in
```csharp
// Pour les gros types valeur
private static double CalculateDistance(in Vector3D point1, in Vector3D point2)
{
    // Calcul sans copier les structures
    return Math.Sqrt(
        Math.Pow(point2.X - point1.X, 2) +
        Math.Pow(point2.Y - point1.Y, 2) +
        Math.Pow(point2.Z - point1.Z, 2)
    );
}
```

## ⚡ Impact sur les performances

### Types valeur (int, double, struct)
```csharp
// Copie (lent pour les gros struct)
void Method(BigStruct data) { }

// Référence (rapide)
void Method(ref BigStruct data) { }    // Lecture/écriture
void Method(out BigStruct data) { }    // Écriture obligatoire
void Method(in BigStruct data) { }     // Lecture seule (optimal)
```

### Types référence (class, string, array)
```csharp
// Déjà passés par référence de l'objet
void Method(MyClass obj) { }           // Référence de l'objet
void Method(ref MyClass obj) { }       // Référence de la référence
void Method(out MyClass obj) { }       // Doit assigner une nouvelle instance
void Method(in MyClass obj) { }        // Référence en lecture seule
```

## 💡 Bonnes pratiques

### ✅ Utilisez ref quand :
- Vous devez modifier une variable existante
- Vous travaillez avec de gros types valeur et avez besoin de les modifier
- Vous implémentez des algorithmes qui doivent modifier leurs paramètres

### ✅ Utilisez out quand :
- Une méthode doit retourner plusieurs valeurs
- Vous implémentez le pattern TryParse/TryGet
- Vous initialisez une variable dans la méthode

### ✅ Utilisez in quand :
- Vous passez de gros types valeur (struct) en lecture seule
- Vous voulez éviter les copies pour des raisons de performance
- Vous voulez garantir qu'un paramètre ne sera pas modifié

### ❌ Évitez :
- D'utiliser `ref`/`out` pour les petits types valeur sans raison
- `in` pour les types référence (pas d'avantage)
- `out` quand `ref` est plus approprié

## 🔍 Résumé des différences

| Aspect | Passage normal | ref | out | in |
|--------|----------------|-----|-----|-----|
| **Copie de valeur** | ✅ Oui | ❌ Non | ❌ Non | ❌ Non |
| **Modification possible** | ❌ Non | ✅ Oui | ✅ Oui | ❌ Non |
| **Initialisation requise** | ✅ Oui | ✅ Oui | ❌ Non | ✅ Oui |
| **Assignment obligatoire** | ❌ Non | ❌ Non | ✅ Oui | ❌ Non |
| **Performance (gros struct)** | 🐌 Lent | ⚡ Rapide | ⚡ Rapide | ⚡ Rapide |

---

> **Note** : Cette documentation accompagne le code d'exemple `Program.cs` qui démontre concrètement l'utilisation de `ref`, `out` et `in` avec des comparaisons avant/après pour bien visualiser les différences de comportement.