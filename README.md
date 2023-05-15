# Geranium.Toposort
[![NuGet version](https://badge.fury.io/nu/Geranium.Toposort.svg)](https://badge.fury.io/nu/Geranium.Toposort)

Interface and class, base alghoritm-implementations, for `Types` sorted according to dependencies from each other.

## Example
Define `BaseModule` class extended from `ToposortType`, create 3 inheritors, and set dependencies of types:
```C#
class BaseModule : ToposortType { }
class Module1 : BaseModule { }
class Module2 : BaseModule {
    protected override void SetDependencies() => this.DependsOn<Module3>();
}
class Module3 : BaseModule {
    protected override void SetDependencies() => this.DependsOn<Module1>();
}
```
Sort instances:
```C#
[TestMethod]
public void DependenciesByTypesTest()
{
    var m1 = new Module1();
    var m2 = new Module2();
    var m3 = new Module3();

    var modules = new BaseModule[] { m1,m2,m3 };
    var sorted = modules.Sort().ToArray();

    Assert.AreEqual(m1, sorted[0]);
    Assert.AreEqual(m3, sorted[1]);
    Assert.AreEqual(m2, sorted[2]);
}
```

## Components
There is base interfacem but better use implementations, and extension methods with generic type checks:
* `Itoposort` - base interface, use it when u can't use base class
* `ToposortType` - base class implemented all logic
* `ToposortExtensions.Sort` - implicit extension method for topo sorting
* `ToposortExtensions.TopoSort` - explicit extension method for topo sorting
* `delegate Toposorting` - delegate "extension point"

## Extensions
#### Interface
You can implement your custom `IToposort` class, for example: set dependencies not by type and on runtime.
#### Sorting
Additionaly, you can pass your own ` delegate Toposorting<TToposort>` in `Sort` and `TopoSort` methods.
