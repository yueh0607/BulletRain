using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> where T : Singleton<T>
{

    protected Singleton() { }

    private static T instance = null;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                //public new /private new
                instance = Activator.CreateInstance<T>();
            }
            return instance;
        }
    }
}