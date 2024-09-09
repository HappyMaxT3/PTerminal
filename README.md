# PTerminal - .NET MAUI App

### PTerminal 
PTerminal is an multiplatform application that simulates some functions of a computer terminal on phone (only Android 11+, other OS are in testing). 

Implement functions:
1. ```man``` - Show the entire manual, which explains all the commands and their meaning.
2. ```clear``` - Clear terminal completely.
3. ```lshw``` - Find out the all available info about system.
4. File commands:
   1. ```cd``` - Change user's work directory. Android users should select a folder in device Explorer after typing 'cd'.
   2. ```ls``` - Shows directories and files in user's work directory.
   3. ```rm `file` ``` - Delete selected file.
      
File commands require permissions. For the app work correctly, go to settings and give the necessary permissions to PTerminal.

### MAUI
.NET MAUI is a SDK which allows to develop multiplatform applications with C# and set of Microsoft tools. More details can be found on the ofiicial [website](https://learn.microsoft.com/ru-ru/windows/apps/windows-dotnet-maui/tutorial-csharp-ui-maui-toolkit).

# Installing

### App
Android 11+ user should install ```.apk``` file in a ```INSTALL_APP_FOLDER``` and give the necessary permissions in settings. 

### Source code
To install the source code the programmer must have Visual Studio or VS Code with all the necessary SDK. Namely:
1. .NET SDK 8.0
2. MAUI
3. Android SDK and Android API Levels or Android Device (OS version 11+)
4. Java Development Kit (JDK)
5. Xcode and iOS SDK (for macOS programmers)

For any problems with installing the source code, contact Microsoft support or visit the StackOverflow (Only God can help you).
