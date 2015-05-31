# gadgeteer

Tk3 gadgeteer assignment


# Structure

Now we have everything spread across 4 ```Namespaces```. Kind of like ```Packages``` in Java.

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
  - 3 Widgets. ```TextView```, ```RectangleView``` and ```EllipseView```
  - Feel free to add new widgets or methods in existing widgets
  - Do NOT change the ```visibilty``` of fields
    - Need to modify fields after initialization? 
    - Add methods like ```SetFillColor``` in ```RectangleView```
  - Just stick to the existing design

- Namespace ```Mastermind.Modules```
  - ```JoystickHandler```
    - Initialized in ```Program.cs```
    - Available in all screens to register for event callbacks
    - Use ```mController.SetJoystickCallback(callBackMethod)``` to register callbacks
  - ```ButtonHandler```
    - Not implemented yet
    - Ideally, should be done the same way as above

- Notes
  -  Make sure to import namespace when using classes from other namespace.
