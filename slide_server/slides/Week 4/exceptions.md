<!-- .slide: data-background="#003d73" -->

## Exceptions

### Debugging

![AU Logo](./../img/aulogo_uk_var2_white.png "AU Logo") <!-- .element style="width: 200px; position: fixed; bottom: 50px; left: 50px" -->


![Exceptions](./img//LiningUp4AftermathX50.jpg) <!-- .element: style="width: 500px;"  -->


----

### Agenda


* What are exceptions
* Handling exceptions
* When to use exceptions
* Defining exceptions

---

### Why

* Test for every little thing in our code
* Still unexpected events will happen
* Forces us to handle errors

----

#### Files

* File exists
* Is readable/writable
* Contains the correct data
* Has not changed after we tested it
    * not on windows

----

#### Data input

* Is it available,
* Correct format
* Not illegal values?

---

<!-- .slide: style="font-size: 36px" -->
### What

When unexcpected (exceptions) happens in an application. A special object is created and thrown

* `Exception` is a special class in .NET
    * also classes derived from `Exception`
* CLR (Common Language Runtim&#x1F41E;) populate state for these objects
* An `Exception` has state
    * Message
    * StackTrace
    * InnerException
    * ...

----

### Throw

* Throwing an exception object 
* Means that any statement after the exception is <mark>thrown</mark> is ignored

----

### `NullReferenceException`

```
Unhandled exception. System.NullReferenceException:
         Object reference not set to an instance of an object.
   at ExceptionExamples.Faulty.IsLastString() in
        /ST2ITS2_Material/source/Exception/ExceptionExamples/Faulty.cs:line 25
   at ExceptionExamples.Faulty.GetShortestString() in 
        /ST2ITS2_Material/source/Exception/ExceptionExamples/Faulty.cs:line 10
   at Program.<Main>$(String[] args) in 
        /ST2ITS2_Material/source/Exception/ExceptionExamples/Program.cs:line 8
```
<!-- .element: style="font-size: 18px;" -->


----

### `FileNotFoundException`

```
Unhandled exception. System.IO.FileNotFoundException: Could not find file '/ST2ITS2_Material/source/Exception/ExceptionExamples/bin/Debug/net6.0/NoExistingFile.txt'.
File name: '/ST2ITS2_Material/source/Exception/ExceptionExamples/bin/Debug/net6.0/NoExistingFile.txt'
   at Interop.ThrowExceptionForIoErrno(ErrorInfo errorInfo, String path, Boolean isDirectory, Func`2 errorRewriter)
   at Microsoft.Win32.SafeHandles.SafeFileHandle.Open(String path, OpenFlags flags, Int32 mode)
   at Microsoft.Win32.SafeHandles.SafeFileHandle.Open(String fullPath, FileMode mode, FileAccess access, FileShare share, FileOptions options, Int64 preallocationSize)
   at System.IO.Strategies.OSFileStreamStrategy..ctor(String path, FileMode mode, FileAccess access, FileShare share, FileOptions options, Int64 preallocationSize)
   at System.IO.Strategies.FileStreamHelpers.ChooseStrategy(FileStream fileStream, String path, FileMode mode, FileAccess access, FileShare share, Int32 bufferSize, FileOptions options, Int64 preallocationSize)
   at System.IO.StreamReader.ValidateArgsAndOpenPath(String path, Encoding encoding, Int32 bufferSize)
   at System.IO.File.InternalReadAllLines(String path, Encoding encoding)
   at System.IO.File.ReadAllLines(String path)
   at ExceptionExamples.Faulty.ReadFileIntoString(String name) in /ST2ITS2_Material/source/Exception/ExceptionExamples/Faulty.cs:line 48
   at Program.<Main>$(String[] args) in /ST2ITS2_Material/source/Exception/ExceptionExamples/Program.cs:line 9
```
<!-- .element: style="font-size: 14px;" -->

```
Unhandled exception. System.IO.FileNotFoundException: Could not find file '/ST2ITS2_Material/source/Exception/ExceptionExamples/bin/Debug/net6.0/NoExistingFile.txt'.
File name: '/ST2ITS2_Material/source/Exception/ExceptionExamples/bin/Debug/net6.0/NoExistingFile.txt'
   at 
   ...
   at System.IO.File.ReadAllLines(String path)
   at ExceptionExamples.Faulty.ReadFileIntoString(String name) in 
        /ST2ITS2_Material/source/Exception/ExceptionExamples/Faulty.cs:line 48
   at Program.<Main>$(String[] args) in 
        /ST2ITS2_Material/source/Exception/ExceptionExamples/Program.cs:line 9
```
<!-- .element: style="font-size: 18px;" class="fragment"-->


---

### How

* Just let errors happen
    * &#x25B6; handle somewhere else
* Handle errors when they occur

![Catch](./img/4416446.jpg) <!-- .element: style="width: 500px" -->


----

### Handling errors

```csharp [1,2,4,5,8]
try
{
    faulty.ReadFileIntoString("NoExistingFile.txt");
} catch (FileNotFoundException e)
{
    Console.Error.WriteLine($"File '{e.FileName}' do not exists");
    Console.Error.WriteLine(e.Message);
}
```

----

### Multiple exceptions

* Allowed to catch different exceptions
* Possibility to handle different errors in specific ways

```csharp [1, 5, 9]
try
{
    faulty.ReadFileIntoString("NotExistingFile.txt");
}
catch (FileNotFoundException e)
{
    Console.Error.WriteLine($"File '{e.FileName}' do not exists");
}
catch (ArgumentException)
{
    Console.Error.WriteLine("Filename supplied is empty, retry");
}
```

----

![Exceptions caught](./img/ExceptionHandling.jpg)

----

### Catch all

```csharp [2, 5, 8]
var filename = "NoExistingFile.txt";
try
{
    faulty.ReadFileIntoString(filename);
} catch (Exception e)
{
    Console.Error.WriteLine($"File '{filename}' do not exists");
    Console.Error.WriteLine(e.Message);
}
```

----

### Finally

* Force execution of code even when an error occur

```csharp [1,4,9-13]
MemoryStream stream = null;
try
{
    stream = new MemoryStream();
    faulty.ReadFileIntoString("NotExistingFile.txt");
}
catch (FileNotFoundException e) { // ...
}
finally
{
    if (stream != null)
        stream.Close();
}
```

note:

* Need to close stream to avoid memory leak
* Syntactical sugar
```csharp
stream?.Close();
```

----

### Cannot handle error here

```csharp
MemoryStream? stream = null;
try
{
    stream = new MemoryStream();
    faulty.ReadFileIntoString("NotExistingFile.txt");
}
finally
{
        stream?.Dispose();
}
```

note:

* This is the same as using
* Syntatic sugar in code


---

### When

* No errors in constructor
* Catch exceptions as close to where they are thrown
* If you can't handle exception
    * log and then rethrow
    * never swallow exception (empty catch clause)

![Windows XP](./img/a22.jpg) <!-- .element: style="width: 300px" -->

----

### Empty catch block

* Do **not** leave catch block empty, even when developing
    * It will stay empty
```csharp
try
{
    TestEnoughCoffee();
} catch (NoCoffeeException)
{}
```
* As a minimum put in a log message (or WriteLine)


----

### Re-throw

* May want to log (or simular) close to the origin of error
    * But can't actually handle error at this point

```csharp [7,8]
try
{
    TestEnoughCoffee();
}
catch (NoCoffeeException)
{
    Console.Error.WriteLine("Cant handle coffee problems here");
    throw;
}
```

---

### Creating Exceptions

* Inherite for Exception
    * or one of the other existing exception classes

```csharp [1,11-12]
public class NoCoffeeException : Exception
{
    public int Expected { get; private set; }
    public NoCoffeeException(string message, int expected)
     : base(message)
    {
        Expected = expected;
    }
}

// somewhere else throw this
throw new NoCoffeeException("No coffee given", 30);
```

----

### Nest exceptions

* Exception can be nested to add more context to an error

```csharp [7-9]
try
{
    TestEnoughCoffee();
}
catch (NoCoffeeException e)
{
    throw ArgumentException(
        "Argument given failed in coffee making",
         e);
}
```


---

### References

As they appear

* `InvalidCastException`
* `FormatException`
* `OverflowException`
* `DivideByZeroException`
* `NullReferenceException`
* `Exception`
* `IndexOutOfRangeException`
* `UnauthorizedAccessException`
* `ArgumentException`

----

### Links

* [Front page comic](https://simply-the-test.blogspot.com/2015/07/)