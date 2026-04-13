<!-- .slide: data-background="#003d73" -->

# MAUI

## Collections + Layouts

![AU Logo](./../img/aulogo_uk_var2_white.png "AU Logo") <!-- .element style="width: 200px; position: fixed; bottom: 50px; left: 50px" -->

<!-- TODO: There are a couple of slides in Week 10 (MVVM) which belong here. -->

----

<!-- .slide: data-background-image="./img/goal.jpeg" -->

## Almost :) <!-- .element: class="fragment" -->

----

### Agenda

* Bindings<br/><!-- .element: class="fragment" --> 
* Collections<br/><!-- .element: class="fragment" --> 
    * CollectionView
* Navigation<br/><!-- .element: class="fragment" --> 
* Layouts<br/><!-- .element: class="fragment" -->

---

## Slider Control

![Slider](https://learn.microsoft.com/en-us/dotnet/xml/microsoft.maui.controls/_images/slider.triplescreenshot.png?view=net-maui-8.0 "Slider Control") <!-- .element: style=" height: 300px"-->

* Slider has <mark>ValueChanged</mark> event and <mark>Value</mark> property.

----

## Bindings

* <!-- .element: class="fragment" --> The <code>Value</code> property can be read as a 'normal' property<br/>
* <!-- .element: class="fragment" --> <code>ValueChanged</code> is an event that is triggered
* 
```csharp
// in code-behind
private void Slider_OnValueChanged(object sender,
                                   ValueChangedEventArgs e)
{
    // ....
}
```

----

### Generally

* all values can be updated
* all values can be accessed

----

## Target / Source

* If we would like to update `Label` text-size, text-rotation... when draging on slider

```xml
<ContentPage ...>
    <StackLayout Padding="10, 0">
        <!-- TARGET -->
        <Label x:Name="label" 
               Text="TEXT"
               FontSize="48"
               HorizontalOptions="Center"
               VerticalOptions="Center" />
        <!-- Source -->
        <Slider x:Name="slider"
                Maximum="360"
                VerticalOptions="Center" />
    </StackLayout>
</ContentPage>
```

----

### Or Setting binding Target 'manually'

* Involves setting
    1. `BindingContext` - meaning the source
    1. `SetBinding()` - mapping target and source property

```csharp
public MainPage()
{
    InitializeComponent();
    label.BindingContext = slider; // 1
    label.SetBinding(Label.FontSizeProperty, "Value"); // 2
}
```

----

### Binding in XAML


```xml [6|7|6-7]
<ContentPage ...>
    <StackLayout Padding="10, 0">
        <Label Text="TEXT"
               HorizontalOptions="Center"
               VerticalOptions="Center"
               BindingContext="{x:Reference Name=slider}"
               FontSize="{Binding Path=Value}" />

        <Slider x:Name="slider"
                Maximum="80"
                VerticalOptions="Center" />
    </StackLayout>
</ContentPage>
```

* Again databinding is set on <mark>Target</mark>

----

### Bind without setting `BindingContext`

* When you want to bind to multiple things<!-- .element: class="fragment" -->
    * bindings can be set per item
* in code<!-- .element: class="fragment" -->
```csharp
label.SetBinding(
    Label.FontSizeProperty,
    new Binding("Value", source: slider));
```
* or from XAML<!-- .element: class="fragment" -->
```xml
<Label ...
    FontSize="{Binding
        Source={x:Reference slider},
        Path=Value}" />
```

----

### `BindableObject`

* <!-- .element: class="fragment" -->BindableObject is the class that makes this possible
    * `View`, `Page` both inherits from this.

* This means we can also bind to properties in the ContentPage (code-behind file)<!-- .element: class="fragment" -->

----

In MainPage.xaml.cs
```csharp [2]
public partial class MainPage : ContentPage {
    public string LabelText { get; set; } = "Hej";
}
```

In MainPage.xaml
```xaml [3, 5-7]
<ContentPage 
    x:Class="Blank.MainPage"
    x:Name="mainPage">
    <Label x:Name="label"
            Text="{Binding
            Source={x:Reference mainPage},
            Path=LabelText}"
            ... />
</ContentPage>
```

---

## Collections

* Showing a list of something is a very common UI pattern.<br/><!-- .element: class="fragment" -->
* Think<!-- .element: class="fragment" -->
    * mails, messages, threads, images, videos ....
    * patients, students, sickness, ...
* Showing these in an efficient manner is important<!-- .element: class="fragment" -->

----

## `CollectionView`

* <!-- .element: class="fragment" -->We will focus on <code>CollectionView</code><br/>
* Same general pattern is used for<!-- .element: class="fragment" -->
    * Picker, TableView, IndicatorView, CarouselView
* Showing a collection consists of<!-- .element: class="fragment" -->
    * <mark><code>ItemSource</code></mark> - the List of items to be displayed
    * <mark><code>ItemTemplate</code></mark> - specify a template for showing a single item from the collection

----

### Item Source

* This is defined as a binding (which we now know all about :))<br/><!-- .element: class="fragment" -->
* either in XAML<br/><!-- .element: class="fragment" -->

```xml
<CollectionView
    x:Name="listView"
    ItemsSource="{Binding Source={x:Reference Name=MainPageCB}, Path=Elements}" />
```
* and in C# (code-behind)<br/><!-- .element: class="fragment" -->
```csharp
public List<Type> Elements {get; set;} // Collection in binding context
public MainPage()
{
    InitializeComponent();
    BindingContext = this;
    //...
}
```

----

### Item Template

* ItemTemplate is a property on CollectionView<br/><!-- .element: class="fragment" data-fragment-index="1" -->
* The type of this is a<!-- .element: class="fragment" data-fragment-index="2" --> [DataTemplate](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.controls.datatemplate?view=net-maui-8.0)<br/><!-- .element: class="fragment" data-fragment-index="2" -->
* ItemTemplate is the template that is displayed for each<br/><!-- .element: class="fragment" data-fragment-index="3" -->

----

### DataTemplate

* Can contain more or less what you want
```xml [3,7|]
<ContentPage
    ...
    xmlns:local="clr-namespace:c_sharp_ns"
>
<CollectionView>
    <CollectionView.ItemTemplate>
        <DataTemplate x:DataType="local:Type">
            <Grid Padding="10">
                ...
                <Image Grid.RowSpan="2"
                        Source="{Binding ImageUrl}" />
                <Label Grid.Column="1"
                        Text="{Binding Name}" />
                <Label Grid.Row="1"
                        Grid.Column="1"
                        Text="{Binding Location}" />
            </Grid>
        </DataTemplate>
    </CollectionView.ItemTemplate>
</CollectionView>
```

---

## Navigation

* Navigation in MAUI means navigating<!-- .element: class="fragment" -->
    * to new pages
    * and showing modals
* Basic this can be done in two ways<!-- .element: class="fragment" -->
    * Shell navigation
    * Navigation class

----

### Shell Navigation

* URL based (like a browser)<br/><!-- .element: class="fragment" -->
* More flexible in larger applications<br/><!-- .element: class="fragment" -->
* Consists of <!-- .element: class="fragment" -->
    * route
    * page
    * query parameters
* This matches how you navigate in a browser<br/><!-- .element: class="fragment" -->
* [Shell Navigation](https://learn.microsoft.com/en-us/dotnet/maui/fundamentals/shell/navigation?view=net-maui-8.0)<br/><!-- .element: class="fragment" -->

----

### Navigation class

* Is stack based<br/><!-- .element: class="fragment" -->
* Allows you to push and pop pages onto the stack<br/><!-- .element: class="fragment" -->
* To have multiple pages, you need a root page<br/><!-- .element: class="fragment" -->
```csharp
public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        MainPage = new NavigationPage(new MainPage());
    }
}
```

----

### Push and Pop pages

* Push a new page (going forward)<br/><!-- .element: class="fragment" -->
```csharp
// From MainPage
private async void OnClick(object s,
                           EventArgs a)
{
    await Navigation.PushAsync(
        new DetailsPage());
}
```
* Pop page (going back)<br/><!-- .element: class="fragment" -->
```csharp
// From DetailsPage
private async void OnClick(object s,
                           EventArgs e)
{
    await Navigation.PopAsync();
}
```

TODO: How to send data to other view

---

## Layouts

![Layouts](https://learn.microsoft.com/en-us/dotnet/maui/user-interface/layouts/media/layouts.png?view=net-maui-8.0 "Layouts in maui")


----

### Auto layouts

* Stack layout<br/><!-- .element: class="fragment" -->
    * Can be horizontal or vertial
    * Often used as parent layout
    * Should not be nested to complex
        * then <code>Grid</code>/<code>FlexLayout</code> is better
* FlexLayout<br/><!-- .element: class="fragment" -->
    * Can wrap children if there are to many


----

### Grid Definition

* Rows and Columns are defined
    * in `Row`-/`ColumnDefinition`
```xml
<Grid>
    <Grid.RowDefinitions>
        <RowDefinition Height="50" />
        <RowDefinition Height="50" />
    </Grid.RowDefinitions>
    <Grid.ColumnDefinitions>
        <ColumnDefinition Width="Auto" />
        <ColumnDefinition />
    </Grid.ColumnDefinitions>
    <!-- content here (next slide) -->
</Grid>
```

----

### Grid Content

* Content is added after
    * Grid.Column / Grid.Row used to specifiy which cell
    * Grid.Span used to make content span multiple cell

```xml
<Grid>
    <!-- (prev slide) row/colum def here -->
    <Label Text="Column 0, Row 0"
           WidthRequest="200" />
    <Label Grid.Column="1"
           Text="Column 1, Row 0" />
    <Label Grid.Row="1"
           Text="Column 0, Row 1" />
    <Label Grid.Column="1"
           Grid.Row="1"
           Text="Column 1, Row 1" />
</Grid>
```

----

### AbsoluteLayout

* Place children at specific coordinates
* Every element is given a placement and size
    * in pixels
```xml
<AbsoluteLayout Margin="40">
    <BoxView Color="Red"
             AbsoluteLayout.LayoutFlags="PositionProportional"
             AbsoluteLayout.LayoutBounds="0.5, 0, 100, 100"
             Rotation="30" />
</AbsoluteLayout>
```

---

## References

* [Navigation](https://learn.microsoft.com/en-us/dotnet/maui/user-interface/pages/navigationpage?view=net-maui-8.0 "Navigation")
* [Layouts](https://learn.microsoft.com/en-us/dotnet/maui/user-interface/layouts/?view=net-maui-8.0 "Layouts")

---

## MAUI:  Summary


----

<!-- .slide: style="font-size: 32px" -->

## Core Concepts

### Bindings
- **Target/Source**: Bind UI elements (e.g., `Label`, `Slider`) to properties or events.
- **XAML/Code**: Use `BindingContext` and `SetBinding()` in code, or `Binding` syntax in XAML.
- **Flexibility**: Bind to multiple sources or properties per element.

### Collections
- **CollectionView**: Efficiently display lists using `ItemsSource` (data) and `ItemTemplate` (UI template).
- **DataTemplate**: Customize how each item is rendered.

----

<!-- .slide: style="font-size: 36px" -->

## Navigation & Layouts

### Navigation
- **Shell**: URL-based, flexible for larger apps (routes, pages, query parameters).
- **Navigation Class**: Stack-based; use `PushAsync`/`PopAsync` for page transitions.

### Layouts
- **StackLayout**: Simple, linear arrangement (horizontal/vertical).
- **Grid**: Define rows/columns for structured content placement.
- **AbsoluteLayout**: Position elements at specific coordinates.

----

## Practical Example

```xml
<!-- Binding Example -->
<Label Text="{Binding Source={x:Reference slider}, Path=Value}" />

<!-- CollectionView Example -->
<CollectionView ItemsSource="{Binding Items}">
  <CollectionView.ItemTemplate>
    <DataTemplate>
      <Label Text="{Binding Name}" />
    </DataTemplate>
  </CollectionView.ItemTemplate>
</CollectionView>
```

**Key Takeaway**: MAUI enables dynamic UIs with bindings, efficient collections, and flexible navigation/layouts.