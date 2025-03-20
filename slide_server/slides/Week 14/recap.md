<!-- .slide: data-background="#003d73" -->

# Recap

![AU Logo](./../img/aulogo_uk_var2_white.png "AU Logo") <!-- .element style="width: 200px; position: fixed; bottom: 50px; left: 50px" -->

----

## Agenda

* Fields / Properties
    * UML
* Class types
    * interfaces
    * abstract
    * `base`
* LINQ
    * Filter
    * Projection
    * Aggregate


---

## Data in classes

**Two** important questions
* which data belongs in a class
    * *cohesion*
* data integrity
    * *encapsulation*

----

## Properties vs fields

* I would 'always' start with private fields
    * So I can respect data integrity can be
* I data is *readonly* I normally don't care if its private
* Some libraries (haven't seen any so far) requires properties to work

----

## In UML

* UML is language agnostic
* So its done in a number of different ways
* In ITS2 we use

```text
+--------------------------+
| MyClass                  |
|--------------------------|
| - Field : int {get; set;}|
| - Field2 : int {readonly}|
|--------------------------|
|                          |
+--------------------------+
```

---

## Classes

You know all about ordinary classes - right?

* Can contain 
    * methods
    * fields
    * properties
* with visibility
* **Inheritance** from a class is shown with
    * *full line, closed arrow*

----

### Abstract classes

* Is classes, but
    * cannot be instantiated
    * can have abstract methods that are not implemented
* **Inheritance** from an abstract class is shown with
    * *full line, closed arrow*

!["Abstract and interface](./../Week%2011/img/interface%20and%20abstract.png "")


----

### Interfaces

* Can contain
    * methods definitions
    * fields definitions
* Cannot contain implementations
* Cannot be instantiated
* **Implementation** from an interface is shown with
    * *dotted line, closed arrow*

!["Abstract and interface](./../Week%2011/img/interface%20and%20abstract.png "")


---

## LINQ

* Filter
    * `Where`
* Projection (Map)
    * `Select`
* Aggregate
    * `Sum`, `Average`, ..
    * `Aggregate`

----

### `Where`

```csharp
public static IEnumerable<TSource> Where<TSource>(
    this IEnumerable<TSource> source,
    Func<TSource,bool> predicate);
```

E.g.

```csharp
List<string> fruits =
    new List<string> { "apple", "passionfruit", "banana", "mango",
                    "orange", "blueberry", "grape", "strawberry" };

IEnumerable<string> query =
    fruits.Where(fruit => fruit.Length < 6);
// query contains 
// apple, mango, grape
```

----


### `Select`

```csharp
public static IEnumerable<TResult> Select<TSource,TResult> (
    this IEnumerable<TSource> source,
    Func<TSource,int,TResult> selector);
```

E.g.

```csharp
string[] fruits = { "apple", "banana", "mango", "orange",
                      "passionfruit", "grape" };

var query =
    fruits.Select((fruit, index) =>
                      new Fruit() { index, fruit });
// Query contains
// [ {0, "apple"}, {1, "banana"}, ...]
```

----

### `Aggregate`

```csharp
public static TAccumulate Aggregate<TSource,TAccumulate> (
    this IEnumerable<TSource> source,
    TAccumulate seed,
    Func<TAccumulate,TSource,TAccumulate> func);
```

E.g.

```csharp
string longestName =
    fruits.Aggregate(
            "banana",
            (longest, next) =>
                next.Length > longest.Length 
                    ? next
                    : longest);
// longestName
// Passionfruit
```

---

### Files

Any specific?


---


## References

* [LINQ](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable?view=net-8.0)

