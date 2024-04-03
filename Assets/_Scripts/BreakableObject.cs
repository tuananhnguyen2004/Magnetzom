using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : MonoBehaviour, IDestroyable
{
    [SerializeField] private float maxToleranceSpeed;
    [SerializeField] private float maxHitPoints;
    [SerializeField] private float objectDamage;

    private float _currentHealth;
    private Rigidbody2D rb;
    private Vector2 currentVel;
    private bool canAttack = true;

    public float Health { get =>_currentHealth; set => _currentHealth = value; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Health = maxHitPoints;
    }

    private void Update()
    {
        currentVel = rb.velocity;
    }

    public void CheckDie()
    {
        if (Health <= 0f)
            Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!canAttack)
            return;

        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Enemy"))
        {
            if (currentVel.magnitude > maxToleranceSpeed)
            {
                Debug.Log(currentVel.magnitude);
                Health -= 1;
                CheckDie();
            }

            if (collision.gameObject.CompareTag("Enemy") && collision.gameObject.TryGetComponent<UnitLife>(out UnitLife life))
            {
                var atk = GetAttackPoint();
                Debug.Log(atk);
                if (atk / objectDamage >= .5)
                {
                    canAttack = false;
                    StartCoroutine(Count());
                    life.Attack(atk);
                }
            }
        }
    }

    private IEnumerator Count()
    {
        var counter = 0f;
        while (counter < .5f)
        {
            counter += Time.deltaTime;
            yield return null;
        }
        canAttack = true;
    }

    private float GetAttackPoint()
    {
        var delta = 0f;
        var rbSpeed = rb.velocity.magnitude;
        if (rbSpeed >= 0 && rbSpeed < 10)
        {
            Debug.Log("no force");
            delta = 0f;
        }
        else if (rbSpeed >= 10 && rbSpeed < 15)
        {
            Debug.Log("weak force");
            delta = .5f;
        }
        else if (rbSpeed >= 15 && rbSpeed < 25)
        {
            Debug.Log("normal force");
            delta = 1f;
        }
        else if (rbSpeed >= 25)
        {
            Debug.Log("strong force");
            delta = 1.5f;
        }
        Debug.Log(rb.velocity.magnitude);
        return objectDamage * delta;
    }
}
