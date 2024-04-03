using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Initializer
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void InstantiatePersistentObjects()
    {
        var persistentObj = Object.Instantiate(Resources.Load("PersistentObjects"));
        Object.DontDestroyOnLoad(persistentObj);
    }
}
