using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatedFinalDoor : MonoBehaviour
{
    [SerializeField] private VoidPublisherSO doorActivatedPublisher;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            doorActivatedPublisher.RaiseEvent();
        }
    }
}
