<!-- .slide: data-background="#003d73" -->

## Inheritance

![AU Logo](./../img/aulogo_uk_var2_white.png "AU Logo") <!-- .element style="width: 200px; position: fixed; bottom: 50px; left: 50px" -->

----

### Agenda

* Catchup
* Inheritance in C#
* Pylymorphsm
* Abstract classes


---

### Immutable

* The ability to set a value once - when the object is created
    * But never to change it again

```csharp
public class Person {
    public readonly String CPR;
    public int Age { get; }
    public Person(string cpr, int age) {
        CPR = cpr; // Allowed to set value once and only once
        Age = age;
    }
    public void SetCPR(string cpr) {
        //CPR = cpr; // will not compile
    }
    public void SetAge(int age) {
        //Age = age; // will not compile
    }
}
```

----

### Visibility

* `public`
    * Other classes and project can access
* `internal`
    * Other classes in same project can access
    * Visual Studio defaults to this
* `private`
    * Only the class it self can access
* No modifier means internal (for classes and structs)

----

```csharp
public class Person {
    private string _cpr;
    string GetCPR() {
        return _cpr;
    }
    internal string GetCPR1() { // same as above
        return _cpr;
    }
    
}
```

----

### Structs

* Definded with keyword `struct`
    ```csharp
    public struct Point2D {
        public int X {get;}
        public int Y {get;}
        // constructor
    }
    ```
* Used for small data-centric classes - with little or no behavoir
* As values - like int, string, ...
* *Value* vs *Reference* semantic :)

---

## Problemm

* Reuse of code
    * Increase efficiency
    * Improve quality
    * Consistency
    * Modularity
    * Collaboration
* Extensability
* Polymorphism

----

### Reuse in OOP 

* Depend on same class from multiple classes
* Delegate work to this class (`Hammer`)

```language-plantuml
@startuml
Surgeon --> Hammer
BrickLayer --> Hammer

skinparam classAttributeIconSize 0
class Hammer {
    +HitWithForce(object)
}
class Surgeon {
    -_hammer: Hammer
    +BreakBone(Bone)
}
class BrickLayer {
    -_hammer: Hammer
    +BreakBrick(Brick)
} 

@enduml
```

----

### In C# 

```csharp
public class Hammer {
    public void HitWithForce(object item) {
        Console.WriteLine($"Hit {item} with force");
    }
}
public class BrickLayer {
    private Hammer _hammer;
    public BrickLayer(Hammer hammer) {
        _hammer = hammer;
    }
    public void BreakBrick(Brick brick) {
        _hammer.HitWithForce(_brick);
    }
}
```

----

### Using the hammer

```csharp
public static void Main() {
    Hammer hammer = new Hammer();
    BrickLayer bl = new BrickLayer(hammer);
    bl.BreakBrick(new Brick());
}
```

ouputs:

```
Hit Brick with force
```


---

## Inheritance in C# 

![Simba](./img/simba.png "") <!-- .element: style="height: 400px" -->

----

### In UML

* Mechanism that allows derieved classes to inherit <mark>properties</mark> and <mark>methods</mark> from base class
* First we focus on syntax
    * then we talk about when its appropriate to inherit
    * warning silly example

```language-plantuml
@startuml
Hammer <|-- Surgeon
Hammer <|-- BrickLayer

skinparam classAttributeIconSize 0
class Hammer {
    +HitWithForce(object)
}
class Surgeon {
    +BreakBone()
}
class BrickLayer {
    +BreakBrick()
} 

@enduml
```

----

### Derieve from base class

Syntax to inherit<br/>
 `class <NameOfClass> : <NameOfBase>`

```csharp [1,7| 2, 9|1-2,7,9]
public class Hammer {
    public void HitWithForce(object item) {
        Console.WriteLine($"Hit {item} with force");
    }
}

public class BrickLayer : Hammer {
    public void BreakBrick(Brick brick) {
        HitWithForce(brick);
    }
}
```

----

### Using the new BrickLayer

```csharp [1-3|4-6|1-6]
BrickLayer bl = new BrickLayer();
bl.BreakBrick(new Brick());
bl.HitWithForce(new Brick()); // is also possible
Hammer hammer = new BrickLayer();
hammer.HitWithForce(new Brick());
//hammer.BreakBrick(new Brick()); // Not possible

```

* Notice that there now is no need to instantiate the `Hammer`.
* `BrickLayer`` <mark>is-a</mark> hammer

----

### Constructing base objects

* Reference the `base` class when constructing

```csharp [3-5, 11]
public class Hammer {
	private int _force;
	public Hammer(int force) {
		_force = force;
	}
    public void HitWithForce(object item) {
        Console.WriteLine($"Hit {item} with {_force}");
    }
}
public class BrickLayer : Hammer {
	public BrickLayer(): base(10) {}
    public void BreakBrick(Brick brick) {
        HitWithForce(brick);
    }
    private Hammer _hammer;
}
```

----

### Constructing a BrickLayer

* Constructing of `BrickLayer` means constructing `Hammer`
* The `BrickLayer` needs to call the `Hammer` constructor
* Why only when base class has a <mark>non-empty</mark> constructor? <!-- .element: class="fragment" data-fragment-index="1"  -->

----

### Empty constructors

```csharp [1,2,6]
public class Hammer : object {
	public Hammer() : base() {}
}

public class BrickLayer : Hammer {
	public BrickLayer() : base() {}
}
```

* These constructors are <mark>genereted</mark> for us
    * \+ all classes inherits from `object`

---

## When to use

* `BrickLayer` and `Surgeon` is now depeneded on inner workings of `Hammer`
* Extending `Hammer` with method `CalculatePrice()` based on materials
    * then this apply to derived classes


```language-plantuml
@startuml
Hammer <|-- Surgeon
Hammer <|-- BrickLayer

skinparam classAttributeIconSize 0
class Hammer {
    + Hammer(int)
    +HitWithForce(object)
    +CalculatePrice()
}
class Surgeon {
    +Surgeon(int)
    +BreakBone()
}
class BrickLayer {
    +BrickLayer(int)
    +BreakBrick()
} 

@enduml
```

----

### When to use

```csharp
Hammer hammer = new BrickLayer();
```
* How should we as developers make sense of the `hammer` object?
* Derieved classes are forced to inherit from `base` classe

Inheritance can give all sorts of problems - especially as the code <mark>evolves</mark> - and it will

----

### Sensible inheritance?

```language-plantuml
@startuml

Tool <|-- Hammer
Tool <|-- Saw
Tool <|-- Axe

skinparam classAttributeIconSize 0
class Tool {
    -price
    -tax
    -discount
    +Price()
}

@enduml
```

```language-plantuml
@startuml

Meteor <|-- Car
Meteor <|-- Plane

skinparam classAttributeIconSize 0
class Meteor {
    -maxVelocity
}

@enduml
```

----

### Sensible inheritance?


```language-plantuml
@startuml

Body <|-- Fluid
Body <|-- RigidBody
RigidBody <|-- Surgeon
RigidBody <|-- Hammer
Fluid <|-- Oil

skinparam classAttributeIconSize 0
class Body {
    -mass
    -volumne
    +dencity()
}

@enduml
```

```language-plantuml
@startuml

Person <|-- Student
Person <|-- Teacher

skinparam classAttributeIconSize 0
class Person {
    -name
    -cpr
    -salery
}

@enduml
```

----

### Are these sensible hierarchies

![In 2-2 or 3-3](https://media.giphy.com/media/v1.Y2lkPTc5MGI3NjExcGNwYWV4MGczMWdrajQ1MnQxams2OXZrNzU1eXJvcHZ4dW1hNnI4YyZlcD12MV9pbnRlcm5hbF9naWZfYnlfaWQmY3Q9Zw/3o6MbsWjZwURvye0ko/giphy.gif "")


---

### object class

* Object class has 3 important methods
    * `string ToString()`
    * `int GetHashCode()`
    * `bool Equals(object? obj)`
* These can be overriden in your classes
    * meaning your class can <mark>change</mark> these methods behavior
    * eg.

```csharp
public override string ToString()
{
    return $"{FirstName} {LastName}";
}
```

----

### virtual methods

* These methods in `object` are defined so they can be overriden.
* This are done with the `virtual` keyword

```csharp [3,8-10]
public class Hammer {
    ...
    public virutal void HitWithForce(object item) {
        Console.WriteLine($"Hit {item} with {_force}");
    }
}
public class BrickLayer : Hammer {
    public override void HidWithForce(object item) {
        Console.WriteLine($"Hit {item} with {_force} with a brick hammer");
    }
}
```

----

### Override without `virtual`

* What if you don't have <mark>control</mark> over base class?
    * E.g. the base class is delivered by someone else?

```csharp
public class Hammer { // Defined by 
    ...
    public void HitWithForce(object item) {
        Console.WriteLine($"Hit {item} with {_force}");
    }
}
public class BrickLayer : Hammer { // Written by us
    public void HitWithForce(object item) {
        Console.WriteLine($"Hit {item} with {_force} with "
            + "a brick hammer");
    }
}
```

----

### Output

```csharp
Hammer hammer = new BrickLayer();
hammer.HitWithForce(new object()); // What happens?
```

<mark>"Hit Brick with 10 with a brick hammer"</mark><!-- .element: class="fragment" data-fragment-index="0" -->


```csharp
BrickLayer brickLayer = new BrickLayer();
brickLayer.HitWithForce(new object()); // What about this?
```
<!-- .element: class="fragment" data-fragment-index="0" -->

<mark>"Hit Brick with 10"</mark><!-- .element: class="fragment" data-fragment-index="1" -->

Why is there a difference?<!-- .element: class="fragment"  data-fragment-index="1" -->

----

### Hide method with `new`

* The compiler is actually helping us?
* And telling us how to be specific about this

```
Warning CS0108 'BrickLayer.HitWithForce' hides inherited
member 'Hammer.HitWithForce'. Use the new keyword if hiding
was intended.
```

```cshar [2]
public class BrickLayer : Hammer { // Written by us
    public new void HidWithForce(object item) {
        Console.WriteLine($"Hit {item} with {_force} with "
         + " a brick hammer");
    }
}
```

----

### `protected` visibility

* Sorry I actually cheated in the above example &#x1FAE3;

```csharp
public class Hammer {
	private int _force;
    ...
}
public class BrickLayer : Hammer { // Written by us
    public new void HidWithForce(object item) {
        Console.WriteLine($"Hit {item} with {_force} with "
         + " a brick hammer");
    }
}
```

This does not compile!! Why not?

----

### Visibility

* `public`
    * Other classes and project can access
* `internal`
    * Other classes in same project can access
    * Visual Studio defaults to this
* `private`
    * Only the class it self can access
* `protected`
    * Only class itself and derieved classes can access
* No modifier means internal (for classes and structs)

----

### Protected in UML

```language-plantuml
@startuml

Hammer <|-- BrickLayer

skinparam classAttributeIconSize 0
class Hammer {
    #force: int
}
class BrickLayer {
    
}

@enduml
```

```csharp [2]
public class Hammer {
	protected int _force;
    ...
}
public class BrickLayer : Hammer {
	...
	public new void HitWithForce(object item) {
		Console.WriteLine($"Hit {item} with {_force} with"
            + " a brick hammer");
	}
}

```

---

### Polymorphism

* We have already seen this in Hammer
```csharp
Hammer hammer1 = new Hammer()
Hammer hammer2 = new BrickLayer();
```
* The hammer type is Polymorph

----

### Better example 

```language-plantuml
@startuml

Person <|-- Student
Person <|-- Teacher
Person <|-- TeachingAssistent
Brightspace -r-> Person

skinparam classAttributeIconSize 0
class Person {
    -name
    -auid
}
class Brightspace {
    -persons: List<Person>
}

@enduml
```

* Brightspace only has knowledge about Person - where it aggregates these
* Don't know anything about `Student`, `Teacher`, or `TeachingAssistent`

----

### Polymorhism

What happens to the <mark>Brightspace</mark> class?
```language-plantuml
@startuml

Person <|-- Student
Person <|-- Teacher
Person <|-- TeachingAssistent
Person <|-- VisitingTeacher
Brightspace -r-> Person

skinparam classAttributeIconSize 0
class Person {
    -name
    -auid
}
class Brightspace {
    -persons: List<Person>
}

@enduml
```

---

### Abstract classes

![Last thing](https://media.giphy.com/media/v1.Y2lkPTc5MGI3NjExMXI2NzdwcWdhMHkxcjhhZGlwZmZraHRyN2FrZTI0Yms1dGNxaXZsMiZlcD12MV9pbnRlcm5hbF9naWZfYnlfaWQmY3Q9Zw/T2jS77x0wizeIp5qSt/giphy.gif "")

----

### Abstract classes

Does the <mark>Person</mark> class make sense in this context?

```language-plantuml
@startuml

Person <|-- Student
Person <|-- Teacher
Person <|-- TeachingAssistent
Person <|-- VisitingTeacher
Brightspace -r-> Person

skinparam classAttributeIconSize 0
class Person {
    -name
    -auid
}
class Brightspace {
    -persons: List<Person>
}

@enduml
```

This is properly to general a class to have in this context <!-- .element: class="fragment" data-fragment-index="1"  -->

----

### Abstract Person

```language-plantuml
@startuml

Person <|-- Student
Person <|-- Teacher
Person <|-- TeachingAssistent
Person <|-- VisitingTeacher
Brightspace -r-> Person

skinparam classAttributeIconSize 0
abstract class Person {
    #name
    -auid
    +{abstract} string GetName()
}
class Brightspace {
    -persons: List<Person>
    
}
@enduml
```

* Person is now abstract, which means
    * We <mark>cannot</mark> create instances of this
    * We <mark>can</mark> have methods/properties without implementation
        * these are `virtual` and must be `override`n

----

### In C#

```csharp [1,4,7|9,13|1,4,7,9,13]
public abstract class Person {
	protected string name;
	private string auid;
	protected Person(string auid) {
		this.auid = auid;
	}
	public abstract string GetName();
}
public class Student : Person {
	public Student(string auid, string name): base(auid) {
		base.name = name;
	}
	public override string GetName() {
		return base.name;
	}
}
```

* `GetName()` is abstract and have not implemention in Person
     * is overriden in Student


----

### Abstract from clients

```csharp [2, 4-6, 9-11]
public class Brightspace {
	private List<Person> _persons = new List<Person>();
	
	public void addPerson(Person person) {
		_persons.Add(person);
	}
	
	public void PrintPersons() {
		foreach (Person person in _persons) {
			Console.WriteLine(person.GetName());
		}
	}
}
```

* Here we don't see any difference

---

### Code example - Hammer

```csharp
using System;

BrickLayer bl = new BrickLayer();
bl.BreakBrick(new Brick());
bl.HitWithForce(new Brick()); // is also possible
Hammer hammer = new BrickLayer();
hammer.HitWithForce(new Brick());
//hammer.BreakBrick(new Brick()); // Not possible

public class Brick {}
	
public class Hammer {
	protected int _force;
	
	public Hammer(int force) {
		_force = force;
	}
    public void HitWithForce(object item) {
        Console.WriteLine($"Hit {item} with {_force}");
    }
}

public class BrickLayer : Hammer {
	public BrickLayer(): base(10) {}
	public new void HitWithForce(object item) {
		Console.WriteLine($"Hit {item} with {_force} with a brick hammer");
	}
    public void BreakBrick(Brick brick) {
        HitWithForce(brick);
    }
}
```


---

### Code example Person

```csharp
using System.Collections.Generic;
using System;

Brightspace bs = new Brightspace();
bs.AddPerson(new Student("au1234", "Nick Cave"));
// bs.AddPerson(new Person("au1234")); // dont' compile


public abstract class Person {
	protected string name;
	private string auid;
	protected Person(string auid) {
		this.auid = auid;
	}
	public abstract string GetName();
}

public class Student : Person {
	public Student(string auid, string name): base(auid) {
		base.name = name;
	}
	public override string GetName() {
		return base.name;
	}
}

public class Brightspace {
	private List<Person> _persons = new List<Person>();
	
	public void addPerson(Person person) {
		_persons.Add(person);
	}
	
	public void PrintPersons() {
		foreach (Person person in _persons) {
			Console.WriteLine(person.GetName());
		}
	}
}
```