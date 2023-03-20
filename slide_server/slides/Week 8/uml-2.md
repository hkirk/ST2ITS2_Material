<!-- .slide: data-background="#003d73" -->

## UML

### Sequence diagrams

![AU Logo](./../img/aulogo_uk_var2_white.png "AU Logo") <!-- .element style="width: 200px; position: fixed; bottom: 50px; left: 50px" -->

----

### Agenda

* Class types
* Sequence diagrams

---

## Abstract & Interface

!["Abstract and interface](../Week%206/img/interface%20and%20abstract.png "")

----

### Abstract classes

* Class name is *italic*
* Abstract methods is *italic* or
    * `{abstract}`
* Remember Abstract class cannot be instantiated
* Remember classes inherits from abstract classes

----

### Abstract class

![Abstract class](./img/abstract.png "Abstract class")

----

### Interfaces

* Class name is 'marked' with `<<interface>>`
* Remember all methods in an interfaces is considered 'abstract'\*


\* This is a lie from C# 8.0 :(

----

### Interfaces

!["Interfaces"](./img/interface.png "")

!["Lollypop"](./img/ballAndSocket.png "Lollypop")


---

## Sequence Diagrams

Try describing a complex call from with 3-4 classes to your neighbour

----

### Solution

* Simple diagram type
    * shows participant involved
    * interaction between these
* Notation is easy to understand

----

![Sequence Diagram example](./img/sequences.png "Example") <!-- .element: style="height: 650px" -->

----

### Explanation

Contains

* Participants with lifelines
* Method calls
    * and return arrows
* activation bars

----

### Object creations

![Object life](./img/object_lifeline.png "") <!-- .element: style="height: 600px" -->

----

### Loop and alternatives

![Alternative and loop](./img/alternatives.png "") <!-- .element: style="height: 600px" -->

----

### Operators

* **alt** Alternative, multiple fragment - one will execute
* **opt** Optional, fragment may execute
* **loop** Loop, fragment may execute multiple times
* **par** Parrallel, fragments will execute in parallel (next semester)

---

## References