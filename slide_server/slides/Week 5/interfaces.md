<!-- .slide: data-background="#003d73" -->

## Interfaces

### Polymorphism

![AU Logo](./../img/aulogo_uk_var2_white.png "AU Logo") <!-- .element style="width: 200px; position: fixed; bottom: 50px; left: 50px" -->


----

### Agenda

* Interface<br/><!-- .element: class="fragment" -->
    * What are interfaces
    * Interfaces as a type
    * Inheritance vs implementation
* Casting<br/><!-- .element: class="fragment" -->
* Object<br/><!-- .element: class="fragment" -->
* Nullable<br/><!-- .element: class="fragment" -->

---

### Remember abstract classes?

* Define and implement methods<br/><!-- .element: class="fragment" -->
    * some left <mark>un</mark>implemented
    * no instances
* Possible to reuse code and customize<br/><!-- .element: class="fragment" -->

```csharp
public abstract class Child {
    public abstract void Play();
    public void Eat() {
        // implementation
    }
}
public class Girl : Child {
    public override void Play() {
        // implementation
    }
}
```
<!-- .element: class="fragment" -->

----

### Abstract class in UML 

```language-plantuml
@startuml

skinparam classAttributeIconSize 0


Child <|-- Girl


abstract class Child {
  +{abstract} Play():void 
  + Eat():void
}

class Girl {
  + Play(): void
}

@enduml
```

----

## Interfaces

* What if all methods should be abstract?
    * meaning only defining the interface

![bluetooth](./img/bluetooth_pairing.webp "bluetooth")  <!-- .element: style="width: 300px; float: left; margin-left: 100px" class="fragment" -->

![ski](./img/ski_binding.webp "ski ") <!-- .element: style="width: 300px; float: right; margin-right: 100px" class="fragment" -->

----

## Interfaces is excatly that

* <!-- .element: class="fragment" -->Special class with <b>no</b> data and <b>only</b> abstract methods
```csharp
public interface IPoint {
    int X {get; set;}
    int Y {get; set;}
    double Distance(Point to);
}
```
<!-- .element: class="fragment" -->
* <!-- .element: class="fragment" -->Name starts with an <mark>I</mark> (uppercase i)<br/>
* Pascal Case in addition to that<br/><!-- .element: class="fragment" -->
    * Upper case begining word + including first

----

## Implementing an interface

```csharp [7-14]
public interface IPoint {
    int X {get; set;}
    int Y {get; set;}
    double Distance(IPoint to);
}

public class Point : IPoint {
    public int X {get; set;}
    public int Y {get; set;}
    public double Distance(IPoint to) {
        return Math.Sqrt(Math.Pow(to.X - X, 2) 
                    + Math.Pow(to.Y - Y, 2));
    }
}
```

* *Note* methods/properties are public by default

----

# In UML

```language-plantuml
@startuml

skinparam classAttributeIconSize 0


IPoint <|.. Point


interface IPoint {
   + X : int {get; set;}
   + Y : int {get; set;}
   + Distance(IPoint): double
}

class Point {
   + X : int {get; set;}
   + Y : int {get; set;}
   + Distance(IPoint): double
}

@enduml
```

----

## Polymorphism

```csharp
IPoint point = new Point();
IPoint point2 = new Point();
point.Distance(point2);
//
List<IPoint> points = new List<IPoint>();
```

* `IPoint` can reference `Point` or any other class <mark>implementing</mark> the interface `IPoint`

----

## Why interfaces

1. <!-- .element: class="fragment" -->A class can implement <mark>multiple</mark> interfaces<br/>
    * but only <mark>one</mark> class
1. <!-- .element: class="fragment" -->Abstract classes tends include implementation<br/>
    * interface is only a specification
        * changed in C# 8.0 - but **not** covered
1. <!-- .element: class="fragment" -->Interfaces is a <mark>type</mark><br/>
    * code towards an interface
1. <!-- .element: class="fragment" -->Specification is often used between teams<br/>
    * each team adheres to a defined interface

----

### Multiple inheritance

```csharp [4, 14, 15]
interface IPrintable {
    void Print();
}
public class Point : IPoint, IPrintable {
    public int X {get; set;}
    public int Y {get; set;}
    public double Distance(Point to) {
        return 0.0;
    }
    public void Print() {
        // Print 
    }
}
// Inherit from class and implement interface
public class Point: Object, IPoint, IPrintable { }
```

----

```language-plantuml
@startuml

skinparam classAttributeIconSize 0


IPoint <|.. Point
IPrintable <|.. Point

interface IPrintable {
   + Print(): void
}

interface IPoint {
   + X : int {get; set;}
   + Y : int {get; set;}
   + Distance(IPoint): double
}

class Point {
   + X : int {get; set;}
   + Y : int {get; set;}
   + Distance(IPoint): double
   + Print(): void
}

@enduml
```

---

## interfaces extending interfaces

* Interfaces can extend interfaces<br/><!-- .element: class="fragment" -->
* Same as with classes, one interface can extend multiple interfaces<br/><!-- .element: class="fragment" -->
```csharp
interface IArea {
	double Area();
}
interface ICircumreference {
	double Circumreference();
}
interface IShape: IArea, ICircumreference {}
```

----

### Abstract class and interfaces

* Abstract class do not need to implement interface methods
```csharp
public abstract class Shape: IArea, ICircumreference {
	public abstract double Area();
	public abstract double Circumreference();
}
// or
public abstract class Shape: IShape {
	public abstract double Area();
	public abstract double Circumreference();
}
```

----

### Your turn <!-- .element: style="color:#003d73; background-color: #ffffff" -->

Work on starter exercises<br/>  <!-- .element: style="color:#000000; background-color: #ffffff" -->

Continue with Shapes exercises  <!-- .element: style="color:#000000; background-color: #ffffff" -->

<!-- .slide: data-background-image="../img/your_turn.png" -->

---

## Casting

* Casting means converting an object from one type to another<br/><!-- .element: class="fragment" -->
* This can be done in a couple of ways<br/><!-- .element: class="fragment" -->
* <!-- .element: class="fragment" -->First lets look at the <mark>safe</mark> way of casting<br/>
```csharp
object obj = "Hello I'm a String";
if (obj is string str) {
    Console.WriteLine(str.ToLower());
}
```
* Here we cast an object to a string<!-- .element: class="fragment" -->

----

## Alternative casting
### Unsafe<!-- .element: class="fragment" data-fragment-index="0" -->

```csharp
Shape shape = new Circle(); // when can we do this?

Circle? cirle = shape as Circle; // 1 
var circle = (Circle) shape; // 2
```
<!-- .element: class="fragment" data-fragment-index="0" -->

1. <!-- .element: class="fragment" data-fragment-index="1" -->'<mark>as</mark>' casts shape to a circle if possible, otherwise null<br/>
2. <!-- .element: class="fragment" data-fragment-index="2" -->'<mark>(Typename)</mark>' cast the shape to a cicle, or throw an exception<br/>

----

## Nullable

```csharp
int? value = 10
if (value is int v) {}
if (value.HasValue) {
    Console.WriteLine($"{value.Value}")
}
if (value != null) {}
int v = value ?? -1
```
<!-- .element: class="fragment" -->

* Represent the underlaying value or null, meaning<!-- .element: class="fragment" -->
    * `bool?` is either `true`, `false`, and `null`
* Helps us avoid mistakes in our code<!-- .element: class="fragment" -->

---

## Object class


----

### ToString

```csharp
public virtual string? ToString()
```
<!-- .element: class="fragment" data-fragment-index="0" -->

This can be overridden in our classes<!-- .element: class="fragment" data-fragment-index="1" -->

```csharp
public class Student {
    public override string? ToString() {
        return $"Student: AuID: {AuID}, " +
            "Name: {FirstName} {LastName}";
    }
}
```
<!-- .element: class="fragment" data-fragment-index="1" -->

Automatically called when object is transformed to a string<!-- .element: class="fragment" data-fragment-index="2" -->


----

### GetType

```csharp
public extern Type GetType();
```
<!-- .element: class="fragment" -->

* <!-- .element: class="fragment" --><mark>GetType</mark> method returns information about the current object type<br/>
* <!-- .element: class="fragment" --><mark>Type</mark> type holds <mark>runtime</mark> information about the object<br/>
* <!-- .element: class="fragment" --><mark>extern</mark> means that the method is defined elsewhere - performance reasons<br/>

----

### `Equals`

```csharp
public virtual bool Equals(object? obj)
```
<!-- .element: class="fragment" -->
* Used to compare if objects are equal<br/><!-- .element: class="fragment" -->
* <!-- .element: class="fragment" -->On numerical types <mark>Equals</mark> and <code>==</code> operator works as expected<br/>
* <!-- .element: class="fragment" -->In general if not overriden it will not work as <code>==</code> but<br/>
    * will test for <mark>reference equality</mark>, meaning same object

```csharp
Point p1 = new Point(1,1);
Point p2 = new Point(1,1);
Point p3 = p1;
Console.Writeline(p1.Equals(p2)) // will print false
Console.Writeline(p1.Equals(p3)) // will print true
```
<!-- .element: class="fragment" -->

----

### Override `Equals`

* To make `Equals()` behave like you would expect

```csharp [2|3|4-5|9|1-10]
public override bool Equals(object? obj) {
    if (obj == null) return false;
    if (this == obj) return true;
    return obj.GetType() == this.GetType() 
        && Equals((Vector) obj);
}

public bool Equals(Point p) {
    return this.X.Equals(p.X) && this.Y.Equals(p.Y);
}
```

----

### 'Secret' use of `Equals`

* <!-- .element: class="fragment" -->Overriden <code>Equals()</code> can be very usefull and important
```csharp
List<Points> points = new List<Point>();
list.Remove(p2);
```
* <!-- .element: class="fragment" -->Remove the first element where <code>Equals()</code> return true<br/>
* <!-- .element: class="fragment" -->Same with <code>Contains()</code>, <code>IndexOf()</code>
    * basicly all [methods](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1?view=net-8.0) taking a object of `T` as argument

----

### `GetHashCode`

```csharp
public virtual int GetHashCode()
```
<!-- .element: class="fragment" -->
* <!-- .element: class="fragment" --><code>GetHashCode()</code> also have hidden usage as with <code>Equals()</code><br/>
* Generates an aggregate integer from the object<br/><!-- .element: class="fragment" -->
    * Default is an hash code based on the memory location
* This means different instances of an object will in general have different hash codes<br/><!-- .element: class="fragment" -->

----

### Rules for overriding `GetHashCode`

* For mutable reference types (classes)
    1. compute based on immutable fields (not changing)
    1. or ensure that hash code don't change while contained in a collection

----

### Rules for overriding `GetHashCode`

* If two objects are considered equal that should have same hash code<br/><!-- .element: class="fragment" -->
* Should consistently return same hash code - while not being modified<br/><!-- .element: class="fragment" -->
* Distribute hash code evenly<br/><!-- .element: class="fragment" -->
* Be 'cheap' to compute<br/><!-- .element: class="fragment" -->
* Not throw exceptions<br/><!-- .element: class="fragment" -->

----

### Overriding `GetHashCode()`

* Easy to implement with [`HashCode`](https://learn.microsoft.com/en-us/dotnet/api/system.hashcode?view=net-8.0) struct
```csharp
public class Point {
    public override int GetHashCode() {
        return HashCode.Combine(X, Y);
    }
}
```
* Used in dictionaries to store elements (we will come back to this)

---

## References

* [C# object](https://learn.microsoft.com/en-us/dotnet/api/system.object?view=net-8.0)
* [List](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1.add?view=net-8.0)
* [HashCode](https://learn.microsoft.com/en-us/dotnet/api/system.object.gethashcode?view=net-8.0)
