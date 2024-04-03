using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Bool Pulisher", menuName = "Scriptable Objects/Events/Bool Publisher")]
public class BoolPublisherSO : ScriptableObject
{
    public UnityAction<bool> OnEventRaised;

    public void RaiseEvent(bool flag)
    {
        OnEventRaised?.Invoke(flag);
    }
}
