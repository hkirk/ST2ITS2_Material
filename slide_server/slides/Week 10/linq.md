<!-- .slide: data-background="#003d73" -->

# Collections types

## Mutability / immutability

![AU Logo](./../img/aulogo_uk_var2_white.png "AU Logo") <!-- .element style="width: 200px; position: fixed; bottom: 50px; left: 50px" -->

----

## Agenda

* Functions
* Lambda
* LINQ

---

## Why is LINQ worth looking into?

* We look at LINQ because
    1. we do the same thing again and again, so lets make it 'easy'
    2. avoid error when manually iterating
    3. seperate **what** and **how**
        * -> simpler code


---

## Methods

* So far we have been talking about *methods*
    * they are a procedure associated with a object
* Objects consists of state and behaviour
    * method is behavoir of that object

```csharp
public class Person {
    private string _name;
    public string GetName() {
        return _name;
    }
}
```

----

## Functions

* Like methods
    * can take arguments
    * return value(s)
* different from method
    * can only work on values given as arguments


```csharp
public class Person {
    public string UpperCaseName(string name) {
        return name.before.Substring(0, 1).ToUpper() +
            name.Substring(1)
    }
}
```

----

## Functions as objects

* Methods can take objects as arguments (or return)
    * functions can also do this
```csharp
public SSN GetSSN() {
    return new SSN(_ssn);
}
```
* Functions can be treated as objects
    * Meaning methods/function can take functions as arguments and/or return types
```csharp
public List<int> Filter(List<int> list,
        Func<int, bool> pred) {}
public Func<int, int> AddA(int a) {}
```

----

### Creating functions as parameters

* Two ways to create functions

```csharp [2-4,10-11|5-7|2-7,10-12]
public class FunctionTest {
	public int Add(int a) {
		return a+3;	
	}
    public static int Add2(int a) {
        return a+4;
    }
}
// usage 
var ft = new FunctionTest();
Func<int, int>  func = ft.Add;
Func<int, int>  func2 = FunctionTest.Add2;

Console.WriteLine(func(3));
```

* Semantically the first is not necessarily a function
* We will get back to the second method :)

---

## Lambda

* Sometimes we only need a function once
```csharp
List<int> ints = new List<int>() {
    1,2,3,4,5,6,7,8,9
};
IEnumerable<int> even = ints.Where(myObject.isEven);
```
* Then we can use anonymous functions (called lambda)
```csharp
IEnumerable<int> even = ints.Where((a) => a % 2 == 0);
```

note:

```csharp
// Missing code to the code in the slide above
public class MyClass {
	public bool isEven(int a) => a % 2 == 0;
}

MyClass myClass = new MyClass();
```

----

### Lambda syntax

* Expression 
```csharp
(input parameters) => expression
```
    * returns the result of the expression
    * used when single expression
* Statement
```csharp
(input paramters) => {
    // Sequence of statements
}
```
    * any number of statements
    * in practices a few
    * used otherwise

----

### Parameters

* Examples on different syntax for parameters
```csharp
() => rand.next();         // zero
(a) => a+3;                // one
(a, b) => a+b;             // many
(int a, int b) => a+b;     // typed
(int a, int b = 2) => a+b; // default
(a, _) => a+1;             // discarded
```
* basicaly all you can do with methods

----

### Return value (and type)

* Most of the time .NET can infer return type
    * but sometimes its needed to add it manually
```csharp
var choose = (bool b) => b ? 1 : "two"; // Gives an error
var choose = object (bool b) => b ? 1 : "two"; 
```

----

<!-- .slide: style="font-size:32px" -->

## Closures

* Remember this:

```csharp
() => rand.next();
```

* Where does `rand` come from?

```csharp
public class MyRandom {
	private List<int> ints;
	
	public IEnumerable<int> RandomNumbers() {
		Random rand = new Random();
		return ints.Where(_ => rand.Next(1) == 0);
	}
}
```
<!-- .element: class="fragment" data-fragment-index="1" -->

* Here our lambda needs access to the<!-- .element: class="fragment" data-fragment-index="1" --> `Random` object <!-- .element: class="fragment" data-fragment-index="1" -->
    * this is 'saved' in a closure, for later usage<!-- .element: class="fragment" data-fragment-index="1" -->

----

### Another example

```csharp
public class MyRandom {
	private List<int> ints;
	private Random _rand = new Random();	
	public Func<int, bool> RandomSelector() {
		return ((_) => _rand.Next(1) == 0);
	}
}
// Somewhere else
List<int> ints = new List<int>() {
    1,2,3,4,5,6,7,8,9
};
MyRandom myRandom = new MyRandom();
ints.Where(myRandom.RandomSelector());
```
* Here the compiler must save the `_rand` object for usage somewhere else.


---

## LINQ method style

* Things we 'always' do with lists :)
    * filtering
    * projection
    * sort
* and some lesser used
    * set
    * quantify
    * partition
    * group

----

### Query vs method syntax

```csharp
// Using query expression syntax.
var query = from word in words
            group word.ToUpper() by word.Length into gr
            orderby gr.Key
            select new { Length = gr.Key, Words = gr };

// Using method-based query syntax.
var query2 = words.
    GroupBy(w => w.Length, w => w.ToUpper()).
    Select(g => new { Length = g.Key, Words = g }).
    OrderBy(o => o.Length);
```

* I personally likes methods best, but that is up to you

note:
```csharp
string sentence = "the quick brown fox jumps over the lazy dog";
string[] words = sentence.Split(' ');
```


----

### Filtering

* We need to get all elements that satisfy some property
```
var wordsWithE = words.Where(word => word.Contains('e'));
var largeWords = words.Where(word => word.Length > 3);
```
* `OfType` returns all elements of a specific type

----

<!-- .slide: style="font-size: 32px" -->

### How to read the documentation


This could require a few deep breath

```csharp
public static System.Collections.Generic.IEnumerable<TSource> Where<TSource>(
    this System.Collections.Generic.IEnumerable<TSource> source,
    Func<TSource,bool> predicate);
```

So let us start by cleaning up the types

```csharp [1-3]
public static IEnumerable<TSource> Where<TSource>(
    this IEnumerable<TSource> source,
    Func<TSource,bool> predicate);
```
1. We return something we can iterate over, containg `TSource`
2. 1. parameters `this` keyword is the <mark>class</mark> we can call this extension method on
    * this determines what `TSource` is
3. 2. parameter is function from `TSource` to `bool`


----

### Projection

* We need to convert elements from `A` -> `B`
```
var wordLength = words.Select(word => word.Length);
var uppercaseWords = words.Select(word => {
    return word.Substring(0,1).ToUpper() + word.Substring(1);
});
```
* We also have `SelectMany` lets you return collection from the lambda
* and `Zip` merges lists

----

## Sorting

* We saw last week we could use IComparable to sort objects
* LINQ lets us to this a bit easier
```csharp
var sortedBySuffix = words.OrderBy(word =>
    word.Substring(1));
var sortedByLength = words.OrderBy(word => word.Length);
```
* `ThenBy` second 'parameter' to sortBy
* `Reverse`
* `*Descending` - order is reverse - default is Ascending

----

## Suming 

* [`Sum`/`Average`](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.sum?view=net-8.0) - can sum directly on number types, but also with a function
    * E.g. `Func<TSource, Int>`
* Count - and count by a [filter](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.count?view=net-8.0#system-linq-enumerable-count-1(system-collections-generic-ienumerable((-0))-system-func((-0-system-boolean))))
* Aggregate - Apply an [accumulator function](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.aggregate?view=net-8.0) over a sequence

```csharp
public static TAccumulate Aggregate<TSource,TAccumulate> (
    this IEnumerable<TSource> source,
    TAccumulate seed,
    Func<TAccumulate,TSource,TAccumulate> func);
```

---

## References


* [LINQ overview](https://learn.microsoft.com/en-us/dotnet/csharp/linq/standard-query-operators/)
* [IEnumerable LINQ methods](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable?view=net-8.0)