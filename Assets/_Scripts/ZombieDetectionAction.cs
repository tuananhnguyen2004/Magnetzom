using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieDetectionAction : MonoBehaviour
{
    [SerializeField] private float timeBetweenAttack;
    [SerializeField] private float atkDamage;
    [SerializeField] private float checkNearbyRadius;
    [SerializeField] private LayerMask nearbyLayer;

    [HideInInspector] public bool isCollided;

    private DetectionZone detection;
    private Animator anim;
    private RaycastHit2D hit;
    private float distance;
    private bool canAttack = true;
    private bool isTargetFirstDetected;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        detection = GetComponentInChildren<DetectionZone>();
    }

    void Update()
    {
        if (anim)
        {
            hit = detection.DetectTarget();
            if (hit)
            {
                if (!isTargetFirstDetected)
                {
                    AudioManager.Instance.PlaySound("zombie-growl");
                    isTargetFirstDetected = true;
                }

                distance = Vector2.Distance(transform.position, hit.transform.position);
                if (distance > detection.minDistance)
                    anim.SetBool("isChasing", true);
                else
                {
                    anim.SetBool("isChasing", false);
                    
                    if (hit.collider.TryGetComponent<UnitLife>(out UnitLife unitLife))
                    {
                        Attack(unitLife);
                    }
                }
                CheckEnemyNearby();
            }
            else
            {
                isTargetFirstDetected = false;
                anim.SetBool("isChasing", false);
            }
        }
    }

    void CheckEnemyNearby()
    {
        var hits = Physics2D.OverlapCircleAll(transform.position, checkNearbyRadius, nearbyLayer);
        for (int i = 0; i < hits.Length; ++i)
        {
            var dir = transform.position - hits[i].transform.position;
            transform.position += dir * Time.deltaTime;
        }
    }

    private void Attack(UnitLife target)
    {
        if (canAttack)
        {
            Debug.Log("attack");
            target.Attack(atkDamage);
            canAttack = false;
            StartCoroutine(Counter());
        }
    }

    private IEnumerator Counter()
    {
        var counter = 0f;
        while (counter < timeBetweenAttack)
        {
            counter += Time.deltaTime;
            yield return null;
        }
        canAttack = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Wall") || collision.collider.CompareTag("Magnetic"))
        {
            isCollided = true;
        }
        else
            isCollided = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, checkNearbyRadius);
    }
}
