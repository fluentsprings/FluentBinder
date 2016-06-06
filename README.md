# FluentBinder
# Intro

FluentBinder is a library extension for INotifyPropertyChanged interface to make your code more clean, readable and structured.

Imagine that you have a ViewModel that inherits INotifyPropertyChanged in your client code you subscribed for OnPropertyChanged event which looks like this:

```
public void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
{
      switch (e.PropertyName)
      {
           case nameof(ViewModel.Id):
           //...code here to handle "Id" property change...
           break;
           case nameof(ViewModel.Name):
           //...code here to handle "Name" property change...
           break;
           case nameof(ViewModel.Description):
           //...code here to handle "Description" property change...
           break;
           case nameof(ViewModel.Value):
           //...code here to handle "Value" property change...
           break;
           case nameof(ViewModel.IsValid):
           ...code here to handle "IsValid" property change...
           break;
           //................ further switch code here................
      }
}
```        


Here is FluentBinder version of code above that actually does more than just switch:
```
ViewModel.Bind(x => x.Id, IdPropertyHandler);
ViewModel.Bind(x => x.Name, NamePropertyHandler);
ViewModel.Bind(x => x.Description, DescriptionPropertyHandler);
ViewModel.Bind(x => x.Value, FirstValuePropertyHandler);
ViewModel.Bind(x => x.Value, SecondValuePropertyHandler);
ViewModel.Bind(x => x.IsValid, x => x.IsValid, IsValidPropertyHandler);
ViewModel.Bind(x => x.IsValid, x => !x.IsValid, IsNotValidPropertyHandler);
```

FluentBinder code can handle multiple handlers and handlers with criteria.

Handler - the last parameter in "Bind" method is "Action" type. In case you have parameters in your handler method you can specify them like that: 
```ViewModel.Bind(x => x.Description, () => DescriptionPropertyHandler(Parameter));```
or you can use predefined ViewModel type parameter in your handler like that:
```
private void DescriptionPropertyHandler(ViewModel viewModel)
{
    //....
}

ViewModel.Bind(x => x.Description, DescriptionPropertyHandler);
```

If you need unbind your handlers you have 5 alternatives:

1. Unbind by property name or property: ```ViewModel.Unbind(nameof(ViewModel.Id));``` or ```ViewModel.Unbind(x => x.Id);``` Just keep in mind if you remove just by property name - all handlers will be unbound of this property, in case you have multiple handlers.
2. Unbind by property name and handler reference: ```ViewModel.Unbind(nameof(ViewModel.Id), handler);```
Also keep in mind that it should be the same reference of Action type that you have provided while binding.
3. Unbind by property name, handler and filter: ```ViewModel.Unbind(nameof(ViewModel.IsValid), handler, x => !x.IsValid);```
4. Unbind by property name and filter: ```ViewModel.Unbind(nameof(ViewModel.IsValid), x => !x.IsValid);```
There is a static class if you want to reset all your bindings: ```FluentBinder.ResetAll();```
