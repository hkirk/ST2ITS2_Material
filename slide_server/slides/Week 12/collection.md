<!-- .slide: data-background="#003d73" -->

# Collections types

## Mutability / immutability

![AU Logo](./../img/aulogo_uk_var2_white.png "AU Logo") <!-- .element style="width: 200px; position: fixed; bottom: 50px; left: 50px" -->

note:
TODO: Husk at export med notes

----

## Agenda

* Lambda<br/><!-- .element: class="fragment" -->
* List <!-- .element: class="fragment" -->
    * IReadOnlyList
* Queue<br/><!-- .element: class="fragment" -->
* Set<br/><!-- .element: class="fragment" -->
* Dictionary<br/><!-- .element: class="fragment" -->

---

## Lambda

* params => body; <!-- .element: class="fragment" data-fragment-index="0"  -->
```csharp
Func<int, int> square = x => x * x;
int result = square(5);
```
<!-- .element: class="fragment" data-fragment-index="0"  -->
* Can be used without assigning to variable (square) <!-- .element: class="fragment" data-fragment-index="1" -->
```csharp
List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
List<int> evenNumbers = numbers.Where(n => n % 2 == 0).ToList();
```

----

### Lambda types

* Lambda that returns a value<!-- .element: class="fragment" data-fragment-index="1" -->
    * Func<...,TReturn>
* Lambda that do not return anything<!-- .element: class="fragment" data-fragment-index="2" -->
```csharp
Action<string> printMessage =
    message => Console.WriteLine(message)
```
<!-- .element: class="fragment" data-fragment-index="2" -->
* Lambda that returns a boolean<!-- .element: class="fragment" data-fragment-index="3" -->
```csharp
Predicate<int> isEven =
    number => number % 2 == 0;
```
<!-- .element: class="fragment" data-fragment-index="3" -->

---

![Collections](./img/Colecciones-c-1.jpg "")

----

## `List<T>`

This we have already seen<!-- .element: class="fragment" -->

* Encapsulates an array<br/><!-- .element: class="fragment" -->
* Resizes when necessary<br/><!-- .element: class="fragment" -->
* When to use<br/><!-- .element: class="fragment" -->
    * size is no fixed over timer
    * access elements by index
    * or iterate

----

## `List` methods

* [List methods](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1?view=net-8.0#methods)
    * ...
    * Find(Predicate\<T\> p): T<br/><!-- .element: class="fragment" -->
    * ForEach(Action\<T\> a): void<br/><!-- .element: class="fragment" -->
    * Sort(): void<br/><!-- .element: class="fragment" -->
    * IndexOf(T t): int - Remember equals?<br/><!-- .element: class="fragment" -->


----

### `IReadOnlyList<T>`

* Sometimes you need a collection that is unchangable (immutable)<br/><!-- .element: class="fragment" -->
    1. clients don't need to change
    1. fewer option of usage for client

```csharp
List<int> ints = new List<int>();
// What and why??
IReadOnlyList<int> readOnlyList = (IReadOnlyList<int>) ints;
IReadOnlyList<int> readOnlyListCopy = ints.AsReadOnly();
```
<!-- .element: class="fragment" -->

---

## `Queue<T>`

![Queue](./img/queue.avif "Queue")

* Generic collection type (like List\<T\>)<br/><!-- .element: class="fragment" -->
* First in, First out (FIFO)<br/><!-- .element: class="fragment" -->

----

### `Queue` Methods

* Enqueue(T t): void<!-- .element: class="fragment" -->
    * Adds a new element to the end
* Peek(): T<!-- .element: class="fragment" -->
    * Returns first element without altering
* Dequeue(): T<!-- .element: class="fragment" -->
    * Returns first element and removes

----

## When to use

* When we want elements in a specific order<br/><!-- .element: class="fragment" -->
* Efficient mutatation<br/><!-- .element: class="fragment" -->
* Dynamic size - liked List<br/><!-- .element: class="fragment" -->
* Alternative to FIFO<br/><!-- .element: class="fragment" -->
    * `Stack<T>` - Last in, First out (LIFO)
    * `PriorityQueue<TE, TP>` - out, based on Priority

---

## `ISet<T>`

* Stores only unique values<br/><!-- .element: class="fragment" -->
* No order<br/><!-- .element: class="fragment" -->
* Implementations<br/><!-- .element: class="fragment" -->
    * `HashSet<T>` - based on ([Object.GetHashCode](https://learn.microsoft.com/en-us/dotnet/api/system.object.gethashcode?#system-object-gethashcode)))
    * `SortedSet<T>` - ([IComparable.Compare](https://learn.microsoft.com/en-us/dotnet/api/system.icomparable.compareto?#system-icomparable-compareto(system-object)))
    * `FrozenSet<T>` - immutable

----

### `HashSet`

* Dynamic size<br/><!-- .element: class="fragment" -->
* Elements are added/retrived by GetHashCode and Equals<br/><!-- .element: class="fragment" -->
    * Remember [`GetHashCode`](../Week%205/\#/5/6)? 
    * Remember `Equals`?
    * [Object](http://localhost:8010/slides/Week%205/#/4)

![HashSet](./img/hash_set.png "") <!-- .element: class="fragment" -->

----

### `HashSet` Methods

* <!-- .element: class="fragment" --><code>Add(T t)</code>, <code>Remove(T t)</code>, <code>Contains(T t)<code><br/>
* Set methods<br/><!-- .element: class="fragment" -->
    * `Overlaps(IEnumerable<T> other)`
    * `UnionWith(IEnumerable<T> other)`
    * ....

----

### When to use a ISet

* To remove duplicates from a list (or other collection)<br/><!-- .element: class="fragment" -->
    * List -> Set -> List
* To make sure there are no duplicates<br/><!-- .element: class="fragment" -->
* Fast lookup of element<br/><!-- .element: class="fragment" -->

----

### `SortedSet`

* Sorted by natural order (IComparable<T>)

```csharp
public interface IComparable<T> {
    int CompareTo(T t);
}
```

----

### IComparable 'rules'

* `int CompareTo(T t)`
    * <!-- .element: class="fragment" --><b>< 0</b> - This object precedes the object specified by the CompareTo method in the sort order.<br/>
    * <!-- .element: class="fragment" --><b>== 0</b> - This current instance occurs in the same position in the sort order as the object specified by the CompareTo method argument.<br/>
    * <!-- .element: class="fragment" --><b>> 0</b> - This current instance follows the object specified by the CompareTo method argument in the sort order.<br/>

note:

```csharp
public class Word: IComparable<Word> {
    public readonly string word;
    public Word(string word) {
        this.word = word.ToLower();
    }

    public int CompareTo(Word? other) {
        if (ReferenceEquals(this, other))
            return 0;
        if (other == null) 
            return 1;
        return word.CompareTo(other.word);
    }
}
```

---

## `Dictionary<TKey, TEle>`

* Almost like as Set<br/><!-- .element: class="fragment" -->
    * but with a key and a value
    * Key determines uniqueness
    * Value is bound to Key

![Nudansk ordbog](./img/nudansk.jpeg "")<!-- .element: class="fragment" -->

----

### Dictionary example

```csharp
public class Nudansk {
    private Dictionary<Word, Meaning> words =
                new Dictionary<Word, Meaning>();
	public Nudansk() {
		words.Add(new Word("Kilde (vb)"), new Meaning("..."));
        words[new Word("Kilde (sb)")] = new Meaning("...");
	}
    public Meaning GetMeaning(string word) {
        return words[new Word(word)];
    }
    public void Print() {
		foreach (KeyValuePair<Word, Meaning> keyValue
                                         in words) {
			Console.WriteLine(keyValue.Key
                     + " -> " + keyValue.Value);
		}
	}
}
```

note:

```csharp
public class Meaning {
    public string Description { get; private set; }
    public Meaning(string description) {
        Description = description;
    }
}
public class Word: IComparable<Word> {
    public readonly string word;
    public Word(string word) {
        this.word = word.ToLower();
    }
	
    public override bool Equals(object? other) {
        if (other is Word)
        {
            Word otherWord = other as Word;
            return word.Equals(otherWord.word);
        }

        return false;
    }
    public override int GetHashCode() {
        return word.GetHashCode();
    }
}

public class Nudansk {
    private Dictionary<Word, Meaning> words = new Dictionary<Word, Meaning>();
	
    public Nudansk() {
        words.Add(new Word("Kilde"), new Meaning("vand fra jordbunden der strømmer eller springer ud på overfladen"));
        words[new Word("Kilde")] = new Meaning("person som har videregivet oplysninger til fx presse eller politi");
		
    }
    public Meaning GetMeaning(string word) {
        return words[new Word(word)];
    }
    public void Print() {
        foreach (KeyValuePair<Word, Meaning> keyValue in words) {
            Console.WriteLine(keyValue.Key + " -> " + keyValue.Value);
        }
    }
}

```

----


## Use dictionary whey

* Key/value pair<br/><!-- .element: class="fragment" -->
* Fast lookup<br/><!-- .element: class="fragment" -->
* Keys are unique<br/><!-- .element: class="fragment" -->

---

## hierarchy

![Hierarchy](./img/collection-overview-1-i1.png "")

---

## Reference

* [IReadOnlyList](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1?view=net-8.0)
* [Queue](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.queue-1?view=net-8.0)
    * [With priority](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.priorityqueue-2?view=net-8.0)
* [Set](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.iset-1?view=net-8.0)