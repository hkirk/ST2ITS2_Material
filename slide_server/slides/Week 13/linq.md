<!-- .slide: data-background="#003d73" -->

# LINQ

## Lambda

![AU Logo](./../img/aulogo_uk_var2_white.png "AU Logo") <!-- .element style="width: 200px; position: fixed; bottom: 50px; left: 50px" -->

----

## Agenda

* Functions<br/><!-- .element: class="fragment" -->
* Lambda<br/><!-- .element: class="fragment" -->
* LINQ<br/><!-- .element: class="fragment" -->

---

## Is LINQ worth it?

* We look at LINQ because
    1. we do the same things over and over<br/><!-- .element: class="fragment" -->
        * so lets make it 'easy'
    2. avoid errors when manually working with collections<br/><!-- .element: class="fragment" -->
    3. <!-- .element: class="fragment" -->seperate <b>what</b> and <b>how</b>
        * simpler code


---

## Methods

* So far we have been talking about </i>methods</i><!-- .element: class="fragment" -->
    * they are a procedures associated with an object
* Objects consists of state and behaviour<!-- .element: class="fragment" -->
    * method is behavoir of that object

```csharp
public class Person {
    private string _name;
    public string GetName() {
        return _name;
    }
}
```
<!-- .element: class="fragment" -->

----

## Functions

* Looks like methods<!-- .element: class="fragment" -->
    * can take arguments
    * return value(s)
* Acts different from methods<!-- .element: class="fragment" -->
    * can only work on values given as arguments


```csharp
public class Person {
    public string UpperCaseName(string name) {
        return name.before.Substring(0, 1).ToUpper() +
            name.Substring(1)
    }
}
```
<!-- .element: class="fragment" -->

----

## Functions as objects

* Methods can take objects as arguments (or return)<!-- .element: class="fragment" -->
    * functions can also do this
```csharp
public SSN GetSSN() {
    return new SSN(_ssn);
}
```
* Functions can be treated as objects<!-- .element: class="fragment" -->
    * meaning methods/function can take functions as arguments and/or return types
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

* Semantically the first is not necessarily a function<!-- .element: class="fragment" -->
* We will get back to the second method :)<!-- .element: class="fragment" -->

---

## Lambda

* Sometimes we only need a function once<!-- .element: class="fragment" -->
```csharp
List<int> ints = new List<int>() {
    1,2,3,4,5,6,7,8,9
};
IEnumerable<int> even = ints.Where(myObject.isEven);
```
* Then we can use anonymous functions (called lambda)<!-- .element: class="fragment" -->
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

* Expression <!-- .element: class="fragment" -->
```csharp
(input parameters) => expression
```
    * returns the result of the expression
    * used when single expression
* Statement<!-- .element: class="fragment" -->
```csharp
(input paramters) => {
    // Sequence of statements
}
```
    * any number of statements
    * in practices a few

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
    * but sometimes type is needed
```csharp
var choose = (bool b) => b ? 1 : "two"; // Gives an error
var choose = object (bool b) => b ? 1 : "two"; 
```

----

<!-- .slide: style="font-size:32px" -->

## Closures

* Where does 'rand'  come from? <!-- .element: class="fragment" data-fragment-index="0" -->
```csharp
() => rand.next();
```



```csharp
public class MyRandom {
	
	public Func<int> RandomNumbers() {
		Random rand = new Random();
		return (() => rand.Next(1));
	}
}
```
<!-- .element: class="fragment" data-fragment-index="1" -->

* Here our lambda needs access to the<!-- .element: class="fragment" data-fragment-index="1" --> `Random` object later<!-- .element: class="fragment" data-fragment-index="1" -->
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

* Things we 'always' do with lists :) <!-- .element: class="fragment" -->
    * filtering
    * projection
    * sort
* and some less common usages <!-- .element: class="fragment" -->
    * set
    * quantify
    * partition
    * group

----

### Query vs method syntax

```csharp [1-5|7-11]
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

* I personally likes method-based best, but that is up to you<!-- .element: class="fragment" -->

note:
```csharp
string sentence = "the quick brown fox jumps over the lazy dog";
string[] words = sentence.Split(' ');
```


----

### Filtering

* We need to get all elements that satisfy some property<!-- .element: class="fragment" -->
```
var wordsWithE = words.Where(word => word.Contains('e'));
var largeWords = words.Where(word => word.Length > 3);
```
* <!-- .element: class="fragment" --><code>OfType</code> returns all elements of a specific type


note:

```csharp
words.OfType<Noun>()...
```

----

<!-- .slide: style="font-size: 32px" -->

### How to read the documentation


This could require a few deep breath <!-- .element: class="fragment" data-fragment-index="1" -->

```csharp
public static System.Collections.Generic.IEnumerable<TSource> Where<TSource>(
    this System.Collections.Generic.IEnumerable<TSource> source,
    Func<TSource,bool> predicate);
```
<!-- .element: class="fragment" data-fragment-index="1" -->

So let us start by cleaning up the types<!-- .element: class="fragment" data-fragment-index="2" -->

```csharp [1-3]
public static IEnumerable<TSource> Where<TSource>(
    this IEnumerable<TSource> source,
    Func<TSource,bool> predicate);
```
<!-- .element: class="fragment" data-fragment-index="2" -->

1. <!-- .element: class="fragment" data-fragment-index="3" --> 1) We return something we can iterate over (<mark>IEnumerable</mark>), contaning <mark>TSource</mark><br/>
2. <!-- .element: class="fragment" data-fragment-index="4" --> 2) 1. parameter `this` keyword is the <mark>class</mark> we can call this extension method on
    * this determines what `TSource` is
3. <!-- .element: class="fragment" data-fragment-index="5" --> 3) 2. parameter is function from <mark>TSource</mark> to <mark>bool</mark>


----

### Projection

* <!-- .element: class="fragment" data-fragment-index="1" -->We need to convert elements from <mark>A -> B</mark>

```
//IEnumerable<TResult> Select<TSource, TResult>(
//    this IEnumerable<TSource> source,
//    Func<TSource,int,TResult> selector)
var wordLength = words.Select(word => word.Length);
var uppercaseWords = words.Select(word => {
    return word.Substring(0,1).ToUpper()
            + word.Substring(1);
});
```
* <!-- .element: class="fragment" data-fragment-index="3" -->We also have <code>SelectMany</code> lets you return collection from the lambda<br/>
* <!-- .element: class="fragment" data-fragment-index="4" -->and <code>Zip</code> merges lists

----

## Sorting

* We saw last week we could use IComparable to sort objects<br/><!-- .element: class="fragment" -->
* LINQ lets us to this a bit easier<br/><!-- .element: class="fragment" -->
```csharp
var sortedBySuffix = words.OrderBy(word =>
    word.Substring(1));
var sortedByLength = words.OrderBy(word => word.Length);
```
* <!-- .element: class="fragment" --><code>ThenBy</code> second 'parameter' to sortBy<br/>
* <!-- .element: class="fragment" --><code>Reverse</code><br/>
* <!-- .element: class="fragment" --><code>*Descending</code> - order is reverse - default is Ascending

----

## Suming 

* <!-- .element: class="fragment" --><a href="https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.sum?view=net-8.0"><code>Sum</code>/<code>Average</code></a> - can sum directly on numeric types, but also with a function
    * E.g. `Func<TSource, Int>`
* <!-- .element: class="fragment" -->Count - and count by a <a href="https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.count?view=net-8.0#system-linq-enumerable-count-1(system-collections-generic-ienumerable((-0))-system-func((-0-system-boolean)))">filter</a><br/>
* <!-- .element: class="fragment" -->Aggregate - Apply an <a href="https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.aggregate?view=net-8.0">accumulator function</a> over a sequence

```csharp
public static TAccumulate Aggregate<TSource,TAccumulate> (
    this IEnumerable<TSource> source,
    TAccumulate seed,
    Func<TAccumulate,TSource,TAccumulate> func);
```
<!-- .element: class="fragment" -->

---

## References


* [LINQ overview](https://learn.microsoft.com/en-us/dotnet/csharp/linq/standard-query-operators/)
* [IEnumerable LINQ methods](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable?view=net-8.0)