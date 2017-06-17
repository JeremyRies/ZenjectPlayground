using Zenject;
using UnityEngine;
using System.Collections;

public class TestInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<string>().FromInstance("Hello World!");
        Container.Bind<Greeter>().AsSingle();
        Container.Bind<SuperGreeter>().AsSingle().NonLazy();
        
    }
}

public class Greeter
{
    private readonly string _message;

    public Greeter(string message)
    {
        _message = message;
        Debug.Log(message);
    }

    public override string ToString()
    {
        return _message;
    }
}

public class SuperGreeter
{
    private readonly Greeter _greeter;


    public SuperGreeter(Greeter greeter)
    {
        _greeter = greeter;
        Debug.Log("SuperGreeter" + _greeter);
    }
}