<!-- .slide: data-background="#003d73" -->

## MVVM

### Model-View-ViewModel

![UI changes](https://imgs.xkcd.com/comics/ui_change.png "")

![AU Logo](./../img/aulogo_uk_var2_white.png "AU Logo") <!-- .element style="width: 200px; position: fixed; bottom: 50px; left: 50px" -->

----

## Agenda

* Problem<br/><!-- .element: class="fragment" -->
* MVVM<br/><!-- .element: class="fragment" -->
* Other GUI Patterns<br/><!-- .element: class="fragment" -->

---

### `ListBox`

[ListView](https://learn.microsoft.com/en-us/dotnet/maui/user-interface/controls/listview?view=net-maui-8.0 "ListView")


```xml
// In XAML file
<ListView ItemsSource="{Binding Source={x:Reference Name=MyPageRef}, Path=Monkeys}" />
```

* Shows a list of selectable items in a MAUI application<br/><!-- .element: class="fragment" -->
* Items can be static or dynamic.<br/><!-- .element: class="fragment" -->
    * Above examples is dynamic.

----

### `ObservableCollection<T>`

[ObservableCollection<T>](https://learn.microsoft.com/en-us/dotnet/api/system.collections.objectmodel.observablecollection-1?view=net-8.0)

* <!-- .element: class="fragment" -->A dynamic collection like List<T><br/>
* Provides notifications when the list is mutated (changed)<br/><!-- .element: class="fragment" -->
    * E.g. `Add`, `Remove` and `Clear` operations
* In MAUI this means that bindings are automaticaly updated<br/><!-- .element: class="fragment" -->

---

## My favorite example :)


![ECG Monitor](./img/ecg_monitor.webp "") <!-- .element: style="height: 300px" -->

- With our current tools, can we make two apps<!-- .element: class="fragment" -->
    1. for operation setup and
    2.  a practitioner consultation? 

----

### What can we reuse?

* Hopefully we could reuse<br/><!-- .element: class="fragment" -->
    * Data access code
    * Business logic code
* UI Forms and controls<br/><!-- .element: class="fragment" -->
    * We can properly reuse the forms and controls
* What a about alarms - showing when the presure is to high or to low<br/><!-- .element: class="fragment" --> 
    * And still have these testable - *I know we don't have test in this course.*

note: 

Operation: sys < 180
Practitionel: 140/90

Sys: high
Dia: low

----

## Problem

- We mix up logic and view in the code-behind file<br/><!-- .element: class="fragment" -->
    - XAML is for design/ux specialist
    - XAML.cs is for developers
- We cannot test the Code-Behind file easily<br/><!-- .element: class="fragment" -->
    - its compiled with the view
- Hard to reuse, again tightly coupled to the specific view<br/><!-- .element: class="fragment" -->


----

### Solution



- We need a specific place to define UI specific logic.
- Like:
    - Alarms<br/><!-- .element: class="fragment" -->
    - Colors<br/><!-- .element: class="fragment" -->
    - Messages<br/><!-- .element: class="fragment" -->


---

## MVVM

![MVVM dependencies](./img/mvvm.png "MVVM") <!-- .element: style="float: right; margin-right: 200px" -->

* **M**odel<br/><br/>
* **V**iew<br/><br/>
* **V**iew**M**odel

----

### Data flow

![MVVM Data flow](./img/mvvm-data.png "MVVM data flow")

----

### Bindings 

* View is updated with data from ViewModel<br/><!-- .element: class="fragment" -->
    * Updated through data bindings
* This can be Two-way - meaning ViewModel is updated from View and back<br/><!-- .element: class="fragment" -->
    * Some widgets are TwoWay: Date, Text...
    * [Binding declaration](https://learn.microsoft.com/en-us/dotnet/maui/fundamentals/data-binding/binding-mode?view=net-maui-8.0)
* Overriding default<br/><!-- .element: class="fragment" --> 
```xml
<Label Text="TEXT"
       Scale="{Binding Path=Value,
                       Mode=TwoWay}" />
```

----

### Commands

* Communication from View to ViewModel<br/><!-- .element: class="fragment" -->
* <!-- .element: class="fragment" -->ICommand exists in .NET for this purpose<br/>
    * `Execute(object)` called when command is actuated 
    * `CanExecute(object)` returns boolen and determines if UI 'unlock' command
    * `CanExecuteChanged` should be raised when 'CanExecute` should be reevaluated
* <!-- .element: class="fragment" -->Command is an implementation of this interface<br/>
```xml
<Button 
    Command="{Binding CalcBMICommand}"/>
```

<!-- .slide: style="font-size: 34px" -->

---

#### Model in C#

BMI calculator

```csharp
public class BMIModel
{
    public double Weight { set; get; }

    public double Height { set; get; }

    public double CalculateBMI()
    {
        return Weight / (Height * Height);
    }
}
```

----

#### BindingContext in C#

Creating a ViewModel 

```csharp
public class BMIViewModel : INotifyPropertyChanged {
}
```

Defining a DataContext

```xaml [4,6-8]
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:local="clr-namespace:BmiCalculator"
             x:Class="BmiCalculator.MainPage">
       <ContentPage.BindingContext>
              <local:BMIViewModel />
       </ContentPage.BindingContext>
</ContentPage>
```

----

#### ViewModel in C# 

INotifyPropertyChanged implementation

```csharp [3, 5-13]
public class BMIViewModel : INotifyPropertyChanged {
    ...
    public event PropertyChangedEventHandler PropertyChanged;

    private void OnPropertyChanged(
        [CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this,
            new PropertyChangedEventArgs(propertyName));
    }
}
```

----

#### ViewModel Binding

Properties

```csharp [3-12]
public class BMIViewModel : INotifyPropertyChanged {
    BMIModel bmiModel = new BMIModel();
    public double Height {
        get { return bmiModel.Height; }
        set {
            if (value != bmiModel.Height)
            {
                bmiModel.Height = value;
                OnPropertyChanged();
            }
        }
    }
    ...
}
```

----

#### ViewModel Read-Only

Read-only property

```csharp [3-6, 10-11]
public class BMIViewModel : INotifyPropertyChanged {
    ...
    double bmi;
    public double BMI {
        get { return bmi; }
    }

    private void CalcBMI()
    {
        bmi = bmiModel.CalculateBMI();
        OnPropertyChanged("BMI");
    }
    ...
}
```

----

#### Bingings in XAML

Input data

```xaml [4-6,10-12]
<ContentPage ..>
    ...
    <Entry ...
        Text="{Binding Path=Weight,
               StringFormat='{0:F1}',
               Mode=TwoWay}"
        />
    ...
    <TextBox ...
        Text="{Binding BMI,
               Mode=OneWay,
               StringFormat='{0:F1}'}" />
    ...
</ContentPage>
```

----

#### ViewModel in C# 

`Commands`

```csharp [3,6-7]
public class BMIViewModel : INotifyPropertyChanged {
    ...
    public ICommand CalcBMICommand { get; set; }
    public MainPageViewModel()
    {
        CalcBMICommand =
            new Command(CalcBMI, CalcBMICanExecute);
    }
    private bool CalcBMICanExecute() {
        if (Weight != 0.0 && Height != 0.0)
            return true;
        else
            return false;
    }
    ...
}
```

----

### Command state update

Update button state

```csharp [10]
public double Weight
{
  get { return calculator.Weight; }
  set
  {
    if (value != calculator.Weight)
    {
      calculator.Weight = value;
      OnPropertyChanged();
      (CalcBMICommand as Command)?.ChangeCanExecute();
    }
  }
}
```

----

#### Commands in XAML

Executing commands

```xaml [3-5]
<Window ..>
    ...
    <Button ...
        Command="{Binding CalcBMICommand, Mode=OneTime}"
        />
    ...
</Windows>
```

---

## MVVM and 3-layers achitecture

![MVVM vs 3 layered arhitecture](./img/mvvm-in-arhitecture.png "")


----

## Other GUI Patterns

----

### MVC

![MVC](https://www.martinfowler.com/eaaDev/uiArchs/mvc-deps.gif "MVC")

* <!-- .element: class="fragment" --><b>M</b>odel-<b>V</b>iew-<b>C</b>ontroller<br/>
* Properly the most cited UI pattern<br/><!-- .element: class="fragment" -->
    * Most wrongly cited pattern
* Used when there are a clear seperations between control and view<br/><!-- .element: class="fragment" -->

----

### MVC 

![IPod](https://i.insider.com/5e1c6e6eb2e66a3a1c350bc2?width=1280 "Ipod") <!-- .element: style="height: 500px" -->

----

### MVP

![MVP](./img/mvp.png "MVP")

----

#### MVP

* <!-- .element: class="fragment" --><b>M</b>odel-<b>V</b>iew-<b>P</b>resenter<br/>
* Used for the same things as MVVM<!-- .element: class="fragment" -->
    * introduced before bindings
    * still used in frameworks without view-bindings.


---

## References

* [UI changes](https://xkcd.com/1770/)
* [BMI calcalator by Poul Ejnar Rovsing](https://pure.au.dk/portal/en/persons/poul-ejnar-rovsing(252c9404-1c55-4a92-bd0c-97a201cd414a).html)
* [GUI Architectures](https://www.martinfowler.com/eaaDev/uiArchs.html)


---

<!-- .slide: style="font-size: 32px" -->

## MVVM: Key Takeaways

- **Separation of Concerns**:
  - **Model**: Handles data and business logic.
  - **View**: Defines UI and user interactions.
  - **ViewModel**: Acts as a bridge between Model and View, exposing data and commands.

- **Data Binding**:
  - Automatically syncs View and ViewModel.
  - Supports `OneWay` and `TwoWay` binding modes.

- **Commands**:
  - Enables user actions (e.g., button clicks) to trigger ViewModel logic.
  - Uses `ICommand` for decoupled event handling.

----

### Why MVVM?

- **Testability**:
  - ViewModel logic can be unit-tested independently of the UI.
- **Reusability**:
  - ViewModels and Models can be reused across different Views.
- **Maintainability**:
  - Clear separation reduces code complexity and eases updates.

----

### MVVM vs. Other Patterns

| Pattern | Key Feature                     | Use Case                     |
|---------|---------------------------------|------------------------------|
| **MVVM** | Data binding, ViewModel focus   | Modern UI frameworks (MAUI, WPF) |
| **MVC**  | Controller mediates View/Model  | Web apps, traditional UIs    |
| **MVP**  | Presenter updates View directly | Legacy frameworks, no binding |
