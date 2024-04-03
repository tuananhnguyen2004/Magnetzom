using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    [SerializeField] private Vector2 castRange;
    [SerializeField] private float offset;
    [SerializeField] private LayerMask magneticLayer;
    [SerializeField] private float pullForce;
    [SerializeField] private float shootForce;
    [SerializeField] private float minDistance;
    [SerializeField] private Transform projetilePoint;

    private List<RaycastHit2D> collectedItems;
    private bool hasObject;
    private bool isPulling;
    private RaycastHit2D[] hits;

    private void Awake()
    {
        collectedItems = new List<RaycastHit2D>();
    }

    void Update()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        if (Input.GetKey(KeyCode.Mouse0) && !hasObject)
        {
            AudioManager.Instance.EnableSound("magnetic-field");
            isPulling = true;
        }
        else if (hasObject)
        {
            isPulling = false;
            collectedItems[0].transform.position = projetilePoint.position;

            if (collectedItems[0].collider.TryGetComponent<Pullable>(out Pullable pullable))
            {
                pullable.col.enabled = false;
            }
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            if (collectedItems.Count > 0)
            {
                if (collectedItems[0].collider.TryGetComponent<Pullable>(out Pullable pullable))
                {
                    pullable.ShootObject(shootForce, direction.normalized);
                }
                collectedItems.Clear();
            }
            AudioManager.Instance.DisableSound("magnetic-field");
            isPulling = false;
            hasObject = false;
        }

        Vector2 mouseDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        var localScale = transform.localScale;
        if (mouseDir.x >= 0)
        {
            localScale.y = Mathf.Abs(localScale.y);
        }
        else
        {
            localScale.y = -Mathf.Abs(localScale.y);
        }
        transform.localScale = localScale;
    }

    private void FixedUpdate()
    {
        if (isPulling)
        {
            Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            direction = direction.normalized * offset;
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            hits = Physics2D.BoxCastAll((Vector2) transform.position + direction, castRange, angle, transform.right, 0f, magneticLayer);

            foreach (var hit in hits)
            {
                var distance = Vector2.Distance(transform.position, hit.transform.position);
                if (hit.collider.TryGetComponent<Pullable>(out Pullable pullable))
                {
                    if (distance > minDistance)
                        pullable.PullObject(pullForce, transform.position - hit.transform.position);
                    else
                    {
                        collectedItems.Add(hit);
                        Debug.Log("item added");
                        hasObject = true;
                        break;
                    }
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            Gizmos.color = Color.green;
            Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            direction = direction.normalized * offset;
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            DrawBox((Vector2)transform.position + direction, castRange, angle, transform.right, 0f);
        }
    }

    public void DrawBox(Vector2 origin, Vector2 size, float angle, Vector2 direction, float distance)
    {
        //Setting up the points to draw the cast
        Vector2 p1, p2, p3, p4, p5, p6, p7, p8;
        float w = size.x * 0.5f;
        float h = size.y * 0.5f;
        p1 = new Vector2(-w, h);
        p2 = new Vector2(w, h);
        p3 = new Vector2(w, -h);
        p4 = new Vector2(-w, -h);

        Quaternion q = Quaternion.AngleAxis(angle, new Vector3(0, 0, 1));
        p1 = q * p1;
        p2 = q * p2;
        p3 = q * p3;
        p4 = q * p4;

        p1 += origin;
        p2 += origin;
        p3 += origin;
        p4 += origin;

        Vector2 realDistance = direction.normalized * distance;
        p5 = p1 + realDistance;
        p6 = p2 + realDistance;
        p7 = p3 + realDistance;
        p8 = p4 + realDistance;


        //Drawing the cast
        Color castColor = Color.green;
        Debug.DrawLine(p1, p2, castColor);
        Debug.DrawLine(p2, p3, castColor);
        Debug.DrawLine(p3, p4, castColor);
        Debug.DrawLine(p4, p1, castColor);

        Debug.DrawLine(p5, p6, castColor);
        Debug.DrawLine(p6, p7, castColor);
        Debug.DrawLine(p7, p8, castColor);
        Debug.DrawLine(p8, p5, castColor);

        Debug.DrawLine(p1, p5, Color.grey);
        Debug.DrawLine(p2, p6, Color.grey);
        Debug.DrawLine(p3, p7, Color.grey);
        Debug.DrawLine(p4, p8, Color.grey);
        //if (hit)
        //{
        //    Debug.DrawLine(hit.point, hit.point + hit.normal.normalized * 0.2f, Color.yellow);
        //}
    }
}