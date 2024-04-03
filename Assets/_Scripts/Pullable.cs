using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pullable : MonoBehaviour
{
    private Rigidbody2D rb;
    [HideInInspector] public Collider2D col;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    public void PullObject(float pullForce, Vector2 pullDirection)
    {
        rb.AddForce(pullDirection * pullForce, ForceMode2D.Force);
    }

    public void ShootObject(float shootForce, Vector2 shootDirection)
    {
        rb.AddForce(shootDirection * shootForce, ForceMode2D.Impulse);
        col.enabled = true;
    }
}
