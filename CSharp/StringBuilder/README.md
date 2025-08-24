# Immutabilité des String et StringBuilder en C#

## 📚 Introduction

En C#, comprendre la différence entre `string` et `StringBuilder` est crucial pour écrire du code performant. Cette documentation explique pourquoi les strings sont immutables et comment `StringBuilder` résout les problèmes de performance liés aux concatenations multiples.

## 🔒 Immutabilité des String

### Concept clé
Une `string` en C# est **IMMUTABLE** :
- Une fois créée, son contenu ne peut pas être modifié en mémoire
- Toute "modification" crée en réalité une **nouvelle string** avec une nouvelle zone mémoire
- L'ancienne string reste inchangée et sera nettoyée par le Garbage Collector

### Exemple pratique
```csharp
string s1 = "Hello";
string s2 = s1;     // s2 pointe vers la même référence que s1

s1 += " World";     // ⚠️ Création d'une NOUVELLE string !

Console.WriteLine($"s1 = {s1}");  // "Hello World"
Console.WriteLine($"s2 = {s2}");  // "Hello" (inchangé)
```

### Problème de performance
Lors de concatenations multiples :
```csharp
string result = "";
for (int i = 0; i < 1000; i++)
{
    result += "text";  // ⚠️ 1000 nouvelles strings créées !
}
```
Chaque `+=` crée une nouvelle string → **performance dégradée** et **consommation mémoire excessive**.

## 🔧 StringBuilder : La solution

### Principe de fonctionnement
`StringBuilder` utilise un **buffer interne mutable** :
- Buffer = tableau de `char` modifiable
- Capacité par défaut : 16 caractères
- Pas de réallocation tant que la capacité n'est pas dépassée
- Plusieurs variables peuvent pointer vers le même buffer

### Exemple pratique
```csharp
StringBuilder sb1 = new StringBuilder("Hello");
StringBuilder sb2 = sb1;    // Même buffer partagé

sb1.Append(" World");       // ✅ Modification directe du buffer

Console.WriteLine($"sb1 = {sb1}");  // "Hello World"
Console.WriteLine($"sb2 = {sb2}");  // "Hello World" (même buffer)
```

## 📈 Gestion du buffer

### Agrandissement automatique
Quand la capacité est dépassée :
1. Création d'un **nouveau buffer plus grand** (souvent doublé)
2. Copie de l'ancien contenu dans le nouveau buffer
3. Poursuite des opérations dans le nouveau buffer

### Visualisation
```
StringBuilder sb = new StringBuilder("Hello", 16);
// Buffer initial (16 chars) :
[ H e l l o _ _ _ _ _ _ _ _ _ _ _ ]

sb.Append(" World");
// Toujours dans le même buffer :
[ H e l l o   W o r l d _ _ _ _ _ ]

sb.Append(" !!!!!!");  // Dépasse 16 chars
// Nouveau buffer (32 chars) :
[ H e l l o   W o r l d   ! ! ! ! ! ! _ _ _ _ _ _ _ _ _ _ _ _ _ _ ]
```

## 🏁 Comparaison des performances

| Opération | String | StringBuilder |
|-----------|--------|---------------|
| 1 concatenation | Rapide | Légèrement plus lent |
| 10 concatenations | Lent | Rapide |
| 100+ concatenations | **Très lent** | **Très rapide** |

### Règle générale
- **String** : Pour des concatenations occasionnelles ou uniques
- **StringBuilder** : Pour des concatenations multiples ou dans des boucles

## 💡 Bonnes pratiques

### ✅ Utilisez StringBuilder quand :
- Concatenations dans une boucle
- Construction progressive d'une string
- Plus de 3-4 concatenations consécutives
- Performance critique

### ❌ Évitez StringBuilder pour :
- Concatenation unique ou rare
- Strings courtes et simples
- Quand la lisibilité prime sur la performance

### 🔧 Optimisation du StringBuilder
```csharp
// Spécifiez la capacité initiale si connue
var sb = new StringBuilder(capacity: 100);

// Ou utilisez string interpolation pour les cas simples
string result = $"{var1} {var2} {var3}";
```

## 🎯 Points clés à retenir

1. **String = Immutable** : Chaque modification crée une nouvelle instance
2. **StringBuilder = Buffer mutable** : Modifications directes, performance optimisée
3. **Choix selon le contexte** : String pour la simplicité, StringBuilder pour la performance
4. **Gestion automatique** : Le buffer s'agrandit automatiquement selon les besoins

## 📊 Cas d'usage typiques

### StringBuilder recommandé
```csharp
// Construction de HTML/XML
var html = new StringBuilder();
html.Append("<div>");
html.Append(content);
html.Append("</div>");

// Concatenation en boucle
var sb = new StringBuilder();
foreach(var item in items)
{
    sb.Append(item.ToString());
}
```

### String suffisant
```csharp
// Concatenation simple
string fullName = firstName + " " + lastName;

// String interpolation
string message = $"Hello {name}, today is {DateTime.Now:yyyy-MM-dd}";
```

---

> **Note** : Cette documentation accompagne le code d'exemple `Program.cs` qui démontre concrètement ces concepts avec des exemples pratiques et des comparaisons de hash codes.