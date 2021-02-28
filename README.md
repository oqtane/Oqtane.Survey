# Oqtane Survey Module
An [Oqtane](https://github.com/oqtane/oqtane.framework) module that allows administrators to create user surveys.

![Animation](https://user-images.githubusercontent.com/1857799/108630910-2d215e80-741c-11eb-91eb-8249195728fa.gif)

## Features

* Unlimited Surveys
* Unlimited Survey Questions
* Survey responses in pie charts 
* Survey Question Types
  * Text Box
  * Text Area
  * Date
  * Date Time
  * Dropdown
  * Multi-Select Dropdown

![image](https://user-images.githubusercontent.com/1857799/109429945-7d1b9a80-79b3-11eb-92fe-6a98090f16a2.png)

![image](https://user-images.githubusercontent.com/1857799/109429948-80168b00-79b3-11eb-8c84-620093a98c08.png)

# Install (into Oqtane)

In a runing version of **Oqtane**, log in as the **Administrator**, and open the **Module Management** in **Admin Dashboard**. On **Download** tab find **Survey** in list of modules. Click on its **Download** button. After it downloads click the **Install** button.

# Install Source
In order to deploy/install a module you should modify the folder path in the debug.cmd and release.cmd files to match the location on your system where the Oqtane framework is installed. Then when you execute this file it will create a Nuget package and copy it to the specified location where the framework will automatically install it upon startup.

# About Oqtane
![Oqtane](https://github.com/oqtane/framework/blob/master/oqtane.png?raw=true "Oqtane")

[Oqtane](https://github.com/oqtane/oqtane.framework) is a Modular Application Framework. It leverages Blazor, an open source and cross-platform web UI framework for building single-page apps using .NET and C# instead of JavaScript. Blazor apps are composed of reusable web UI components implemented using C#, HTML, and CSS. Both client and server code is written in C#, allowing you to share code and libraries.

Oqtane is being developed based on some fundamental principles which are outlined in the [Oqtane Philosophy](https://www.oqtane.org/Resources/Blog/PostId/538/oqtane-philosophy).

Please note that this project is owned by the .NET Foundation and is governed by the **[.NET Foundation Contributor Covenant Code of Conduct](https://dotnetfoundation.org/code-of-conduct)**
