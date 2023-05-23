in NvrGazeInputModule, change line:

[if using one camera]
centerOfScreen = new Vector2(Screen.width / 2, Screen.height / 2);

[if using dual camera]
centerOfScreen = new Vector2(Screen.width / 4, Screen.height / 2);