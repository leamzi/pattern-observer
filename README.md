# Observable Pattern Helper for Unity

This Unity package provides a helper class to implement the Observer pattern. The `Observable<T>` class allows you to easily create observable properties and listen for changes to their values.

## Installation

To include this package in your Unity project:

1. Download the `ObservablePatternHelper` package.
2. In Unity, go to `Window -> Package Manager`.
3. Click on the `+` button in the top left corner and select `Add package from disk`.
4. Navigate to the downloaded package and select the `package.json` file.

## Usage

### Importing the Namespace

To use the `Observable<T>` class, ensure you include the appropriate namespace in your scripts:

```csharp
using CodeForFun.Patterns;
```

### Creating an Observable
You can create an observable property by instantiating the `Observable<T>` class with an initial value:

```csharp
public class Example : MonoBehaviour
{
    private Observable<int> health;

    void Start()
    {
        health = new Observable<int>(100);
        health.AddListener(OnHealthChanged);
    }

    private void OnHealthChanged(int newHealth)
    {
        Debug.Log($"Health changed to: {newHealth}");
    }
}
```
### Changing the Value
To change the value of the observable property and notify listeners, simply set the `Value` property:

```csharp
void TakeDamage(int damage)
{
    health.Value -= damage;
}
```
### Listening for Changes
You can add or remove listeners using the `AddListener` and `RemoveListener` methods:

```csharp
void OnEnable()
{
    health.AddListener(OnHealthChanged);
}

void OnDisable()
{
    health.RemoveListener(OnHealthChanged);
}
```
### Disposing of the Observable
When you no longer need the observable, you can dispose of it to clean up resources:

```csharp
void OnDestroy()
{
    health.Dispose();
}
```
## API Reference
### `Observable<T>`
#### Properties
- `T` Value
  - Gets or sets the current value. Setting the value invokes the ValueChanged event if the value has changed.
- Constructors
  - `Observable(T value, Action<T> onValueChanged = null)`
    - Initializes a new instance of the Observable<T> class with the specified initial value and an optional value changed event handler. 
- Methods
  - `void Set(T newValue)`
    - Sets the value and invokes the ValueChanged event if the value has changed. 
  - `void Invoke()`
    - Manually invokes the ValueChanged event with the current value. 
  - `void AddListener(Action<T> handler)`
    - Adds a listener to the ValueChanged event.\
  - `void RemoveListener(Action<T> handler)`
    - Removes a listener from the ValueChanged event. 
  - `void Dispose()`
    - Disposes of the observable, removing all listeners and resetting the value to its default.

#### Events
- `event Action<T> ValueChanged`
  - Occurs when the value is changed.

#### Example

Here is a complete example demonstrating the usage of Observable<T> in a Unity script:

```csharp
using UnityEngine;
using CodeForFun.Patterns;

public class Player : MonoBehaviour
{
    private Observable<int> health;

    void Start()
    {
        health = new Observable<int>(100, OnHealthChanged);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(10);
        }
    }

    private void TakeDamage(int damage)
    {
        health.Value -= damage;
    }

    private void OnHealthChanged(int newHealth)
    {
        Debug.Log($"Health changed to: {newHealth}");
    }

    void OnDestroy()
    {
        health.Dispose();
    }
}
```