using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitLife : MonoBehaviour, IDestroyable
{
    [SerializeField] private float maxHealth;

    [SerializeField] private float _health;
    [SerializeField] private FloatPublisherSO OnHealthChangedPublisher;
    [SerializeField] private VoidPublisherSO OnDiePublisher;
    [SerializeField] private ParticleSystem particle;
    public float Health { get => _health; set => _health = value; }
    public float MaxHealth { get => maxHealth; private set { } }

    private void Awake()
    {
        Health = maxHealth;
    }

    public void CheckDie()
    {
        if (OnDiePublisher != null)
        {
            OnDiePublisher.RaiseEvent();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Attack(float damage)
    {
        Debug.Log("attack");
        Health -= damage;
        Instantiate(particle, transform.position, Quaternion.identity);
        AudioManager.Instance.PlaySound("blood-splash");
        OnHealthChangedPublisher.RaiseEvent(damage);
        if (Health <= 0f)
        {
            CheckDie();
        }
    }

    public void ResetHealth()
    {
        Health = maxHealth;
    }
}
