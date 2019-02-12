# Embedding SQLite in Console Application Demo

[Costura.Fody](https://github.com/Fody/Costura) is an add-in for [Fody](https://github.com/Fody/Home/) and it is by far the best and easiest way to embed external assemblies in your executable.

It is just enough to add [Costura NuGet Package](https://www.nuget.org/packages/Costura.Fody/) to your project and all of the external libraries will automatically be embedded into your executable after build!

In our case external library **SQLite.Interop.dll** targeting both platforms, 32 and 64-bits and **we have to embed both dlls**. That's why we have to create two folders called costura32 and costura64 and copy the correct version of **SQLite.Interop.dll** into each one. 
<u>We also have to change the Build Action for both files to Embedded Resource.</u>

![](https://github.com/matjazbravc/Embedded-SQLite-Demo/blob/master/res/CosturaFody.jpg)

By installing Costura NuGet Package a config file called **FodyWeavers.xml** was added to the root folder of project. Usually you don’t have to specify what external libraries to embed, but for all external libraries that are targeted to a specific platform (32 and 64-bits) we have to specify this in the **FodyWeavers.xml** like it is in our case (do not include .dll in the names):

```xml
<?xml version="1.0" encoding="utf-8"?>
<Weavers xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xsi:noNamespaceSchemaLocation="FodyWeavers.xsd">
  <Costura IncludeDebugSymbols='false'>
    <Unmanaged32Assemblies>
      SQLite.Interop
    </Unmanaged32Assemblies>
    <Unmanaged64Assemblies>
      SQLite.Interop
    </Unmanaged64Assemblies>
  </Costura>
</Weavers>
```
**It is also important that you set IncludeDebugSymbols='false' which exclude .pdbs for referenced assemblies to be embedded.**

You will notice that after rebuild your project is quite larger then it was before.
Now you can move your executable outside debug directory and run it without breaking any dependencies. 

## Prerequisites:
- [Visual Studio](https://www.visualstudio.com/vs/community) 2017 15.9.x
- [System.Data.SQLite NuGet Package](https://www.nuget.org/packages/System.Data.SQLite/)
- [Costura NuGet Package](https://www.nuget.org/packages/Costura.Fody/)

Enjoy!

## Licence

Licenced under [MIT](http://opensource.org/licenses/mit-license.php).
Contact me on [LinkedIn](https://si.linkedin.com/in/matjazbravc).
