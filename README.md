# Aoxe.Extensions

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![Stars](https://img.shields.io/github/stars/AoxeTech/Aoxe.Extensions.svg)](https://github.com/AoxeTech/Aoxe.Extensions/stargazers)
[![Forks](https://img.shields.io/github/forks/AoxeTech/Aoxe.Extensions.svg)](https://github.com/AoxeTech/Aoxe.Extensions/network/members)

---

Aoxe.Extensions is a powerful C# library that provides a rich set of extension methods to enhance your development experience and boost productivity. Our goal is to simplify common programming tasks and make your code more readable and maintainable.

## 🚀 Features

- 🧩 Extensive collection of extension methods for built-in C# types
- 🎯 Designed to reduce boilerplate code and improve readability
- 🔧 Easy to use with a fluent API design
- 📚 Comprehensive documentation and examples
- ⚡ High-performance implementations
- 🔒 Thread-safe operations where applicable
- 🧪 Thoroughly tested with high code coverage

## 📦 Installation

Install Aoxe.Extensions via Clr:

```bash
PM> Install-Package Aoxe.Extensions
```

## 🔧 Usage

To use Aoxe.Extensions in your project, add the following using statement:

```csharp
using Aoxe.Extensions;
```

## 🌟 Examples

Aoxe.Extensions is based on the idea of fluent style, providing a huge number of extension methods to extend C#, thus making the C# language more readable.

```csharp
// Exception handling
try
{
    throw new InvalidOperationException("Outer", 
        new ArgumentException("Inner", 
            new NullReferenceException("Inmost")));
}
catch (Exception ex)
{
    var inmostException = ex.GetInmostException();
    Console.WriteLine($"Inmost exception message: {inmostException.Message}");
    // Output: Inmost exception message: Inmost
}

// String extensions
string text = "Hello, World!";
Console.WriteLine(text.IsNullOrEmpty());  // False
Console.WriteLine(text.Truncate(8));      // "Hello..."

// Collection extensions
var numbers = new List<int> { 1, 2, 3, 4, 5 };
Console.WriteLine(numbers.IsNullOrEmpty());  // False
var evenNumbers = numbers.Where(n => n % 2 == 0);
Console.WriteLine(string.Join(", ", evenNumbers));  // 2, 4

// DateTime extensions
var date = DateTime.UtcNow;
Console.WriteLine(date.ToUnixTimeSeconds());

// Enum extensions
public enum Color { Red, Green, Blue }
var color = Color.Red;
Console.WriteLine(color.GetDescription());

// Numeric extensions
int number = 42;
Console.WriteLine(number.IsEven());  // True
```

These examples showcase potential usage of extension methods that might be included in the Aoxe.Extensions library. The actual implementation and available methods may vary, so it's recommended to refer to the official documentation or source code for accurate usage information.

[Source: https://github.com/AoxeTech/Aoxe.Extensions]

---

Thank`s for [JetBrains](https://www.jetbrains.com/) for the great support in providing assistance and user-friendly environment for my open source projects.

[![JetBrains](https://resources.jetbrains.com/storage/products/company/brand/logos/jb_beam.svg?_gl=1*f25lxa*_ga*MzI3ODk2MjY0LjE2NzA0NjY4MDQ.*_ga_9J976DJZ68*MTY4OTY4NzY5OS4zNC4xLjE2ODk2ODgwMDAuNTMuMC4w)](https://www.jetbrains.com/community/opensource/#support)
