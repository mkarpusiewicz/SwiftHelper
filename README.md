# SwiftHelper

[![Build status](https://ci.appveyor.com/api/projects/status/43g27kognqaqmbgr?svg=true)](https://ci.appveyor.com/project/mkarpusiewicz/swifthelper)
[![NuGet](https://img.shields.io/nuget/v/SwiftHelper.svg)](https://www.nuget.org/packages/SwiftHelper/)

### What is SwiftHelper?
SwiftHelper is a simple C# library with extension methods for commonly used operations on collections with optimization in mind.

### Where can I get it?

Nuget, install alpha pre-release [SwiftHelper](https://www.nuget.org/packages/SwiftHelper/) from the package manager console:

```
PM> Install-Package SwiftHelper -Pre
```

### Sample code
Below you can find sample usages of extension methods.
#### IEnumerable\<TSource\>.IsNullOrEmpty()
Checks if source is null or empty.
```csharp
public void DoSomething(IEnumerable<string> source)
{
   if (source.IsNullOrEmpty()) {
        throw new ArgumentNullException(nameof(source));
   }
   ...
   //some logic
}
```

#### ICollection\<TSource\>.ForEach(Action\<TSource\> action)
Execute an action for every element in a collection.
```csharp
//todo: sample code
```

#### IEnumerable\<TSource\>.DistinctBy(Func\<TSource, TSelector\> selector)
Filter distinct objects in a collection using a specific field as a distinct parameter using default equals/hashcode implementation.
```csharp
private static readonly List<SimpleUser> Users = new List<SimpleUser>
{
    new SimpleUser {Name = "John", Gender = Gender.Male, Age = 27},
    new SimpleUser {Name = "Amy", Gender = Gender.Female, Age = 21},
    new SimpleUser {Name = "Kate", Gender = Gender.Female, Age = 19}
};

public void SomeMethod()
{
    var result = Users.DistinctBy(u => u.Gender).ToArray();
    
    Assert.Equal(2, result.Length);
    Assert.Equal("John", result[0].Name);
    Assert.Equal("Amy", result[1].Name);
}
```

#### ICollection\<TSource\>.Compare(ICollection\<TElement\> oldEnumerable, ICollection\<TElement\> newEnumerable)
Compare two lists using default comparer and return result with added and removed elements collection
```csharp
//todo: sample code
```

#### TSource.Generate(Func\<TSource, TSource\> generationAction, Func\<TSource, bool\> whileCondition = null)
Create an enumerable from a starting object, apply generation action after every element to the starting object until while condition is not met. Can create infinite series generator.
```csharp
public void GenerateRangeOfNumbers()
{
    var generator = 1.Generate(i => i + 1);
    var result = string.Join(",", generator.Skip(5).Take(3));

    Assert.Equal("6,7,8", result);
}
```
Or a more complicated example of a fibonacci number generator with new C# 7 tuples:
```csharp
public void FibonacciGenerator()
{
    var generator = (prev: 0UL, state: 1UL).Generate(f => (f.state, f.prev + f.state)).Select(f => f.state);
    var result = generator.ElementAt(93 - 1); //93th fibonacci number

    Assert.Equal(12200160415121876738UL, result);
}
```

### Do you have an issue or idea?

If you have any problems please check the issues and fill one if no resolution is found.
Of course if you have an idea for an improvement or a new extension please don't hesitate to fill an issue or submit a pull request.

### License

SwiftHelper is Copyright &copy; 2017 Maciej Karpusiewicz and other contributors under the [MIT license](LICENSE).
