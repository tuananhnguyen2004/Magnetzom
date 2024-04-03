using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BoolEventListener : MonoBehaviour
{
    [SerializeField] private UnityEvent<bool> EventResponse;
    [SerializeField] private BoolPublisherSO publisher;

    private void OnEnable()
    {
        publisher.OnEventRaised += Respond;
    }

    private void OnDisable()
    {
        publisher.OnEventRaised -= Respond;
    }

    private void Respond(bool flag)
    {
        EventResponse?.Invoke(flag);
    }
}
