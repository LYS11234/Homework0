using System;
using System.Collections.Generic;
using UnityEngine;

public class Subject : MonoBehaviour, ISubject
{
    private List<IObserver> observers = new List<IObserver>();
    public void RegisterObserver(IObserver observer)
    {
        observers.Add(observer);
    }

    public void RemoveObserver(IObserver observer)
    {
        observers.Remove(observer);
    }

    public virtual void NotifyObservers()
    {
        
    }

}
