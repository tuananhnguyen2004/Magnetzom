using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInit : MonoBehaviour
{
    [SerializeField] private Vector2 spawnPos;

    public void Init()
    {
        transform.position = spawnPos;
    }
}
