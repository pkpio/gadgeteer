# Structure

We have everything spread across 4 ```Namespaces```.

- Namespace ```Mastermind```
  - ```Program.cs``` 
    - The program starts here
    - Initializes ```Controller```
    - Starts ```StartScreen``` and that's it.
  - ```Controller.cs``` 
    - ```Controller``` is an equivalent of ```Context``` for Android. 
    - All modules are interfaced using this.
    - Screen - Screen transitions are handled by this

- Namespace ```Mastermind.Screens```
  - ```Screen``` is as an equivalent of ```Activity``` for Android
  - Use  ```Controller.ChangeScreen``` to change screens. Just like, ```context.startActivity```
  - ```Init()``` method will be the entry point for a screen

- Namespace ```Mastermind.Widgets```
  - 5 Widgets.
    - TextView
    - RectangleView
    - EllipseView
    - CodeView
    - KeyView
  - Each View abstracts the graphical actions assosiated with it.

- Namespace ```Mastermind.Modules```
  - ```JoystickHandler```
    - Initialized in ```Program.cs``` at startup
    - Available in all screens to register for event callbacks
  - ```ButtonHandler```
    - Initialized in ```Program.cs``` at startup
    - Available in all screens to register for event callbacks for ```Press```, ```Release``` and ```Long press```

