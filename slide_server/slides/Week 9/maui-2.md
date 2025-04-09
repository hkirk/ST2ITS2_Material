<!-- .slide: data-background="#003d73" -->

# MAUI

## Collections + Layouts

![AU Logo](./../img/aulogo_uk_var2_white.png "AU Logo") <!-- .element style="width: 200px; position: fixed; bottom: 50px; left: 50px" -->

TODO: There are a couple of slides in Week 10 (MVVM) which belong here.

----

<!-- .slide: data-background-image="./img/goal.jpeg" -->

## Almost :) <!-- .element: class="fragment" -->

----

### Agenda

* Bindings
* Collections
    * ListView
    * CollectionView
* Navigation
* Layouts

---

## Slider Control

![Slider](https://learn.microsoft.com/en-us/dotnet/xml/microsoft.maui.controls/_images/slider.triplescreenshot.png?view=net-maui-8.0 "Slider Control") <!-- .element: style=" height: 300px"-->

* Slider has <mark>ValueChanged</mark> event and <mark>Value</mark> property.

----

## Bindings

* <!-- .element: class="fragment" data-fragment-index="0" --> To update the `Value` property we need to manually create an event handler for all `Controls`
```csharp
// in code-behind
private void Slider_OnValueChanged(object sender,
                                   ValueChangedEventArgs e)
{
    slider.Value = e.NewValue;
}
```

* <!-- .element: class="fragment" data-fragment-index="1" --> Bindings automates updating<!-- .element: class="fragment" data-fragment-index="1" --> `Value` property - instead of manually updating it through event. <!-- .element: class="fragment" data-fragment-index="1" -->

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

### Setting binding Target 'manually'

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

### BindableObject

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

* Showing a list of something is a very common UI pattern.<!-- .element: class="fragment" -->
* Think<!-- .element: class="fragment" -->
    * mails, messages, threads, images, videos ....
    * patients, students, sickness, ...
* Showing these in an efficient manner is important<!-- .element: class="fragment" -->

----

## `ListView` and `CollectionView`

* <!-- .element: class="fragment" -->We will focus on `ListView` and `CollectionView`
* Same general pattern is used for<!-- .element: class="fragment" -->
    * Picker, TableView, IndicatorView, CarouselView
* Showing a collection consists of<!-- .element: class="fragment" -->
    * <mark>ItemSource</mark> - the List of items to be displayed
    * <mark>ItemTemplate</mark> - specify a template for showing a single item from the collection

----

### Item Source

* This is defined as a binding (which we now know all about :))<br/><!-- .element: class="fragment" -->
* either in XAML<br/><!-- .element: class="fragment" -->
```xml
<ListView
    x:Name="listView"
    ItemsSource="{Binding Elements}" />
```
* or in C# (code-behind)<br/><!-- .element: class="fragment" -->
```csharp
CollectionView.SetBinding(
        CollectionView.ItemsSourceProperty,
        "Elements");
```
* <!-- .element: class="fragment" -->Here 'Elements' is the collection property in the 'BindingContext'

----

### Item Template

* ItemTemplate is a property on both CollectionView and ListView<br/><!-- .element: class="fragment" data-fragment-index="1" -->
* The type of this is a<!-- .element: class="fragment" data-fragment-index="2" --> [DataTemplate](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.controls.datatemplate?view=net-maui-8.0)<br/><!-- .element: class="fragment" data-fragment-index="2" -->
* DataTemplate must reference a <!-- .element: class="fragment" data-fragment-index="3" -->[Cell](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.controls.cell?view=net-maui-8.0)<!-- .element: class="fragment" data-fragment-index="2" -->
    * TextCell, ImageCell - kinda standard
    * ViewCell - custom


----

### Cell examples

* TextCell - which holds just text<!-- .element: class="fragment" -->
```xml
<DataTemplate>
    <TextCell Text="{Binding Name}"
        Detail="{Binding Description}"
        />
</DataTemplate>
```
* ImageCell - TextCell + Image<!-- .element: class="fragment" -->
```xml
<DataTemplate>
    <ImageCell
        ImageSource="{Binding Image}"
        Text="{Binding Name}"
        Detail="{Binding Description}"
        />
</DataTemplate>
```

----

### ViewCell e.g.

* Can contain more or less what you want
```xml
<ViewCell>
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
</ViewCell>
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
* Properly more flexible in larger applications<br/><!-- .element: class="fragment" -->
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


---

## Layouts

![Layouts](https://learn.microsoft.com/en-us/dotnet/maui/user-interface/layouts/media/layouts.png?view=net-maui-8.0 "Layouts in maui")


----

### Auto layouts

* Stack layout<br/><!-- .element: class="fragment" -->
    * Can be horizontal or vertial
    * Often used as parent layout
    * Should not be nested to complex
        * then Grid/flex is better
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
    <!-- content here -->
</Grid>
```

----

### Grid Content

* Content is added after
    * Grid.Column / Grid.Row used to specifiy which cell
    * Grid.Span used to make content span multiple cell

```xml
<Grid>
    <!-- row/colum def here -->
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