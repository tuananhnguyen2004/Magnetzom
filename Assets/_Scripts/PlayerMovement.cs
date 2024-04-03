using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float smoothTime;

    private Vector2 movementVect;
    private Rigidbody2D rb;
    private Vector2 currVel;
    private Animator anim;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        currVel = Vector2.zero;
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        movementVect.x = Input.GetAxisRaw("Horizontal");
        movementVect.y = Input.GetAxisRaw("Vertical");

        if (movementVect != Vector2.zero)
        {
            AudioManager.Instance.EnableSound("concrete-footsteps");
            anim.SetBool("isRunning", true);
        }
        else
        {
            AudioManager.Instance.DisableSound("concrete-footsteps");
            anim.SetBool("isRunning", false);
        }

        //Vector2 mouseDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        //var localScale = transform.localScale;
        //if (mouseDir.x >= 0)
        //{
        //    localScale.x = Mathf.Abs(localScale.x);
        //}
        //else
        //{
        //    localScale.x = -Mathf.Abs(localScale.x);
        //}
        //transform.localScale = localScale;
    }

    private void OnDisable()
    {
        rb.velocity = Vector2.zero;
        AudioManager.Instance.DisableSound("concrete-footsteps");
        anim.SetBool("isRunning", false);
    }

    private void FixedUpdate()
    {
        var topSpeed = movementVect.normalized * moveSpeed;

        rb.velocity = Vector2.SmoothDamp(rb.velocity, topSpeed, ref currVel, smoothTime);
    }
}
